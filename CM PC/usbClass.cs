using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace SpaceUSB
{
    public class Response
    {
        public Boolean success { get; set; }
        public String message { get; set; }
        public Reply tmcReply { get; set; }
    }

    public class Reply
    {
        public int replyAddress { get; set; } = 0;
        public int moduleAddress { get; set; } = 0;
        public int status { get; set; } = 0;
        public int cmdNum { get; set; } = 0;
        public int value { get; set; } = 0;
        public int checkSum { get; set; } = 0;
    }

    public class TMCConn
    {
        private static SerialPort _serialPort = new SerialPort();
        private Object portLock;
        //        public Parameters GlobalSystemValues;
        //        public ErrorsValues GlobErrRegValues;
        //        public SystemStatus GlobSystemStatus;
        //        public SimpleSystemStatus GlobSimpleSystemStatus;
        //        public ReadMotorPositions GlobMotorPositions;
        //        public int DrawVolume;
        //        public bool VialChanged;
        public bool bBlocking;
        public bool TrinamicOK = false;
        public string bBlockingFunction;
        public bool bErrorWritten = false;
        public bool eliminateSendCommand = false;

        //-------------------------------------------------------------------------
        public static Dictionary<int, string> dict = new Dictionary<int, string>
        {
            { 100, "Successfully executed, no error"},
            { 101, "Command loaded into TMCL program EEPROM"},
            { 1,"Wrong checksum"},
            { 2,"Invalid command"},
            { 3,"Wrong type"},
            { 4,"Invalid value"},
            { 5,"Con1guration EEPROM locked"},
            { 6,"Command not available"}
        };

        public void MessageResponse(Response response)
        // prints a Message box with Response f\results from Trinamic
        {
            string v =
          "CANaddr: " + Convert.ToString(response.tmcReply.replyAddress)
          + "\n module: " + Convert.ToString(response.tmcReply.moduleAddress)
          + "\n status: " + dict[Convert.ToInt32(response.tmcReply.status)]
          + "\n command:" + Convert.ToString(response.tmcReply.cmdNum)
          + "\n value:  " + Convert.ToString(response.tmcReply.value)
          + "\n Chksum: " + Convert.ToString(response.tmcReply.checkSum);
            MessageBox.Show(v);
        }

        //-------------------------------------------------------------------------
        public TMCConn()
        {
            Initialize();
            //            DrawVolume = 0;
        }

        //-------------------------------------------------------------------------
        public void Initialize()
        {
            string[] portnames = SerialPort.GetPortNames();     // get all ports
            portLock = new Object();

            // We are going to loop every serial port to start.  If we don't find one that works, we will shut down.
            /*
                 int index = -1;
                 string ComPortName = null;
                 do
                 {
                     index += 1;
                     MessageBox.Show(portnames[index]);
                 }
                 while (!((portnames[index] == ComPortName) || (index == portnames.GetUpperBound(0)))); 
            */
            foreach (string port in portnames)
            {
                // Create a new SerialPort object with default settings.
                try
                {
                    Response rResponse = new Response();
                    if (!_serialPort.IsOpen)
                    {
                        _serialPort = new SerialPort(port, 57600, Parity.None, 8, StopBits.One);
                    }
                    // Set the read/write timeouts
                    _serialPort.ReadTimeout = 500;
                    _serialPort.WriteTimeout = 500;
                    //Test the port for data from a Trinamic.  We are looking for a response of the serial port speed.  
                    rResponse = SendCommand("1", "10", "65", "0", "5");
                    //                  MessageBox.Show("Initializing,  port  " + port + ":  reply for 1 10 65 0 5:  ");
                    //                  MessageResponse(rResponse);
                    if (rResponse.success)
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    //var logger = NLog.LogManager.GetCurrentClassLogger();
                    //logger.Error(ex, "Error Initializing Port!.");
                    MessageBox.Show("Exception initialization", ex.Message);
                }
            }
            if (!_serialPort.IsOpen)
            {
                System.Diagnostics.Debug.WriteLine("Failed to open Trinamic. Shutting Down.");
                MessageBox.Show("Trinamic control board is not responding");
                TrinamicOK = false;
                //Environment.Exit(0);
            }
            else
            {
                TrinamicOK = true;
            }
            bBlocking = false;
            bBlockingFunction = "";

            //            Thread thread = new Thread(new ThreadStart(VialChangeMonitor));
            //            thread.Start();
        }

        //-------------------------------------------------------------------------
        //public bool trinamicIsOK()
        //{
        //    // MessageBox.Show(" no communication port to trinamic");
        //    return TrinamicOK;
        //}
        public bool TrinamicAborted()
        {
            // MessageBox.Show(" Trinamic aborted");
            return eliminateSendCommand;   // no communication port
        }
        public Response SendCommand(string moduleAddr, string cmdNum, string typeNum, string motorNum, string value)
        {
            /// <summary>
            /// This function send command to TMC board and get the response from the board. 
            /// 
            /// Send Command Structure: 
            ///          1                1             1            1          4        1             => 9 bytes in total
            ///   <ModuleAddress> <CommandNumber> <TypeNumber> <MotorNumber> <Value> <CheckSum>
            ///
            /// Reply Message Structure:
            ///          1              1            1           1           4        1                => 9 bytes in total
            ///   <ReplyAddress> <ModuleAddress> <Status> <CommandNumber> <Value> <CheckSum>
            /// 
            /// </summary>
            /// <param name="moduleAddr"></param>
            /// <param name="cmdNum"></param>
            /// <param name="typeNum">  Somtimes used as Global Param / User Variable Number</param>
            /// <param name="motorNum"> Somtimes used as Bank Number</param>
            /// <param name="value"> 4 bytes MSB first</param>
            /// <returns></returns>

            lock (portLock)
            {
                Response response = new Response();

                if (eliminateSendCommand)
                {
                    // MessageBox.Show("Aborted in portLock, eliminates communication");
                    // return response;
                }
                try
                {
                    byte nModuleAddr = Byte.Parse(moduleAddr);
                    byte nCmd = Byte.Parse(cmdNum);
                    byte nType = Byte.Parse(typeNum);
                    byte nMotor = Byte.Parse(motorNum);
                    int nValue = Int32.Parse(value);

                    if (!_serialPort.IsOpen)
                    {
                        _serialPort.Open();
                        System.Diagnostics.Debug.WriteLine("Serial port is open.");
                    }

                    byte[] cmd = GetCmdBytes(nModuleAddr, nCmd, nType, nMotor, nValue);

                    //write commands to serial port
                    _serialPort.Write(cmd, 0, cmd.Length);

                    //read reply message
                    byte[] readBytes = new byte[9];
                    //int num = _serialPort.Read(readBytes, 0, 9);

                    int i = 0;
                    int count = 0;
                    while (count < 100 && i < 9)
                    {                   //try at most 100 times
                        int val = _serialPort.ReadByte();

                        if (val > -1)
                        {
                            readBytes[i] = (byte)val;
                            i++;
                        }
                        count++;
                    }

                    if (count < 100)         //means read success
                    {
                        //System.Diagnostics.Debug.WriteLine("Read from Serial Port Succeed.");
                        Reply result = new Reply();
                        ParseReplyBytes(readBytes, result);

                        if (result.status == 100 || result.status == 101)   // 100 -- Success with no error. 101 -- Command loaded into TMCL program EEPROM
                            response.success = true;

                        response.message = dict[result.status];
                        response.tmcReply = result;
                    }

                    // _serialPort.Close();

                    return response;
                }
                catch (Exception e)
                {

                    //var logger = NLog.LogManager.GetCurrentClassLogger();
                    //logger.Debug(e, "Serial Port Communication Issue!");

                    System.Diagnostics.Debug.WriteLine(e.Message);
                    if (_serialPort.IsOpen)
                        _serialPort.Close();

                    response.message = e.Message;

                    return response;

                }
            }
        }

        //-------------------------------------------------------------------------
        public byte[] GetCmdBytes(byte address, byte command, byte type, byte motor, int value)
        {
            byte[] bytes = new byte[9];

            bytes[0] = address;
            bytes[1] = command;
            bytes[2] = type;
            bytes[3] = motor;

            for (int i = 4; i < 8; i++)
            {
                bytes[i] = (byte)((value >> (7 - i) * 8) & 0xff);
            }

            int checksum = 0;
            for (int i = 0; i < 8; i++)
            {
                checksum += bytes[i];
            }
            bytes[8] = (byte)(checksum & 0xff);

            return bytes;
        }

        //-------------------------------------------------------------------------
        public void ParseReplyBytes(byte[] replyBytes, Reply result)
        {
            if (replyBytes == null || replyBytes.Length != 9)
                return;

            result.replyAddress = replyBytes[0];
            result.moduleAddress = replyBytes[1];
            result.status = replyBytes[2];
            result.cmdNum = replyBytes[3];

            for (int i = 4; i < 8; i++)
            {
                result.value = result.value * 256 + replyBytes[i];
            }

            result.checkSum = replyBytes[8];

        }
        //-------------------------------------------------------------------------
        public Response GetGGP(string numBank, string numVar)
        {
            /// <summary>
            /// This function executes GGP(10) command which reads global param/user variable according to the specified Bank num & Variable num
            /// </summary>
            /// <param name="numBank"> numBank => Motor </param>
            /// <param name="numVar">  numVar  => Type  </param>
            /// <returns>Value of the global param/user variable</returns>

            return SendCommand(AddressBank.Trinamic, Instruction.GetGlobalParameter, numVar, numBank, "0");
        }
        //-------------------------------------------------------------------------
        public Response SetSGP(string numBank, string numVar, string value)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => 0 for system, 2 for parameters
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            return SendCommand(AddressBank.Trinamic, Instruction.SetGlobalParameter, numVar, numBank, value);
        }
        //-------------------------------------------------------------------------
        public Response SetSGPandStore(string numBank, string numVar, string value)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => 0 for system, 2 for parameters
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            SendCommand(AddressBank.Trinamic, Instruction.SetGlobalParameter, numVar, numBank, value);
            return SendCommand(AddressBank.Trinamic, Instruction.StoreGlobalParameter, numVar, numBank, "0");
        }
        //-------------------------------------------------------------------------
        public Response GetGAP(string motor, string numVar)
        {
            /// <summary>
            /// This function executes GGP(10) command which reads global param/user variable according to the specified Bank num & Variable num
            /// </summary>
            /// <param name="numBank"> numBank => Motor </param>
            /// <param name="numVar">  numVar  => Type  </param>
            /// <returns>Value of the global param/user variable</returns>

            return SendCommand(AddressBank.Trinamic, Instruction.GetAxisParameter, numVar, motor, "0");
        }
        //-------------------------------------------------------------------------
        public Response SetSAP(string motor, string numVar, string value)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => Motor </param>
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            return SendCommand(AddressBank.Trinamic, Instruction.SetAxisParameter, numVar, motor, value);
        }

        //-------------------------------------------------------------------------
        public Response SetSAPandStore(string motor, string numVar, string value)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => Motor </param>
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            SendCommand(AddressBank.Trinamic, Instruction.SetAxisParameter, numVar, motor, value);
            return SendCommand(AddressBank.Trinamic, Instruction.StoreAxisParameter, numVar, motor, "0");
        }

        //-------------------------------------------------------------------------
        public Response SetOutput(string outputNum, string OnOff)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => Motor </param>
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            return SendCommand(AddressBank.Trinamic, Instruction.SetSwitchOutput, outputNum, AddressBank.setOutputs, OnOff);
        }

        //-------------------------------------------------------------------------
        public Response GetOutput(string outputNum)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => Motor </param>
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            return SendCommand(AddressBank.Trinamic, Instruction.GetGlobalInOut, outputNum, AddressBank.setOutputs, "0");
        }

        public Response GetDigitalInput(string InputNum)
        {
            /// <summary>
                        /// This function will send SGP command which set a global param/user to the specified value according to the specified Bank num & Variable num
                        /// </summary>
                        /// <param name="numBank"> numBank => Motor </param>
                        /// <param name="numVar">  numVar  => Type  </param>
                        /// <param name="value">   value   => Value </param>

            return SendCommand(AddressBank.Trinamic, Instruction.GetGlobalInOut, InputNum, AddressBank.getDigitalInputs, "0");
        }

        public Response GetAnalogInput(string InputNum)  // TODO : check with ali id need put these param lines 
        {
            /////// <summary>
                        /// This function will send GIO command which gets the specified inputs value
                        ///////// </summary>
                        ///////// <param name="numBank"> numBank => Motor </param>
                        ///////// <param name="numVar">  numVar  => Type  </param>
                        ///////// <param name="value">   value   => Value </param>

            return SendCommand(AddressBank.Trinamic, Instruction.GetGlobalInOut, InputNum, AddressBank.getAnalogInputs, "0");
        }

        //-------------------------------------------------------------------------
        public Response RunCommand(string commandToRun)
        {
            /// <summary>
                        /// This function will send a reqques to run a function in a specific address

            Response resp = new Response();

            if (commandToRun == GeneralFunctions.INIT_CM)   // value is command to run when "run command"
            {
                {
                   // MessageBox.Show("Trinamic enabled");
                    eliminateSendCommand = false;
                }
            }
            if (eliminateSendCommand == true)
            {
                //MessageBox.Show("Trinamic aborted. Turn OFF and ON the Robot, then run HOME");
                return resp;
            }

            resp = SendCommand(AddressBank.Trinamic, Instruction.RunApplication, AddressBank.CmdSpecificAddress, "0", commandToRun);

            // if aborted, let the abort command run, then eliminate further commands to execute
            // *********************************************************************************
            if (commandToRun == GeneralFunctions.ABORT)   // value is command to run when "run command"
            {
                {
                   // MessageBox.Show("Trinamic aborted. Turn OFF and ON the Robot, then run HOME");
                    MessageBox.Show("A B O R T E D");
                   // eliminateSendCommand = true;
                }
            }
            return resp;

            // the following will occur after sending the ABORT commant to the Trinamic
        }

        public Response ReadTillDone(string numBank, string numVar, string Value)
        {
            /// <summary>
            /// This function polls global parameter / user variable every second. It keeps polling until the value is 1, then return response with success = true.
            /// </summary>
            /// <param name="numBank"> numBank => Motor </param>
            /// <param name="numVar">  numVar  => Type  </param>
            /// <returns>Success = true if the value is 1, otherwise Success = fals </returns>

            Response resp = new Response();

            int count = 0;
            while (count < 450)             //poll at most 450 times or 45 seconds
            {
                resp = SendCommand(AddressBank.Trinamic, Instruction.GetGlobalParameter, numVar, numBank, "2");

                if (resp.tmcReply != null)
                    System.Diagnostics.Debug.WriteLine(String.Format("{0} : {1}", count, resp.tmcReply.value));

                if (resp.tmcReply != null && resp.tmcReply.value.ToString() == Value)
                    return resp;

                count++;
                Thread.Sleep(100);
            }

            resp.message = "Timeout Error";
            resp.success = false;
            return resp;
        }

        //-------------------------------------------------------------------------
        public Response ReadTillDoneWithErrorCheck(string numBank, string numVar, string Value)
        {
            /// <summary>
            /// This function polls global parameter / user variable every second. It keeps polling until the value is 1, then return response with success = true.
            /// </summary>
            /// <param name="numBank"> numBank => Motor </param> 2
            /// <param name="numVar">  numVar  => Type  </param> 5
            /// <returns>Success = true if the value is 1, otherwise Success = false</returns>

            Response resp = new Response();

            int count = 0;
            while (count < 450)             //poll at most 450 times or 45 seconds
            {
                resp = SendCommand(AddressBank.Trinamic, Instruction.GetGlobalParameter, numVar, numBank, "2");

                if (resp.tmcReply != null)
                    System.Diagnostics.Debug.WriteLine(String.Format("{0} : {1}", count, resp.tmcReply.value));

                if (resp.tmcReply != null && resp.tmcReply.value.ToString() == Value)
                    return resp;
                /*
                                LoadSimpleSystemStatus();
                                if (GlobErrRegValues.AnyError == "1" && numVar != SystemVariables.RetreatMovementComplete)
                                {
                                    resp.message = GlobSimpleSystemStatus.Status;
                                    resp.success = false;
                                    return resp;
                                }
                */
                count++;
                Thread.Sleep(100);
            }

            resp.message = "Timeout Error";
            resp.success = false;
            return resp;
        }
    }
}
//-------------------------------------------------------------------------

