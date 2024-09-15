using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Drawing.Printing;
using System.Diagnostics;
using System.Runtime.InteropServices;
//using BCL.easyPDF.Printer;
using Microsoft.Win32;
using System.Globalization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SpaceUSB
{
    public partial class CMForm : Form
    {
        //  all the following directories will be under the base directory:

        public string pcCode = "2024-08-18";

        public string cmPath = "C:\\cmRUN\\";    // base directory. can be changed by the user
        public string logPath = "logfiles\\";
        public string setupPath = "setupfiles\\";
        public string cmRUNpath = "cmFiles\\";
        public string logFileName = "cmLogs.txt";
        public string lastSetupName = "lastSetup.txt";
        public string SetupName = "Setup.txt";
        public string backupPath = "TrinamicBackupFiles\\";  // backupFiles\\";
                                                             // string backupFileName = "Backup"; 
                                                             // 2021-08-25 09-52 Backup sn 1030002 sw 1030002.txt
        public string cmFileNameEnd = " cmRUN.txt";

        public string paramsPath = "paramsFiles\\";
        public string paramsFileName = "cmParams.txt";
        public string goDistance_um;

        public string pwPath = "pwFiles\\";
        public string pwFileName = "cmPW.txt";

        //string pdfPath = "pdfFiles\\";       // file names acording to the .txt files

        public string username = "";
        public string userPassWord;
        public string pwMaster;
        public string runDay;
        public DateTime startProcessDate;
        public string cmFile;
        public string curentPrintFile;
        public string asVialSize = "vial_size";

        public string last_setBumpPosVertTB;
        public string last_setDockHeightTB;
        public string last_setBumpBottomTB;
        public string last_setCenterOfVial1TB;
        public string last_setCapLoadingTB;
        public string last_setVial4BottomTB;
        public string last_setVial4TopTB;
        public string last_setArmVialTB;
        public string last_setArmDisposeVials456TB;
        public string last_setArmAtBotomTB;
        public string last_setPistonStartTB;

        public string last_LD_minVolTB;
        public string last_LD_maxVolTB;
        //public string last_setLD_acceptedDevTB;
        //public string last_setLD_definedVolTB;

        public string last_setHeadRotateTopTB;
        public string last_setHeadRotateStartTB;
        public string last_setHeadAtBottomTB;
        public string last_setDropVials123TB;
        public string elapsedTime;
        public double v;
        public double thumbRestDistance;
        public double thumbRestDistanceAvg = 0;
        public double thumbRestDistanceSum = 0;
        public int thumbRestDistanceFilterSize = 4;
        public int thumbRestDistanceCount = 1;

        public int linearSpaceBetweenVialsuM;

        public bool isAdministrator = false;      // start program with non-administrator
        public bool anyErrorGotTrue = false;
        public bool readyForNewCommand;
        public bool calibrating = false;
        public bool RunInProcess = false;
        public bool cmdInProcess;
        public bool motorIsMoving;
        public bool homingDone;
        public bool anyError = false;
        public bool stopOnError = false;
        public bool nextTipInProcess;
        public bool movingToBottle;
        public bool aborted = false;
        public int microLdispensedSoFar;
        public bool askOverWrite;
        //public bool bagWasReplaced = true;
        //public bool bagWasRemoved = false;
        //public bool syringeWasReplaced = true;
        //public bool syringeWasRemoved = false;
        public bool inLDcalibLocation = false;
        public bool b;

        // *** E R R O R S ***
        public int errorsSyringeBag;
        public int errorsM_Vertical;
        public int errorsM_Linear;
        public int errorsM_Arm;
        public int errorsM_Piston;
        public int errorsM_HeadRotate;
        public int errorsM_Dispose;
        public int errorsM_CapHolder;

        public int errors_Vial_1;
        public int errors_Vial_2;
        public int errors_Vial_3;
        public int errors_Vial_4;
        public int errors_Vial_5;
        public int errors_Vial_6;

        public int errors_findHome;
        public int errors_wrong_PC_command;
        public int special_Error = 0;
        public int NeedleGauge;
        public int NeedleLength;
        public int errorsWrongPCcmd;
        public int TrinamicCode;
        public int TrinamicSerialNum;
        public int CyclesTotal;
        public int activeBottle;
        public int leftTries;
        public int getGBresult;
        public int unitsToMove;
        public int LoadingHight;
        public int linearHomePos;
        public int setCenterOfVial1;
        public int setVial4BottomLocation;
        public int setVial4TopLocation;
        public int setCapLoading;
        public int ArmHomePosition;
        public int ArmDisposePosition;
        public int BumpBottom;
        public int BumpPosVert;
        public int ArmAtBottom;
        public int PistonHomePos;
        public int HeadRotateHomePos;
        public int HeadRotateAtTop;
        public int LD_minVol;
        public int LD_maxVol;
        public int LD_definedVol;
        public int LD_acceptedDev;
        public int messuredAmountOfLiquid;
        public int Vial4Bottom;
        public int Vial4Top;
        public int DisposeDropVialPos;
        public int CapHolderHomePos;
        public int microLinBAG;
        public int microLbagToFill;
        public int Vial1Volume;
        public int Vial2Volume;
        public int Vial3Volume;
        public int Vial4Volume;
        public int Vial5Volume;
        public int Vial6Volume;
        public int vibrating4Done;
        public int vibrating56Done;
        public int vibrationTime4;
        public int vibrationTime56;
        public int vibrationHz;
        public int vibrationStrength;
        public int vialsExist;
        public bool okPolling = true;
        // *** vials exist bits ***
        public int Bit_vial1 = 0b00000001; // bit   1
        public int Bit_vial2 = 0b00000010; // bit   2
        public int Bit_vial3 = 0b00000100; // bit   4
        public int Bit_vial4 = 0b00001000; // bit   8
        public int Bit_vial5 = 0b00010000; // bit  16
        public int Bit_vial6 = 0b00100000; // bit  32
        public int Bit_bag = 0b01000000; // bit  64 

        public int maxPWtrials = 4;
        public int maxPWmonths = 5;
        public int percAddP100 = 25;
        public int maxGracePWdays = 16;
        public int currentTAB = 0;
        public Regex rgNumber = new Regex("^[0-9]+$");      // regular expression: from the start of string, any number, multiple, end of string
        public Regex rgMinus = new Regex("^-?[0-9]+$");     // regular expression: from the start of string, posible minus, any number, multiple, end of string
        public Regex rgfloat = new Regex("^([0-9]+)?.?[0-9]+$");      // regular expression: from the start of string, any number, multiple, end of string
        //Regex rgEmpty = new Regex("^[-, ]?[0-9]+$"); // regular expression: from the start of string, posible minus, any number, multiple, end of string

        private TMCConn rTMCConn;
        private Thread updateThread;
        Response tResponse = new Response();
        public string tString = "";
        public CMForm()
        {
            InitializeComponent();
            visibleFalse();
            //ThreadsClass tc = new ThreadsClass(this);   // for pointer of Form1
            checkDirectories();
            readParamsFile();
            setRegeditNotepadTextsize80();
            // following lines for testing, without pw
            isAdministrator = true;
            visibleMaster();

            if (!File.Exists(cmPath + pwPath + pwFileName))     // password file exists?
            {
                logAndShow("PW file does not exist. \n\nTry master PW, then create your master user name");
                PWfileEmptyPnl.Visible = true;          // enable master PW entrance, PW = DATE
                userPWtlp.Visible = false;
                return;
            }

            loadSetupFileCore(cmPath + setupPath + lastSetupName);

            rTMCConn = new TMCConn();
            updateThread = new Thread(ThreadPollTrinamic);
            updateThread.Start();

        }

        public void ThreadPollTrinamic()   // this is the thread that polls the motors positions 
                                           // and end Trinamic commands
        {
            // https://stackoverflow.com/questions/661561/how-do-i-update-the-gui-from-another-thread
            double dblVial1WithdrawMicroL;
            double dblVial2WithdrawMicroL;
            double dblVial3WithdrawMicroL;
            double dblVial4WithdrawMicroL;
            double dblVial5WithdrawMicroL;
            double dblVial6WithdrawMicroL;
            double dblmLbagToFillTB;

            while (true)
            {
                this.Invoke((MethodInvoker)delegate { PcCodeTB.Text = pcCode; });

                if (rTMCConn == null || !rTMCConn.TrinamicOK || aborted) continue;  // leaves the itteration as long that the conditions are true

                tResponse = rTMCConn.GetGAP(MotorsNum.M_Vertical, AddressBank.actualPosition);
                v = Convert.ToDouble(tResponse.tmcReply.value) / StepsPerMM.M_VerticalStepsPerMM;
                this.Invoke((MethodInvoker)delegate { M_VerticalLocationTb.Text = $"{v,10:0.00}"; });

                tResponse = rTMCConn.GetGAP(MotorsNum.M_Linear, AddressBank.actualPosition);
                v = Convert.ToDouble(tResponse.tmcReply.value) / StepsPerMM.M_LinearStepsPerMM;
                this.Invoke((MethodInvoker)delegate { M_LinearLocationTb.Text = $"{v,10:0.00}"; });

                tResponse = rTMCConn.GetGAP(MotorsNum.M_Arm, AddressBank.actualPosition);
                v = Convert.ToDouble(tResponse.tmcReply.value) / StepsPerMM.M_ArmStepsPerDeg;
                this.Invoke((MethodInvoker)delegate { M_ArmLocationTb.Text = $"{v,10:0.00}"; });

                tResponse = rTMCConn.GetGAP(MotorsNum.M_Piston, AddressBank.actualPosition);
                v = Convert.ToDouble(tResponse.tmcReply.value) / StepsPerMM.M_PistonStepsPerMM;
                this.Invoke((MethodInvoker)delegate { M_PistonLocationTb.Text = $"{v,10:0.00}"; });


                if (currentTAB == 3)
                {
                    //tResponse = rTMCConn.RunCommand(GeneralFunctions.getLaserDistAVAL);
                    //tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_thumbRestDistance);
                    tResponse = rTMCConn.SetOutput(SwitchOutputs.Out_Multiplexer, "0");
                    Thread.Sleep(100);   // wait 100 ms for the FUNC to finish
                    tResponse = rTMCConn.GetAnalogInput(TrinamicInputs.In_thumbRestDistance);
                    thumbRestDistance = Convert.ToDouble(tResponse.tmcReply.value);

                    // thumbRestDistanceSum += thumbRestDistance;                                  // sum input readings
                    // thumbRestDistanceAvg = thumbRestDistanceSum / thumbRestDistanceCount;       // avarage the sum by the amount of time the input was read and summed 
																															  
                    //thumbRestDistanceSum += thumbRestDistance;                                  // sum input readings
                    //thumbRestDistanceAvg = thumbRestDistanceSum / thumbRestDistanceCount;       // avarage the sum by the amount of time the input was read and summed 
                    //thumbRestDistanceSum *= Convert.ToDouble(thumbRestDistanceFilterSize - 1) / thumbRestDistanceFilterSize;

                    thumbRestDistanceAvg = (thumbRestDistance / thumbRestDistanceFilterSize) + (thumbRestDistanceAvg * (thumbRestDistanceFilterSize-1)/ thumbRestDistanceFilterSize);


																																													 

                    v = thumbRestDistanceAvg * 100 / Values.maxAVAL;                            // convert averaged value to display as percentage
                    this.Invoke((MethodInvoker)delegate { LD_valTb.Text = $"{v,10:0.00}"; });

                    // if (thumbRestDistanceCount < thumbRestDistanceFilterSize)                   // limit the summing by the amount of times dictated nt thumbRestDistanceFilterSize
                    // {
                        // thumbRestDistanceCount++;
                    // }
                    // else
                    // {
                        // thumbRestDistanceSum *= Convert.ToDouble(thumbRestDistanceFilterSize - 1) / thumbRestDistanceFilterSize;
                    // }
                }

                    //tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearSpaceBetweenVialsuM);
                    //linearSpaceBetweenVialsuM = Convert.ToInt32(tResponse.tmcReply.value);
                    //this.Invoke((MethodInvoker)delegate { linearSpaceBetweenVialsuMTB.Text = Convert.ToString(linearSpaceBetweenVialsuM, 10); });


																														
																					  
																																			 

                tResponse = rTMCConn.GetGAP(MotorsNum.M_HeadRotate, AddressBank.actualPosition);
                v = Convert.ToDouble(tResponse.tmcReply.value) / StepsPerMM.M_RotateStepsPerDeg;
                this.Invoke((MethodInvoker)delegate { M_headRotateLocationTb.Text = $"{v,10:0.00}"; });

                tResponse = rTMCConn.GetGAP(MotorsNum.M_Dispose, AddressBank.actualPosition);
                v = Convert.ToDouble(tResponse.tmcReply.value) / StepsPerMM.M_disposeMicroStepsPerMM;
                this.Invoke((MethodInvoker)delegate { M_DisposeLocationTb.Text = $"{v,10:0.00}"; });   /////////// why this line was "comment out"?

                tResponse = rTMCConn.GetGAP(MotorsNum.M_Piston, AddressBank.RightLimitSwStatus);
                b = Convert.ToBoolean(tResponse.tmcReply.value);
                if (b) // SYRINGE_IN_PLACE      =    1
                {
                    this.Invoke((MethodInvoker)delegate { syringeInPlaceTB.Text = $"SYRINGE IN"; });
                }
                else // close
                {
                    this.Invoke((MethodInvoker)delegate { syringeInPlaceTB.Text = $"NO SYRINGE"; });
                }

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_CapHolderHolds);
                b = Convert.ToBoolean(tResponse.tmcReply.value);
                if (!b)
                {
                    this.Invoke((MethodInvoker)delegate { M_CapHolderLocationTB.Text = $"O P E N"; });
                }
                else // close
                {
                    this.Invoke((MethodInvoker)delegate { M_CapHolderLocationTB.Text = $"C L O S E"; });
                }

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_CmdInProcess);
                cmdInProcess = Convert.ToBoolean(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { CmdInProcTB.Text = $"{cmdInProcess}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_MotorIsMoving);
                motorIsMoving = Convert.ToBoolean(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { MotorIsMovingTB.Text = $"{motorIsMoving}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HomingDone);
                homingDone = Convert.ToBoolean(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { HomingDoneTB.Text = $"{homingDone}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_any_Error);
                anyError = Convert.ToBoolean(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { AnyErrorTB.Text = $"{anyError}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_syringe_bag);
                errorsSyringeBag = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { SyringeBagErrorsTB.Text = Convert.ToString(errorsSyringeBag, 10); });
                this.Invoke((MethodInvoker)delegate { BagErrorTB.Text = Convert.ToString(errorsSyringeBag, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_verticalMotor);
                errorsM_Vertical = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_VerticalErrorsTB.Text = Convert.ToString(errorsM_Vertical, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_linearMotor);
                errorsM_Linear = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_LinearErrorsTB.Text = Convert.ToString(errorsM_Linear, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_armMotor);
                errorsM_Arm = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_ArmErrorsTB.Text = Convert.ToString(errorsM_Arm, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_pistonMotor);
                errorsM_Piston = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_PistonErrorsTB.Text = Convert.ToString(errorsM_Piston, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_headRotateMotor);
                errorsM_HeadRotate = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_HeadRotateErrorsTB.Text = Convert.ToString(errorsM_HeadRotate, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_disposeMotor);
                errorsM_Dispose = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_DisposeErrorsTB.Text = Convert.ToString(errorsM_Dispose, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_M_capHolderMotor);
                errorsM_CapHolder = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { M_CapHolderErrorsTB.Text = Convert.ToString(errorsM_CapHolder, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_wrong_PC_command);
                errorsWrongPCcmd = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { WrongPcErrorsTB.Text = Convert.ToString(errorsWrongPCcmd, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_findHome);
                errors_findHome = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { FindHomeErrorsTB.Text = Convert.ToString(errors_findHome, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_special_Error);
                special_Error = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { SpecialErrorsTB.Text = Convert.ToString(special_Error, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_Vial_1);
                errors_Vial_1 = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { Vial1ErrTB.Text = Convert.ToString(errors_Vial_1, 10); });
                this.Invoke((MethodInvoker)delegate { Vial1ErrorTB.Text = Convert.ToString(errors_Vial_1, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_Vial_2);
                errors_Vial_2 = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { Vial2ErrTB.Text = Convert.ToString(errors_Vial_2, 10); });
                this.Invoke((MethodInvoker)delegate { Vial2ErrorTB.Text = Convert.ToString(errors_Vial_2, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_Vial_3);
                errors_Vial_3 = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { Vial3ErrTB.Text = Convert.ToString(errors_Vial_3, 10); });
                this.Invoke((MethodInvoker)delegate { Vial3ErrorTB.Text = Convert.ToString(errors_Vial_3, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_Vial_4);
                errors_Vial_4 = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { Vial4ErrTB.Text = Convert.ToString(errors_Vial_4, 10); });
                this.Invoke((MethodInvoker)delegate { Vial4ErrorTB.Text = Convert.ToString(errors_Vial_4, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_Vial_5);
                errors_Vial_5 = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { Vial5ErrTB.Text = Convert.ToString(errors_Vial_5, 10); });
                this.Invoke((MethodInvoker)delegate { Vial5ErrorTB.Text = Convert.ToString(errors_Vial_5, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_errors_Vial_6);
                errors_Vial_6 = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { Vial6ErrTB.Text = Convert.ToString(errors_Vial_6, 10); });
                this.Invoke((MethodInvoker)delegate { Vial6ErrorTB.Text = Convert.ToString(errors_Vial_6, 10); });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_currentVersion);
                TrinamicCode = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { TrinamicCodeTB.Text = $"{TrinamicCode}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_RobotSerialNumber); // get serial #
                TrinamicSerialNum = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { robotSerialTB.Text = $"{TrinamicSerialNum}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_cyclesTotal);
                CyclesTotal = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { CyclesTotalTB.Text = $"{CyclesTotal}"; });

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, getGBnumberTB.Text);
                getGBresult = Convert.ToInt32(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { getGBresultTB.Text = $"{getGBresult}"; });

                //tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinBAG);  //GB_99
                //microLinBAG = Convert.ToInt32(tResponse.tmcReply.value);
                //this.Invoke((MethodInvoker)delegate { mLinBagTB.Text = $"{Convert.ToDouble(microLinBAG) / 1000}"; });

                //tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLbagToFill);  //GB_27
                //microLbagToFill = Convert.ToInt32(tResponse.tmcReply.value);
                //this.Invoke((MethodInvoker)delegate { mLbagToFillTB.Text = $"{Convert.ToDouble(microLbagToFill) / 1000}"; });

                if (!rgNumber.Match(Vial1WithdrawMlTB.Text).Success)
                    Vial1WithdrawMlTB.Text = "0";
                dblVial1WithdrawMicroL = Convert.ToDouble(Vial1WithdrawMlTB.Text);

                if (!rgNumber.Match(Vial2WithdrawMlTB.Text).Success)
                    Vial2WithdrawMlTB.Text = "0";
                dblVial2WithdrawMicroL = Convert.ToDouble(Vial2WithdrawMlTB.Text);

                if (!rgNumber.Match(Vial3WithdrawMlTB.Text).Success)
                    Vial3WithdrawMlTB.Text = "0";
                dblVial3WithdrawMicroL = Convert.ToDouble(Vial3WithdrawMlTB.Text);

                if (!rgNumber.Match(Vial4WithdrawMlTB.Text).Success)
                    Vial4WithdrawMlTB.Text = "0";
                dblVial4WithdrawMicroL = Convert.ToDouble(Vial4WithdrawMlTB.Text);

                if (!rgNumber.Match(Vial5WithdrawMlTB.Text).Success)
                    Vial5WithdrawMlTB.Text = "0";
                dblVial5WithdrawMicroL = Convert.ToDouble(Vial5WithdrawMlTB.Text);

                if (!rgNumber.Match(Vial6WithdrawMlTB.Text).Success)
                    Vial6WithdrawMlTB.Text = "0";
                dblVial6WithdrawMicroL = Convert.ToDouble(Vial6WithdrawMlTB.Text);

                dblmLbagToFillTB = dblVial1WithdrawMicroL
                                              + dblVial2WithdrawMicroL
                                              + dblVial3WithdrawMicroL
                                              + dblVial4WithdrawMicroL
                                              + dblVial5WithdrawMicroL
                                              + dblVial6WithdrawMicroL;

                this.Invoke((MethodInvoker)delegate { mLbagToFillTB.Text = $"{dblmLbagToFillTB}"; });

                if (rgfloat.Match(Vial1SizeMlTB.Text).Success)        // floating point number
                {
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_1);  //GB_197
                    Vial1Volume = Convert.ToInt32(Convert.ToDouble(Vial1SizeMlTB.Text) * 1000 + Convert.ToDouble(tResponse.tmcReply.value));
                    this.Invoke((MethodInvoker)delegate { mLinVial1TB.Text = $"{Convert.ToDouble(Vial1Volume) / 1000}"; });
                }

                if (rgfloat.Match(Vial2SizeMlTB.Text).Success)        // floating point number
                {
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_2);  //GB_198
                    Vial2Volume = Convert.ToInt32(Convert.ToDouble(Vial2SizeMlTB.Text) * 1000 + Convert.ToDouble(tResponse.tmcReply.value));
                    this.Invoke((MethodInvoker)delegate { mLinVial2TB.Text = $"{Convert.ToDouble(Vial2Volume) / 1000}"; });
                }

                if (rgfloat.Match(Vial3SizeMlTB.Text).Success)        // floating point number
                {
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_3);  //GB_199
                    Vial3Volume = Convert.ToInt32(Convert.ToDouble(Vial3SizeMlTB.Text) * 1000 + Convert.ToDouble(tResponse.tmcReply.value));
                    this.Invoke((MethodInvoker)delegate { mLinVial3TB.Text = $"{Convert.ToDouble(Vial3Volume) / 1000}"; });
                }

                if (rgfloat.Match(Vial4SizeMlTB.Text).Success)        // floating point number
                {
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_4);  //GB_200
                    Vial4Volume = Convert.ToInt32(Convert.ToDouble(Vial4SizeMlTB.Text) * 1000 + Convert.ToDouble(tResponse.tmcReply.value));
                    this.Invoke((MethodInvoker)delegate { mLinVial4TB.Text = $"{Convert.ToDouble(Vial4Volume) / 1000}"; });
                }

                if (rgfloat.Match(Vial5SizeMlTB.Text).Success)        // floating point number
                {
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_5);  //GB_201
                    Vial5Volume = Convert.ToInt32(Convert.ToDouble(Vial5SizeMlTB.Text) * 1000 + Convert.ToDouble(tResponse.tmcReply.value));
                    this.Invoke((MethodInvoker)delegate { mLinVial5TB.Text = $"{Convert.ToDouble(Vial5Volume) / 1000}"; });
                }

                if (rgfloat.Match(Vial6SizeMlTB.Text).Success)        // floating point number
                {
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_6);  //GB_202
                    Vial6Volume = Convert.ToInt32(Convert.ToDouble(Vial6SizeMlTB.Text) * 1000 + Convert.ToDouble(tResponse.tmcReply.value));
                    this.Invoke((MethodInvoker)delegate { mLinVial6TB.Text = $"{Convert.ToDouble(Vial6Volume) / 1000}"; });
                }

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrator4done);  //GB_121
                vibrating4Done = Convert.ToInt32(tResponse.tmcReply.value);
                if (vibrating4Done == 0)
                {
                    this.Invoke((MethodInvoker)delegate { isVibrating4TB.Text = $"VIBRATING"; });
                }
                else // ==1
                {
                    this.Invoke((MethodInvoker)delegate { isVibrating4TB.Text = $"NO"; });
                }

                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrator56done);  //GB_122
                vibrating56Done = Convert.ToInt32(tResponse.tmcReply.value);
                if (vibrating56Done == 0)
                {
                    this.Invoke((MethodInvoker)delegate { isVibrating56TB.Text = $"VIBRATING"; });
                }
                else // ==1
                {
                    this.Invoke((MethodInvoker)delegate { isVibrating56TB.Text = $"NO"; });
                }

                // arrange locations of invoking according to tabs - this goes to tab 3
                this.Invoke((MethodInvoker)delegate { thumbRestDistanceFilterSizeTB.Text = $"{thumbRestDistanceFilterSize}"; });
                this.Invoke((MethodInvoker)delegate { runInProcessTB.Text = $"{RunInProcess}"; });
                ///////////////////////////////////////////////////////////////////////

                // arrange locations of invoking according to tabs - this goes to tab 2
                this.Invoke((MethodInvoker)delegate { DateTimeNowTxt.Text = DateTime.Now.ToString("  dd-MM-yyyy   HH:mm:ss"); });
                ///////////////////////////////////////////////////////////////////////

                //                this.Invoke((MethodInvoker)delegate { PcCodeTB.Text = pcCode; });

                // arrange locations of invoking according to tabs - this goes to tab 1
                this.Invoke((MethodInvoker)delegate { runUserNameTB.Text = $" user: {username}"; });
                ///////////////////////////////////////////////////////////////////////

                readyForNewCommand = !cmdInProcess && !motorIsMoving && !anyError; ;

                //if (anyError)
                //{
                    
                //}
                if (!anyErrorGotTrue && anyError)    // will happen for one cycle after anyError was set
                {
                    ErrorsLog();
                    //if (commandToRun == GeneralFunctions.INIT_CM)   // if the is a running command of INIT_CM then end its thread
                    //    goHomeThread.Abort();
                    //    // maybe i just need to add ( ... && !anyError) to the (!aborted) in the thread it self
                }
                anyErrorGotTrue = anyError;

                // *********************************************************************************************
                // ******  Check bag and vials ******
                // *********************************************************************************************

                if (!RunInProcess && (currentTAB == 2 || currentTAB == 5))
                {
                    // tResponse = rTMCConn.SetOutput(SwitchOutputs.Out_Multiplexer, "1");
                    // Thread.Sleep(100);   // wait 100 ms for the FUNC to finish

                    tResponse = rTMCConn.RunCommand(GeneralFunctions.screenAllVials);    // function 36
                    Thread.Sleep(100);   // wait 100 ms for the program to finish to switch and thread sleep

                    //// calculate if syrige was replaced
                    //if (syringeInPlaceTB.Text == $"SYRINGE IN")                // syringe is in?
                    //{
                    //    if (syringeWasRemoved == true)
                    //    {
                    //        syringeWasReplaced = true;
                    //        syringeWasRemoved = false;
                    //    }
                    //}
                    //else   // no syringe
                    //{
                    //    syringeWasReplaced = false;
                    //    syringeWasRemoved = true;
                    //}

                    // display when no RUN the vials and bag in place
                    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vialsExist);  //GB_218
                    vialsExist = Convert.ToInt32(tResponse.tmcReply.value);
                    this.Invoke((MethodInvoker)delegate { vialsExistTB.Text = Convert.ToString(vialsExist, 2).PadLeft(7, '0'); });

                    if ((vialsExist & Bit_bag) == Bit_bag)  // bag is in?
                    {
                        this.Invoke((MethodInvoker)delegate { BagInPlaceTB.Text = "In Place"; });
                        //if (bagWasRemoved == true)
                        //{
                        //    bagWasReplaced = true;
                        //    bagWasRemoved = false;
                        //}
                    }
                    else   // no bag
                    {
                        this.Invoke((MethodInvoker)delegate { BagInPlaceTB.Text = "No BAG"; });
                        //bagWasReplaced = false;
                        //bagWasRemoved = true;
                    }
                    if ((vialsExist & Bit_vial1) == Bit_vial1)
                    {
                        this.Invoke((MethodInvoker)delegate { Vial1InPlaceTB.Text = "In Place"; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Vial1InPlaceTB.Text = ""; });
                    }
                    if ((vialsExist & Bit_vial2) == Bit_vial2)
                    {
                        this.Invoke((MethodInvoker)delegate { Vial2InPlaceTB.Text = "In Place"; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Vial2InPlaceTB.Text = ""; });
                    }
                    if ((vialsExist & Bit_vial3) == Bit_vial3)
                    {
                        this.Invoke((MethodInvoker)delegate { Vial3InPlaceTB.Text = "In Place"; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Vial3InPlaceTB.Text = ""; });
                    }
                    if ((vialsExist & Bit_vial4) == Bit_vial4)
                    {
                        this.Invoke((MethodInvoker)delegate { Vial4InPlaceTB.Text = "In Place"; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Vial4InPlaceTB.Text = ""; });
                    }
                    if ((vialsExist & Bit_vial5) == Bit_vial5)
                    {
                        this.Invoke((MethodInvoker)delegate { Vial5InPlaceTB.Text = "In Place"; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Vial5InPlaceTB.Text = ""; });
                    }
                    if ((vialsExist & Bit_vial6) == Bit_vial6)
                    {
                        this.Invoke((MethodInvoker)delegate { Vial6InPlaceTB.Text = "In Place"; });
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Vial6InPlaceTB.Text = ""; });
                    }
                }
            }
        }
        private void tstringToSGPtest()    // SGP commands
        {
            try
            {
                //if (tResponse.tmcReply == null)
                //{
                //    return;
                //}
                tString = Convert.ToString(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { SGPtestTB.Text = tString; });
            }
            catch (Exception e)
            {
                //logAndShow($"Error writing to 'for SGP cmd'");
                logAndShow(e.Message);
                return;
            }
        }
        private void tstringToRUNtest()    // RUN commands
        {
            try
            {
                tString = Convert.ToString(tResponse.tmcReply.value);
                this.Invoke((MethodInvoker)delegate { ReplyValueTB.Text = tString; });
                tString = Convert.ToString(tResponse.tmcReply.status);
                this.Invoke((MethodInvoker)delegate { replyStatusTB.Text = tString; });
            }
            catch (Exception e)
            {
                logAndShow(e.Message);
                return;
            }
        }
        private void tstringDiv1000()   // move command
        {
            tString = Convert.ToString(Convert.ToDouble(tResponse.tmcReply.value) / 1000);
            this.Invoke((MethodInvoker)delegate { movingTB.Text = tString; });
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        //=============
        // E R R O R S
        //=============
        private void ErrorsLog()
        {
            stopOnError = true;  // ok to run cycle *** stop running;

            if (errorsSyringeBag > 0)
            {
                if (errorsSyringeBag == 32)
                {
                    logAndShow
                    (
                      $"An Error occured in the robot:\r" +
                      $"==========================\r\r" +
                      $"\t machine was aborted \r\r" +
                      $"\t run HOME  \r" +
                      $"========================== \r"
                      // $"\t 2- click RUN \r"
                    );
                }
                else
                {
                    logAndShow
                    (
                      $"An Error occured in the robot:\r" +
                      $"====================\r\r" +
                      $"Syringe:  {errorsSyringeBag}\r" +
                      $"\tbag Is Missing =\t       1\r" +
                      $"\tsyringe Popped Out =\t       2\r" +
                      $"\tvolume Exceeds Bag size =\t      4\r" +
                      $"\tsyringe Is In =\t      8\r" +
                      $"\tsyringe Missing =\t      16\r" +
                      $"\tmachine Aborted =\t      32\r" +
                      $"_______________________ \r"
                    );
                }
            }
            // *** MOTORS Errors ***
            if (errorsM_Vertical != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Vertical motor:\t{errorsM_Vertical}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsM_Linear != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Linear motor:\t{errorsM_Linear}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsM_Arm != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Arm motor:\t{errorsM_Arm}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsM_Piston != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Piston motor:\t{errorsM_Piston}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsM_HeadRotate != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Head rotating motor:\t{errorsM_HeadRotate}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsM_Dispose != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Dispose motor:\t{errorsM_HeadRotate}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsM_CapHolder != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"Cap Holder motor:\t{errorsM_HeadRotate}   TimeOut\r\r" +
                    $"_______________________ \r"
                );
            }
            // *** VIALs errors ***
            if (errors_Vial_1 != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"errors Vial_1:\t{errors_Vial_1} \r\r" +
                    $"\tVial Too Small =\t 1\r" +
                    $"\tVial Missing =\t 2\r" +
                    $"\tVial PoppedOut =\t 4\r" +
                    $"_______________________ \r"
                );
            }
            if (errors_Vial_2 != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"errors Vial_2:\t{errors_Vial_2} \r\r" +
                    $"\tVial TooSmall =\t 1\r" +
                    $"\tVial Missing =\t 2\r" +
                    $"\tVial PoppedOut =\t 4\r" +
                    $"_______________________ \r"
                );
            }
            if (errors_Vial_3 != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"errors Vial_3:\t{errors_Vial_3} \r\r" +
                    $"\tVial Too Small =\t 1\r" +
                    $"\tVial Missing =\t 2\r" +
                    $"\tVial Popped Out =\t 4\r" +
                    $"_______________________ \r"
                );
            }
            if (errors_Vial_4 != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"errors Vial_4:\t{errors_Vial_4} \r\r" +
                    $"\tVial Too Small =\t 1\r" +
                    $"\tVial Missing =\t 2\r" +
                    $"\tVial Popped Out =\t 4\r" +
                    $"_______________________ \r"
                );
            }
            if (errors_Vial_5 != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"errors Vial_5:\t{errors_Vial_5} \r\r" +
                    $"\tVial Too Small =\t 1\r" +
                    $"\tVial Missing =\t 2\r" +
                    $"\tVial Popped Out =\t 4\r" +
                    $"_______________________ \r"
                );
            }
            if (errors_Vial_6 != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"errors Vial_6:\t{errors_Vial_6} \r\r" +
                    $"\tVial Too Small =\t 1\r" +
                    $"\tVial Missing =\t 2\r" +
                    $"\tVial Popped Out =\t 4\r" +
                    $"_______________________ \r"
                );
            }

            // *** FIND HOME Errors ***
            if (errors_findHome != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"wrong PC cmd:\t{errors_findHome}\r\r" +
                    $"\tsyringe Is In while Find Home =\t 1\r" +
                    $"\texpecting WAITING_DISPENSE =\t 4\r" +
                    $"_______________________ \r"
                );
            }
            if (errorsWrongPCcmd != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"wrong PC cmd:\t{errorsWrongPCcmd}\r\r" +
                    $"\texpecting GP58_10_OR_30 =\t 1\r" +
                    $"\texpecting WAITING_DISPENSE =\t 2\r" +
                    $"\tvibrate Paremeter Error =\t 8\r" +
                    $"_______________________ \r"
                );
            }
            if (special_Error != 0)
            {
                logAndShow
                (
                $"An Error occured in the robot:\r" +
                $"====================\r\r" +
                    $"special Error:\t{special_Error}\r\r" +
                    $"\tSliding Door Is Open =\t 1\r" +
                    $"\tDrawer Overflow =\t 2\r" +
                    $"\tNo vials =\t\t 4\r" +
                    $"\tdrawer Is Open =\t\t 8\r" +
                    $"\tvial Not Defined =\t\t16\r" +
                    $"_______________________ \r"
                );
            }
        }
        //======================
        // write R U N results
        //======================
        private void WriteCMrun(bool askOverWrite)   // *****  create CM file for previous runs   ************
        {
            int i;
            string strWithdraw;
            string vialSize;
            string fileName;
            string fillSize;
            Control dd = new Control();
            Control ff = new Control();
            Control gg = new Control();

            if (!readyForNewCommand)
            {
                //                logAndShow("The robot is busy");
                return;
            }

            runDay = DateTime.Now.ToString("yyyy-MM-dd HH-mm");
            fileName = cmPath + cmRUNpath + runDay + cmFileNameEnd;

            if (File.Exists(fileName) && askOverWrite)
            {
                DialogResult dr2 = MessageBox.Show($"The file: \'{fileName}\'exists. \n" +
                                                   "Do you want to over Write it?",
                                                   "overwrite existing file?", MessageBoxButtons.YesNo);
                if (dr2 == DialogResult.No)
                {
                    return;  // exit
                }
            }
            string toWrite = " RescueDose CM bag RUN report\n"
                           + "\n start Date: " + runDay
                           + "\n user: " + username + "\n\n"
                           + "==============================\n"
                           + "  Bag Size    Bag Volume [mL] \n"
												 
                           + $"   {BagSizeMlTB.Text:10} {mLinBagTB.Text,24} \n\n"
															 
                           + "  Vial#   Vial size  volume [mL] \n"
                           + "==============================\n\n";
            File.WriteAllText(fileName, toWrite);
            this.Invoke((MethodInvoker)delegate { mLinBagTB.Text = ""; });

            for (i = 1; i <= 6; i++)                                // go over 18 bottles
            {
                vialSize = $"Vial{i:D1}SizeMlTB";                           // 1 2 3 volume column
                foreach (Control d in RunParametersTLP.Controls)
                {
                    if (d is TextBox && string.Equals(vialSize, d.Name))
                    {
                        dd = d;
                    }
                }
				   
                strWithdraw = $"Vial{i:D1}WithdrawMlTB";                           // 1 2 3 volume column
                foreach (Control f in RunParametersTLP.Controls)
                {
                    if (f is TextBox && string.Equals(strWithdraw, f.Name))
                    {
                        ff = f;
                    }
                }
                fillSize = $"Vial{i:D1}FillMlTB";
                foreach (Control g in RunParametersTLP.Controls)
                {
                    if (g is TextBox && string.Equals(fillSize, g.Name))
                    {
                        gg = g;
                    }
                }

                if ((dd.Text != "0" && dd.Text != "") || ff.Text != "")
                {
                    File.AppendAllText(fileName, $" {i,8} {dd.Text,14} {ff.Text,12}  {gg.Text,12} \n");
                }
            }
            runDay = DateTime.Now.ToString("yyyy-MM-dd HH-mm");
            File.AppendAllText(fileName, $"\n elapsed time:  {elapsedTime} seconds \n");
        }

        // =========================
        //      write Setup file    
        // =========================
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            string vialSize;
            string strWithdraw;
            string fillSize;
            string fileNameLast = setupPath + lastSetupName;
            string fileNameDate = setupPath + DateTime.Now.ToString("yyyy-MM-dd HH-mm ") + SetupName;
            uint i;
            Control dd = new Control();
            Control ff = new Control();
            Control gg = new Control();

            File.WriteAllText(cmPath + fileNameLast,
                            " Last Setup file,  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\n");

            File.WriteAllText(cmPath + fileNameDate,
                            " Setup file,  " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") + "\n");

            string toWrite = " User: " + username + "\n\n"
                            + "==============================\n"
                            + "  Bag Size[mL]     \n"
                            + $"{BagSizeMlTB.Text} \n\n"
                            + "  # Vial withdraw fill [mL] \n"
                            + "==============================\n";

            dd.Text = "";
            ff.Text = "";
            gg.Text = "";

            for (i = 1; i <= 6; i++)                                // go over 18 bottles
            {
                vialSize = $"Vial{i:D1}SizeMlTB";                           // 1 2 3 volume column
                strWithdraw = $"Vial{i:D1}WithdrawMlTB";                           // 1 2 3 volume column
                fillSize = $"Vial{i:D1}FillMlTB";
                foreach (Control d in RunParametersTLP.Controls)
                {
                    if (d is TextBox && string.Equals(vialSize, d.Name))
                    {
                        if (d.Text == "")
                        {
                            d.Text = "0";
                        }
                        dd = d;
                    }
																				  
                    else if (d is TextBox && string.Equals(strWithdraw, d.Name))
                    {
                        if (d.Text == "")
                        {
                            d.Text = "0";
                        }
                        ff = d;
                    }
                    else if (d is TextBox && string.Equals(fillSize, d.Name))
                    {
                        if (d.Text == "")
                        {
                            d.Text = "0";
                        }
                        gg = d;
                    }
                }
                if ((dd.Text != "0" && dd.Text != "") || ff.Text != "")
                {
                    toWrite += $"{i} {dd.Text} {ff.Text} {gg.Text} \n";
                }
            }
            // write vibration data
            toWrite += $"\n Vibration parameters:\ntime of vial 4, time of vials 56, HZ, Strength\n"
                    + "=======================================\n";
            toWrite += $"{vibrationTime4TB.Text} {vibrationTime56TB.Text} {vibrationHzTB.Text} {vibrationStrengthTB.Text}\n";


            File.AppendAllText(cmPath + fileNameLast, toWrite);
            File.AppendAllText(cmPath + fileNameDate, toWrite);

            writeLogFile(fileNameLast + "  written ");
            writeLogFile(fileNameDate + "  written \n");
            logAndShow("Setup files saved");
        }

        // ======================
        //      load Setup button
        // ======================
        private void loadSetupsBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = cmPath + setupPath;
            openFileDialog1.Filter = "text files (*Setup.txt)|*Setup.txt";
            DialogResult dr = openFileDialog1.ShowDialog();  // choose the directory from file list 
            if (dr == DialogResult.OK)
            {
                string setupFile = openFileDialog1.FileName;
                loadSetupFileCore(setupFile);
            }
        }

        // ===========================================================================================

        private void loadSetupFileCore(string FileToLoad)
        {
            int i;
            string vialSize;
            string strWithdraw;
            string fillSize;
            string line;
            string[] result; // = new string[16];

            if (!File.Exists(FileToLoad))
            {
                return;
            }

            StreamReader sr = new StreamReader(FileToLoad);

            for (i = 0; i < 5; i++)  // wait for first lines
            {
                sr.ReadLine();
            }

            // read bag
            line = sr.ReadLine();

								 
            result = line.Split(' ');          // BagSize = result[0], BagVolume = result[1]
								 
									 
            BagSizeMlTB.Text = result[0];

            for (i = 0; i < 3; i++)  // wait for vial lines
            {
                line = sr.ReadLine();
                result = line.Split(' ');
            }

            // read vials
            for (i = 1; i <= 6; i++)
            {
                line = sr.ReadLine();
                result = line.Split(' ');          // vial = result[0], sizeOfVial = result[1], volume -= result[2]

                vialSize = $"Vial{i:D1}SizeMlTB";                              // 1 2 3 volume column
                strWithdraw = $"Vial{i:D1}WithdrawMlTB";                         // 1 2 3 volume column
                fillSize = $"Vial{i:D1}FillMlTB";
                // now insert into table
                foreach (Control c in RunParametersTLP.Controls)              // find the next ADD
                {
                    if (c is TextBox && string.Equals(vialSize, c.Name))       // is volume?
                    {
                        c.Text = result[1];
                    }
                    if (c is TextBox && string.Equals(strWithdraw, c.Name))    // is volume?
                    {
                        c.Text = result[2];
                    }
                    if (c is TextBox && string.Equals(fillSize, c.Name))    // is volume?
                    {
                        c.Text = result[3];
											 
                    }
                }
            }

            // read vibration
            for (i = 0; i < 4; i++)  // wait for vibration lines
            {
                line = sr.ReadLine();
                result = line.Split(' '); //
            }
            line = sr.ReadLine();
            result = line.Split(' ');  // time of 4 = result[0], time of 5 = result[1], HZ = result[2], strength = result[3]
            vibrationTime4TB.Text = result[0];
            vibrationTime56TB.Text = result[1];
            vibrationHzTB.Text = result[2];
            vibrationStrengthTB.Text = result[3];

            sr.Close();
        }


        // ===========================================================================================
        //                                   R U N   button
        // ===========================================================================================
        //
        // =====================================================================================================
        private void RunBtn_Click(object sender, EventArgs e)    // clicking run will start a thread for run
                                                                 // =========================================
        {
            string BagSizeMicroL;

            string Vial1SizeMicroL;
            string Vial2SizeMicroL;
            string Vial3SizeMicroL;
            string Vial4SizeMicroL;
            string Vial5SizeMicroL;
            string Vial6SizeMicroL;

            string Vial4FillMicroL;
            string Vial5FillMicroL;
            string Vial6FillMicroL;

            string Vial1WithdrawMicroL;
            string Vial2WithdrawMicroL;
            string Vial3WithdrawMicroL;
            string Vial4WithdrawMicroL;
            string Vial5WithdrawMicroL;
            string Vial6WithdrawMicroL;

            int intVial1WithdrawMicroL;
            int intVial2WithdrawMicroL;
            int intVial3WithdrawMicroL;
            int intVial4WithdrawMicroL;
            int intVial5WithdrawMicroL;
            int intVial6WithdrawMicroL;

            int uLinVial1;
            int uLinVial2;
            int uLinVial3;
            int uLinVial4;
            int uLinVial5;
            int uLinVial6;

            if (BagSizeMlTB.Text == "0")
            {
                logAndShow("Bag size cannot be 0");
                return;
            }

            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                logAndShow("The robot is busy or no connected.");
                return;
            }
            if (RunInProcess)
            {
                logAndShow("The PC did not finish the RUN");
                return;
            }

            BagSizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(BagSizeMlTB.Text) * 1000));

            Vial1SizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial1SizeMlTB.Text) * 1000));
            Vial2SizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial2SizeMlTB.Text) * 1000));
            Vial3SizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial3SizeMlTB.Text) * 1000));
            Vial4SizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial4SizeMlTB.Text) * 1000));
            Vial5SizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial5SizeMlTB.Text) * 1000));
            Vial6SizeMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial6SizeMlTB.Text) * 1000));

            Vial4FillMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial4FillMlTB.Text) * 1000));
            Vial5FillMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial5FillMlTB.Text) * 1000));
            Vial6FillMicroL = Convert.ToString(Convert.ToInt32(Convert.ToDouble(Vial6FillMlTB.Text) * 1000));

            uLinVial1 = Convert.ToInt32(Convert.ToDouble(mLinVial1TB.Text) * 1000);
            uLinVial2 = Convert.ToInt32(Convert.ToDouble(mLinVial2TB.Text) * 1000);
            uLinVial3 = Convert.ToInt32(Convert.ToDouble(mLinVial3TB.Text) * 1000);
            uLinVial4 = Convert.ToInt32(Convert.ToDouble(mLinVial4TB.Text) * 1000);
            uLinVial5 = Convert.ToInt32(Convert.ToDouble(mLinVial5TB.Text) * 1000);
            uLinVial6 = Convert.ToInt32(Convert.ToDouble(mLinVial6TB.Text) * 1000);

            intVial1WithdrawMicroL = Convert.ToInt32(Convert.ToDouble(Vial1WithdrawMlTB.Text) * 1000);
            intVial2WithdrawMicroL = Convert.ToInt32(Convert.ToDouble(Vial2WithdrawMlTB.Text) * 1000);
            intVial3WithdrawMicroL = Convert.ToInt32(Convert.ToDouble(Vial3WithdrawMlTB.Text) * 1000);
            intVial4WithdrawMicroL = Convert.ToInt32(Convert.ToDouble(Vial4WithdrawMlTB.Text) * 1000);
            intVial5WithdrawMicroL = Convert.ToInt32(Convert.ToDouble(Vial5WithdrawMlTB.Text) * 1000);
            intVial6WithdrawMicroL = Convert.ToInt32(Convert.ToDouble(Vial6WithdrawMlTB.Text) * 1000);


            if (uLinVial1 < intVial1WithdrawMicroL)
            {
                logAndShow("The request to withdraw is more than the volume left in Vial 1 ");
                return;
            }

            if (uLinVial2 < intVial2WithdrawMicroL)
            {
                logAndShow("The request to withdraw is more than the volume left in Vial 2 ");
                return;
            }

            if (uLinVial3 < intVial3WithdrawMicroL)
            {
                logAndShow("The request to withdraw is more than the volume left in Vial 3 ");
                return;
            }

            if (uLinVial4 < intVial4WithdrawMicroL)
            {
                logAndShow("The request to withdraw is more than the volume left in Vial 4 ");
                return;
            }

            if (uLinVial5 < intVial5WithdrawMicroL)
            {
                logAndShow("The request to withdraw is more than the volume left in Vial 5 ");
                return;
            }

            if (uLinVial6 < intVial6WithdrawMicroL)
            {
                logAndShow("The request to withdraw is more than the volume left in Vial 6 ");
                return;
            }

            // open run file

            startProcessDate = DateTime.Now;
            runDay = DateTime.Now.ToString("yyyy-MM-dd HH-mm");

            cmFile = cmRUNpath + runDay + cmFileNameEnd;                // this is the run file name
                                                                        //string toWrite = " RescueDose CM bag RUN report\n"
                                                                        //               + "\n Date: " + runDay
                                                                        //               + "\n user: " + username + "\n\n"
                                                                        //               + "====================================================\n"
                                                                        //               + "       Vial:                     Vibrate \n"
                                                                        //               + " size [ml]  withdraw [ml]     HZ [1/s]  strength[%]\n"
                                                                        //               + "====================================================\n\n";
                                                                        //File.WriteAllText(cmPath + cmFile, toWrite);

            ///WriteCMrun(false);      // *****  create file for future loading   ****

            // check if "asVialSize", if yes, copy vial size //** "asVialSize" is not in use any more **//

            if (Vial1WithdrawMlTB.Text == asVialSize)
            {
                Vial1WithdrawMicroL = Vial1SizeMicroL;
                Vial1WithdrawMlTB.Text = Vial1SizeMlTB.Text;
            }
				  
            else { Vial1WithdrawMicroL = Convert.ToString(intVial1WithdrawMicroL); }

            if (Vial2WithdrawMlTB.Text == asVialSize)
            {
                Vial2WithdrawMicroL = Vial2SizeMicroL;
                Vial2WithdrawMlTB.Text = Vial2SizeMlTB.Text;
            }
				   
            else { Vial2WithdrawMicroL = Convert.ToString(intVial2WithdrawMicroL); }

            if (Vial3WithdrawMlTB.Text == asVialSize)
            {
                Vial3WithdrawMicroL = Vial3SizeMicroL;
                Vial3WithdrawMlTB.Text = Vial3SizeMlTB.Text;
            }
				   
            else { Vial3WithdrawMicroL = Convert.ToString(intVial3WithdrawMicroL); }

            if (Vial4WithdrawMlTB.Text == asVialSize)
            {
                Vial4WithdrawMicroL = Vial4SizeMicroL;
                Vial4WithdrawMlTB.Text = Vial4SizeMlTB.Text;
            }
				   
            else { Vial4WithdrawMicroL = Convert.ToString(intVial4WithdrawMicroL); }

            if (Vial5WithdrawMlTB.Text == asVialSize)
            {
                Vial5WithdrawMicroL = Vial5SizeMicroL;
                Vial5WithdrawMlTB.Text = Vial5SizeMlTB.Text;
            }
				   
            else { Vial5WithdrawMicroL = Convert.ToString(intVial5WithdrawMicroL); }

            if (Vial6WithdrawMlTB.Text == asVialSize)
            {
                Vial6WithdrawMicroL = Vial6SizeMicroL;
                Vial6WithdrawMlTB.Text = Vial6SizeMlTB.Text;
            }
				   
            else { Vial6WithdrawMicroL = Convert.ToString(intVial6WithdrawMicroL); }

            //  ************ send RUN parameters to board *******************

            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_BagSize_microL, BagSizeMicroL);

            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vialSize_microL_1, Vial1SizeMicroL);
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vialSize_microL_2, Vial2SizeMicroL);
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vialSize_microL_3, Vial3SizeMicroL);
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vialSize_microL_4, Vial4SizeMicroL);
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vialSize_microL_5, Vial5SizeMicroL);
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vialSize_microL_6, Vial6SizeMicroL);

            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoFill_4, Vial4FillMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoFill_5, Vial5FillMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoFill_6, Vial6FillMicroL);

            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoWithdraw_1, Vial1WithdrawMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoWithdraw_2, Vial2WithdrawMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoWithdraw_3, Vial3WithdrawMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoWithdraw_4, Vial4WithdrawMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoWithdraw_5, Vial5WithdrawMicroL);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLtoWithdraw_6, Vial6WithdrawMicroL);

            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_4, vibrationTime4TB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_56, vibrationTime56TB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationHz, vibrationHzTB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrStrengthPercentCalc, vibrationStrengthTB.Text);
            tstringToSGPtest();
            stopOnError = false;    // ok to run cycle
            aborted = false;

            Thread runThread = new Thread(RunBtn_Click_Impl);     // start the thread for "run"
            runThread.Start();                                    // runThread.Start();
        }
        // ===========================================================================================
        //                                   R U N   Thread
        // ===========================================================================================
        //
        // this is the method to run the bag filling according to the textBox table
        // the following thread will run the RUN button request
        private void RunBtn_Click_Impl()
        {
            Control dd = new Control();
            Control ee = new Control();

            if (aborted) return;
            /*
                        if (!bagWasReplaced)
                        {
                            logAndShow("Please replace a new bag");
                            goto exit;  // exit
                        }

                        if (!syringeWasReplaced)
                        {
                            logAndShow("Please replace a new syring");
                            goto exit;  // exit
                        }
            if (readyForNewCommand)
            {
           *** maybe put here ***
            CSUB    VERIFY_READY_DRAW          // check if syringe, vial are loaded // keeps STATE WAITING_DISPENSE for easy recovery
            }
            */
            RunInProcess = true;                                  // eliminate re-entrance

            this.Invoke((MethodInvoker)delegate { RunParametersTLP.Enabled = false; });
            this.Invoke((MethodInvoker)delegate { calibrateTLP.Enabled = false; });

            if (!readyForNewCommand)
            {
                logAndShow("The Robot is busy, wait and try again");
                goto exit;  // exit
            }
            if (rTMCConn.TrinamicAborted())
            {
                logAndShow(" Robot aborted, please intitiate HOME");
                goto exit;  // exit
            }

            tResponse = rTMCConn.RunCommand(GeneralFunctions.DRAW_DOSE);   // run draw process
            tstringToRUNtest();

            Thread.Sleep(300);  // wait before polling the "ready for new command

            while (!readyForNewCommand)            // wait for the end of "MULTI"
            {
                //blink RunBtn
                RunBtn.BackColor = Color.Bisque;
                Thread.Sleep(300);
                RunBtn.BackColor = Color.LawnGreen;
                Thread.Sleep(300);
                if (stopOnError || aborted) // Stop looping
                {
                    //ErrorsLog();
                    goto exit;  // exit
                }
            }
            // all vials are done
            Thread.Sleep(300);

            elapsedTime = ((DateTime.Now - startProcessDate).TotalSeconds).ToString("0.00", CultureInfo.InvariantCulture);
            WriteCMrun(false);      // *****  create RUN results file   ****
            string fileName = cmPath + cmRUNpath + runDay + cmFileNameEnd;
            //File.AppendAllText(fileName, $" {"\n The RUN process ended at:"} {DateTime.Now.ToString("yyyy-MM-dd HH-mm")} \n");

            logAndShow("The RUN process is done \n" +
                "         process time: " + elapsedTime);
        exit:
            RunBtn.BackColor = Color.LawnGreen;
            //bagWasReplaced = false;
            //bagWasRemoved = false;
            //syringeWasReplaced = false;
            //syringeWasRemoved = false;
            this.Invoke((MethodInvoker)delegate { RunParametersTLP.Enabled = true; });
            this.Invoke((MethodInvoker)delegate { calibrateTLP.Enabled = true; });
            RunInProcess = false;
        }
        // =============================================================================
        // =============================================================================

        // *** Vertical motor control ***
        private void VerticalUpBtn_Click(object sender, EventArgs e)
        {
            VerticalGoUp();
        }
        private void VerticalUpArrowPnl_Click(object sender, EventArgs e)
        {
            VerticalGoUp();
        }

        private void VerticalDownBtn_Click(object sender, EventArgs e)
        {
            VerticalGoDown();
        }
        private void VerticalDownArrowPnl_Click(object sender, EventArgs e)
        {
            VerticalGoDown();
        }

        public void VerticalGoUp()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Backward);   // Backward=-1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.VerticalManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }
        public void VerticalGoDown()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Forward);   // forward=1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.VerticalManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        // *** Linear motor control ***
        private void LinearLeftBtn_Click(object sender, EventArgs e)
        {
            LinearGoLeft();
        }
        private void LinearLeftArrowPnl_Click(object sender, EventArgs e)
        {
            LinearGoLeft();
        }

        private void LinearRightBtn_Click(object sender, EventArgs e)
        {
            LinearGoRight();
        }
        private void LinearRightArrowPnl_Click(object sender, EventArgs e)
        {
            LinearGoRight();
        }

        public void LinearGoLeft()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Backward);   // Backward=-1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);    // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.LinearMotorManual);
                tstringToRUNtest();   // display on "for RUN cmd"
            }
        }
        public void LinearGoRight()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Forward);   // forward=1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.LinearMotorManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        // *** Arm motor control ***

        private void ArmDownBtn_Click(object sender, EventArgs e)
        {
            ArmGoDown();
        }
        private void ArmDownArrowPnl_Click(object sender, EventArgs e)
        {
            ArmGoDown();
        }

        private void ArmUpBtn_Click(object sender, EventArgs e)
        {
            ArmGoUp();
        }
        private void ArmUpArrowPnl_Click(object sender, EventArgs e)
        {
            ArmGoUp();
        }

        public void ArmGoDown()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Forward);   // Forward = "1"
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.armMotorManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        public void ArmGoUp()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Backward);   // Backward = "-1"
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.armMotorManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        // *** Piston motor control ***
        private void PistonInBtn_Click(object sender, EventArgs e)
        {
            PistonGoIn();
        }
        private void PistonInArrowPnl_Click(object sender, EventArgs e)
        {
            PistonGoIn();
        }

        private void PistonOutBtn_Click(object sender, EventArgs e)
        {
            PistonGoOut();
        }
        private void PistonOutArrowPnl_Click(object sender, EventArgs e)
        {
            PistonGoOut();
        }

        public void PistonGoOut()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Forward);   // Forward=1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.PistonManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }
        public void PistonGoIn()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Backward);   // Backward=-1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.PistonManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        // *** Head rotate motor control ***
        private void SyringeUpBtn_Click(object sender, EventArgs e)
        {
            HeadRotateUp();
        }
        private void SyringeDownArrowPnl_Click(object sender, EventArgs e)
        {
            HeadRotateUp();
        }

        private void SyringeDownBtn_Click(object sender, EventArgs e)
        {
            HeadRotateDown();
        }
        private void SyringeUpArrowPnl_Click(object sender, EventArgs e)
        {
            HeadRotateDown();
        }

        public void HeadRotateDown()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Forward);   // Forward=1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.RotationManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }
        public void HeadRotateUp()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Backward);   // Backward=-1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.RotationManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        // *** Dispose motor control ***
        private void DisposeBtn_Click(object sender, EventArgs e)
        {
            DisposeGoOut();
        }
        private void DisposeArrowPnl_Click(object sender, EventArgs e)
        {
            DisposeGoOut();
        }

        private void DiposeBackBtn_Click(object sender, EventArgs e)
        {
            DisposeGoIn();
        }
        private void DisposeBackArrowPnl_Click(object sender, EventArgs e)
        {
            DisposeGoIn();
        }

        public void DisposeGoOut()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Forward);   // Forward=1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.DisposeManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }
        public void DisposeGoIn()
        {
            //if (readyForNewCommand)
            {
                setManualDistance();
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_moveManualBackwards, Values.Backward);   // Backward=-1
                tstringToSGPtest();   // display on "for SGP cmd"
                Thread.Sleep(300);  // wait before moving
                tResponse = rTMCConn.RunCommand(GeneralFunctions.DisposeManual);
                tstringToRUNtest();    // display on "for RUN cmd"
            }
        }

        // *** Cap Holder motor control ***
        private void HoldCapBtn_Click(object sender, EventArgs e)
        {
            CapHolderHold();
        }
        private void CapHoldArowPnl_Click(object sender, EventArgs e)
        {
            CapHolderHold();
        }

        private void ReleaseCapBtn_Click(object sender, EventArgs e)
        {
            CapHolderRelease();
        }
        private void CapReleaseArowPnl_Click(object sender, EventArgs e)
        {
            CapHolderRelease();
        }

        public void CapHolderHold()
        {
            //if (readyForNewCommand)
            {
                tResponse = rTMCConn.RunCommand(GeneralFunctions.holdCap);
                tstringToRUNtest();
                setManualDistance();
            }
        }

        public void CapHolderRelease()
        {
            //if (readyForNewCommand)
            {
                tResponse = rTMCConn.RunCommand(GeneralFunctions.homeCapHolderMotor);
                tstringToRUNtest();    // display on "for RUN cmd"
                //setManualDistance();
            }
        }

        // ******************
        //  set jog distance
        // ******************

        private void Jog20RB_Click(object sender, EventArgs e)
        {
            // if (Jog20RB.Checked == false) return;
            goDistanceTB.Text = "20";
            setManualDistance();
        }

        private void Jog5RB_Click(object sender, EventArgs e)
        {
            // if (Jog5RB.Checked == false) return;
            goDistanceTB.Text = "5";
            setManualDistance();
        }

        private void Jog2RB_Click(object sender, EventArgs e)
        {
            // if (Jog2RB.Checked == false) return;
            goDistanceTB.Text = "2";
            setManualDistance();
        }

        private void Jog1RB_Click(object sender, EventArgs e)
        {
            // if (Jog1RB.Checked == false) return;
            goDistanceTB.Text = "1";
            setManualDistance();
        }

        private void Jog04RB_Click(object sender, EventArgs e)
        {
            // if (Jog04RB.Checked == false) return;
            goDistanceTB.Text = "0.4";
            setManualDistance();
        }

        private void Jog02RB_Click(object sender, EventArgs e)
        {
            // if (Jog02RB.Checked == false) return;
            goDistanceTB.Text = "0.2";
            setManualDistance();
        }

        private void Jog01RB_Click(object sender, EventArgs e)
        {
            // if (Jog01RB.Checked == false) return;
            goDistanceTB.Text = "0.1";
            setManualDistance();
        }
        // ************************
        // ** set distance to go **
        // ************************

        private void goDistanceTB_Leave(object sender, EventArgs e)
        {
            setManualDistance();
        }

        private void goDistanceTB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                setManualDistance();
            }
        }
        private void setManualDistance()
        {
            if (rgfloat.Match(goDistanceTB.Text).Success)        // floating point number
            {
                goDistance_um = Convert.ToString(Convert.ToInt32(Convert.ToDouble(goDistanceTB.Text) * 1000));
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_UnitsToMoveManual, goDistance_um);
                refreshParams();
            }
            else
            {
                //logAndShow($"A non-number value for the GB {setGBnumberTB.Text}");
                goDistanceTB.Text = "0";
            }
        }

        // ***********
        //  GOTO HOME
        // ***********

        private void RunHomeBtn_Click(object sender, EventArgs e)
        {
            goHome();
        }
        private void calibrateHOMEbtn_Click(object sender, EventArgs e)
        {
            goHome();
        }
        public void goHome()
        {
            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                logAndShow("The robot is busy or not connected.");
                return;
            }
            Thread goHomeThread = new Thread(goHome_Impl);        // start the thread for "run"
            aborted = false;
            goHomeThread.Start();
        }
        // ===========================================================================================
        //                                GO HOME   Thread
        // ===========================================================================================
        //
        private void goHome_Impl()
        {
            if (aborted) return;

            RunInProcess = true;                                  // eliminate re-entrance
            tResponse = rTMCConn.RunCommand(GeneralFunctions.INIT_CM);
            tstringToRUNtest();    // display on "for RUN cmd"
            Thread.Sleep(600);
            while (!homingDone && !aborted && !anyError)  // wait for the end of "HOME" //                 if (!anyErrorGotTrue && anyError)    // will happen for one cycle after anyError was set
            {
                //blink button  
                calibrateHOMEbtn.BackColor = Color.AntiqueWhite;
                RunHomeBtn.BackColor = Color.AntiqueWhite;
                Thread.Sleep(300);
                calibrateHOMEbtn.BackColor = Color.Chocolate;
                RunHomeBtn.BackColor = Color.Chocolate;
                Thread.Sleep(300);
            }
            calibrateHOMEbtn.BackColor = Color.Chocolate;
            RunHomeBtn.BackColor = Color.Chocolate;
            Thread.Sleep(300);           // wait before polling the "ready for new command
            setManualDistance();
            if (!aborted && !anyError)
            {
                logAndShow("Go home done.");
            }

            this.Invoke((MethodInvoker)delegate { RunParametersTLP.Enabled = true; });
            this.Invoke((MethodInvoker)delegate { calibrateTLP.Enabled = true; });
            RunInProcess = false;
        }
        // ===========================================================================================

        private void CalibrateAbortBtn_Click(object sender, EventArgs e)
        {
            AbortCM();
        }
        private void RunAbortBtn_Click(object sender, EventArgs e)
        {
            AbortCM();
        }
        private void AbortCM()
        {
            aborted = true;
            tResponse = rTMCConn.RunCommand(GeneralFunctions.ABORT);
            tstringToRUNtest();    // display on "for RUN cmd"
            Thread.Sleep(300);           // wait before polling the "ready for new command
            tResponse = rTMCConn.RunCommand(GeneralFunctions.FIRST_RUN);
            tstringToRUNtest();    // display on "for RUN cmd"
                                   //logAndShow("Aborted. Run HOME");
                                   // Environment.Exit(0);
            RunInProcess = false;
        }

        private void cmTC_KeyDown(object sender, KeyEventArgs e)
        { //// temporaraly commented out instead of deleting  -  not needed keyboard shortcuts for now ...
            //if (username == "") { return; }
            //if (isAdministrator == false) { return; }
            //if (currentTAB <= 2) { return; }               // only for calibrate OR setup

            ///*MessageBox.Show(
            //            ""
            //            + " KeyCode =  " + e.KeyCode.ToString()
            //            + "\r value=   " + e.KeyValue
            //            + "\r control= " + e.Control
            //            + "\r Alt=     " + e.Alt);
            //*/
            //switch (Convert.ToInt32(e.KeyCode))            // move motors using keyboard
            //{
            //    case (char)221: LinearGoRight(); break;    // "]"
            //    case (char)219: LinearGoLeft(); break;     // "["
            //    case (char)222: HeadRotateDown(); break;   // '"'
            //    case (char)187: HeadRotateUp(); break;     // "+="
            //    case (char)109: ArmGoDown(); break;        // "-" on key pad
            //    case (char)107: ArmGoUp(); break;          // "+" on key pad
            //    case (char)33: VerticalGoUp(); break;      // "page Up"
            //    case (char)34: VerticalGoDown(); break;    // "page down"
            //    case (char)36: PistonGoIn(); break;        // "Home"
            //    case (char)35: PistonGoOut(); break;       // "End"
            //}

        }

        //private void cmTC_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (username == "") { return; }
        //    if (isAdministrator == false) { return; }
        //    if (currentTAB <= 2) { return; }               // only for calibrate OR setup

        //    /*MessageBox.Show(
        //                ""
        //                + " KeyCode =  " + e.KeyCode.ToString()
        //                + "\r value=   " + e.KeyValue
        //                + "\r control= " + e.Control
        //                + "\r Alt=     " + e.Alt);
        //    */
        //    switch (Convert.ToInt32(e.KeyCode))            // move motors using keyboard
        //    {
        //        case (char)221: LinearGoRight(); break;    // "]"
        //        case (char)219: LinearGoLeft(); break;     // "["
        //        case (char)222: HeadRotateDown(); break;   // '"'
        //        case (char)187: HeadRotateUp(); break;     // "+="
        //        case (char)109: ArmGoDown(); break;        // "-" on key pad
        //        case (char)107: ArmGoUp(); break;          // "+" on key pad
        //        case (char)33: VerticalGoUp(); break;      // "page Up"
        //        case (char)34: VerticalGoDown(); break;    // "page down"
        //        case (char)36: PistonGoIn(); break;        // "Home"
        //        case (char)35: PistonGoOut(); break;       // "End"
        //    }
        //}

        // ***** set the tabs entered a number *****
        private void adminTP_Enter(object sender, EventArgs e)
        {
            currentTAB = 1;
        }
        private void RunTP_Enter(object sender, EventArgs e)
        {
            currentTAB = 2;
            //refreshParams();
        }
        private void calibrateTP_Enter(object sender, EventArgs e)
        {
            currentTAB = 3;
            setManualDistance();
            //refreshParams();
            // set initial distancce for calibration
            goDistanceTB.Text = "3";
            goDistance_um = Convert.ToString(Convert.ToInt32(Convert.ToDouble(goDistanceTB.Text) * 1000));
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_UnitsToMoveManual, goDistance_um);
        }
        private void SetupsTP_Enter(object sender, EventArgs e)
        {
            Boolean showOverrride;
            Boolean disposeYN;
            Boolean skipVial456;
            Boolean skipBag;

            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                return;
            }
            currentTAB = 4;
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_ShowOverride);
            showOverrride = Convert.ToBoolean(tResponse.tmcReply.value);
            if (showOverrride)
            {
                showOverideRB.Checked = true;
            }
            else
            {
                normalRunRB.Checked = true;
            }

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_disposeYN);
            disposeYN = Convert.ToBoolean(tResponse.tmcReply.value);
            if (disposeYN)
            {
                disposeBottlesRB.Checked = true;
            }
            else
            {
                NoDisposeRB.Checked = true;
            }

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_skipCheckVial456);
            skipVial456 = Convert.ToBoolean(tResponse.tmcReply.value);
            if (skipVial456)
            {
                skipvial456RB.Checked = true;
            }
            else
            {
                dontskipvial456RB.Checked = true;
            }
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_skipCheckBag);
            skipBag = Convert.ToBoolean(tResponse.tmcReply.value);
            if (skipBag)
            {
                skipbagRB.Checked = true;
            }
            else
            {
                dontskipbagRB.Checked = true;
            }

        }

        private void robotTP_Enter(object sender, EventArgs e)
        {
            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                return;
            }
            currentTAB = 5;
            //refreshParams();
        }

        private void showOverideRB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_ShowOverride, "1");
        }

        private void normalRunRB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_ShowOverride, "0");
        }

        private void disposeBottlesRB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_disposeYN, "1");
        }

        private void NoDisposeRB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_disposeYN, "0");
        }

        private void dontskipvial456RB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_skipCheckVial456, "0");
        }

        private void skipvial456RB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_skipCheckVial456, "1");
        }

        private void dontskipbagRB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_skipCheckBag, "0");
        }

        private void skipbagRB_CheckedChanged(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_skipCheckBag, "1");
        }
        // ==============
        //    BackUp
        // ==============
        private void AdmBackUpBtn_Click(object sender, EventArgs e)
        {
            int i;
            string toDay;
            string fileName;

            string fileNameEnd = " Backup sn " + robotSerialTB.Text + " sw " + TrinamicCodeTB.Text + ".txt";
            // ==============================
            // save file
            toDay = DateTime.Now.ToString("yyyy-MM-dd HH-mm");
            fileName = cmPath + backupPath + toDay + fileNameEnd;
            File.WriteAllText(fileName, "\n Backup of CM sn: " + robotSerialTB.Text + "  sw: " + TrinamicCodeTB.Text + "\n" +
                                         "\n Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm") +
                                         "\n User: " + username + "\n\n" +
                                         "=============== \n" +
                                         " GP        value\n" +                  // append to the file
                                         "=============== \n\n");
            for (i = 0; i < 56; i++)
            {
                tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, i.ToString("D"));
                tstringToSGPtest();   // display on "for SGP cmd"
                File.AppendAllText(fileName, i.ToString("D3") + $"{SGPtestTB.Text,12}\n");
            }
            //CreatePdfFile(backupPath + toDay + fileNameEnd);
            logAndShow("Trinamic board back up done: " + fileNameEnd);
        }

        // ==============
        //    RESTORE
        // ==============
        private void AdmRestoreBtn_Click(object sender, EventArgs e)
        {
            int i;
            int j;
            string line;
            string GP;
            string value;
            string[] result; // = new string[16];

            openFileDialog1.InitialDirectory = cmPath + backupPath;
            openFileDialog1.Filter = "text files (*.txt)|*.txt";
            DialogResult dr = openFileDialog1.ShowDialog();  // choose the directory from file list 
            if (dr == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                for (i = 0; i < 10; i++)  // wait for first lines
                {
                    sr.ReadLine();
                }
                for (i = 0; i < 56; i++)
                {
                    line = sr.ReadLine();
                    result = line.Split(' ');
                    for (j = 1; result[j] == ""; j++)  // read each substring (skipping the first, the number)
                    { }                                // skipping the spaces to find the parameter at location j
                    GP = result[0];
                    value = result[j];
                    tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, GP, value);
                    tstringToSGPtest();   // display on "for SGP cmd"
                }
                logAndShow($"Trinamic parameters restored to the robot: \'{openFileDialog1.FileName}\'");
                sr.Close();
            }
        }

        // ***********************
        //       PASSWORD
        // ***********************

        private void visibleUser()
        {
            isAdministrator = false;
            debugPnl.Visible = true;
            GBpanelPNL.Enabled = false;
            backupPnl.Visible = false;
            addRemovePnl.Visible = false;
            PWfileEmptyPnl.Visible = false;
            userPWtlp.Visible = true;
            logoutBtn.Visible = true;
            addToLogPnl.Visible = true;

            UserAdminTLP.Visible = true;
            RunSC.Visible = true;
            calibrateTLP.Visible = false;
            setupTLP.Visible = false;
        }
        private void visibleMaster()
        {
            isAdministrator = true;
            FilesPathTB.Text = cmPath;
            maxBadPWtb.Text = Convert.ToString(maxPWtrials);
            monthsForPWtb.Text = Convert.ToString(maxPWmonths);

            debugPnl.Visible = true;
            GBpanelPNL.Enabled = true;
            backupPnl.Visible = true;
            addRemovePnl.Visible = true;
            PWfileEmptyPnl.Visible = false;
            userPWtlp.Visible = true;
            logoutBtn.Visible = true;
            addToLogPnl.Visible = true;

            UserAdminTLP.Visible = true;
            RunSC.Visible = true;
            calibrateTLP.Visible = true;
            setupTLP.Visible = true;
        }
        private void visibleFalse()
        {
            isAdministrator = false;
            debugPnl.Visible = false;
            GBpanelPNL.Enabled = false;
            backupPnl.Visible = false;
            addRemovePnl.Visible = false;
            PWfileEmptyPnl.Visible = false;
            userPWtlp.Visible = true;
            logoutBtn.Visible = false;
            addToLogPnl.Visible = false;

            UserAdminTLP.Visible = true;
            RunSC.Visible = false;
            //RunParametersTLP.Visible = false;
            calibrateTLP.Visible = false;
            setupTLP.Visible = false;
        }

        // =================
        //    check pw
        // =================
        private void enterPWbtn_Click(object sender, EventArgs e)
        {
            checkPW();
        }
        private void userPWtb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                checkPW();
            }
        }

        private void checkPW()
        {
            string newHashString;
            string line;
            string[] result;
            DateTime stopPwDate;
            DateTime gracePwDate;
            TimeSpan overDuePWdays;
            TimeSpan gracePWdays;

            username = userNameTB.Text;
            userPassWord = userPWtb.Text;
            userPWtb.Text = "";

            if (username == "")
            {
                visibleFalse();
                JustShow("user name is missing");
            }
            else if (userPassWord == "")
            {
                visibleFalse();
                JustShow("Password is missing");
            }
            else  // both ok
            {
                if (!File.Exists(cmPath + pwPath + pwFileName))     // password file exists?
                {
                    logAndShow("PW file does not exist. Try master PW");
                    visibleFalse();
                    PWfileEmptyPnl.Visible = true;          // enable master PW entrance, PW = DATE (2020-12-06)
                    userPWtlp.Visible = false;
                    return;
                }
                // open PW file:
                StreamReader sr = new StreamReader(cmPath + pwPath + pwFileName);

                line = sr.ReadLine();  // read first line
                line = sr.ReadLine();  // read first line
                line = sr.ReadLine();  // read first line
                if (line == null)
                {
                    logAndShow("PW file is empty \n Try master PW");
                    visibleFalse();
                    PWfileEmptyPnl.Visible = true;
                    userPWtlp.Visible = false;
                    return;
                }
                newHashString = getHashString(userPassWord);      // now calculate the HASH string

                while (line != null)
                {
                    result = line.Split(';');
                    if (result[0] == username)             // found the user
                    {
                        if (result[1] == "yesAdmin")
                        {
                            isAdministrator = true;
                        }
                        if (result[3] == "")
                        {
                            result[3] = "0";
                        }
                        if (Convert.ToInt32(result[3]) >= maxPWtrials)   // check number of missed PW trials
                        {
                            if (isAdministrator)
                            {
                                logAndShow("Exeeded max PW tries \n Try master PW");
                                visibleUser();
                                PWfileEmptyPnl.Visible = true;
                                //userPWtl.Visible = false;
                            }
                            else //  !isAdministrator
                            {
                                logAndShow($"Exeeded max PW tries. Erase and create new user \'{username}\'");
                                sr.Close();
                                leftTries = incFailedTimes(username);
                            }
                            return;
                        }
                        // check PW creation time overdue

                        stopPwDate = Convert.ToDateTime(result[2]).AddMonths(maxPWmonths);
                        overDuePWdays = DateTime.Now - stopPwDate;
                        if (overDuePWdays.Days > 0 && !isAdministrator)   // it means we passed the limit
                        {
                            gracePwDate = stopPwDate.AddDays(maxGracePWdays);
                            gracePWdays = DateTime.Now - gracePwDate;
                            if (gracePWdays.Days > 0)   // it means we passed the grace time
                            {
                                logAndShow("PW over due. Goto administrator to renew your password");
                                sr.Close();
                                return;
                            }
                            logAndShow($"Exeeded max PW renewal by {overDuePWdays.Days} days \n" +
                                            $"            You have {-gracePWdays.Days} grace days left");
                        }
                        if (result[4] == newHashString)    // check the hashed PW
                        {
                            if (result.Length == 5)
                            {
                                if (result[1] == "yesAdmin")
                                {
                                    isAdministrator = true;
                                    visibleMaster();
                                }
                                else
                                {
                                    isAdministrator = false;
                                    visibleUser();
                                }
                            }
                            else
                            {
                                isAdministrator = false;
                                visibleUser();
                            }
                            sr.Close();
                            resetFailedTimes(username);
                            logAndShow("Welcome " + (isAdministrator ? "Admin " : "") + "user \'" + username + " \'");
                            return;
                        }
                        else
                        {
                            sr.Close();
                            leftTries = incFailedTimes(username);
                            logAndShow($"PW not correct. You have {leftTries} more tries");
                            return;
                        }
                    }
                    line = sr.ReadLine();
                }
                logAndShow("no maching user was found. Try logging again.");
                sr.Close();
            }
        }
        // *****************
        // new user write
        // *****************
        private void newUserBtn_Click(object sender, EventArgs e)
        {
            userPassWord = newUserPwTB.Text;
            newUserPwTB.Text = "";
            bool needToEraseUser = false;
            bool erasedSuccessfully;
            string line;
            string[] result;
            string newHashString;
            bool Finished = false;

            while (!Finished)
            {
                if (newUserNameTB.Text == "")
                {
                    JustShow("Please enter user name ");
                    break;
                }
                else if (userPassWord == "")   //pwCurrent)
                {
                    // check PW validity
                    JustShow("Please enter Password ");
                    break;
                }
                else if (userPassWord.Length < 4)
                {
                    JustShow("Please enter Password with at least 4 characters");
                    break;
                }
                else  // filled -> set a hash and save to file
                {
                    newHashString = getHashString(userPassWord);      // now calculate the HASH string

                    //  if the file already exists, check for user already exist

                    if (File.Exists(cmPath + pwPath + pwFileName))
                    {
                        StreamReader sr = new StreamReader(cmPath + pwPath + pwFileName);
                        sr.ReadLine();  // read first info line
                        sr.ReadLine();  // read second "===" line
                        line = sr.ReadLine();  // read third 1'st data line

                        while (line != null)
                        {
                            result = line.Split(';');
                            if (result[0] == newUserNameTB.Text)             // found the user?
                            {
                                DialogResult dr1 = MessageBox.Show($"user \'{newUserNameTB.Text}\' already exists \n" +
                                                                   $"Do you want to overwrite \'{newUserNameTB.Text}\' ?",
                                                                   "overwrite existing user?", MessageBoxButtons.YesNo);
                                if (dr1 == DialogResult.No)
                                {
                                    sr.Close();
                                    return;
                                }
                                else //  if (dr1 == DialogResult.Yes)
                                {
                                    sr.Close();
                                    needToEraseUser = true;
                                    break;     // over write the user
                                }
                            }
                            line = sr.ReadLine();
                        }
                        sr.Close();
                    }
                    else //file does not exists
                    {
                        File.WriteAllText(cmPath + pwPath + pwFileName, "** user; yesAdmin; dateCreated; times PW failed; PW hash \n"
                                                                + "** ==================================================== \n");
                    }
                    if (needToEraseUser)
                    {
                        erasedSuccessfully = eraseUser(newUserNameTB.Text);
                        if (!erasedSuccessfully)
                        {
                            logAndShow("Failed to erase user \'" + newUserNameTB.Text + "\'");
                            return;
                        }
                    }

                    // no user duplication, add the new user

                    File.AppendAllText(cmPath + pwPath + pwFileName, newUserNameTB.Text);    // write user name
                    if (IsAdminCkb.Checked)
                    {
                        File.AppendAllText(cmPath + pwPath + pwFileName, ";yesAdmin");        // assign administrator
                    }
                    else
                    {
                        File.AppendAllText(cmPath + pwPath + pwFileName, ";notAdmin");       // assign administrator
                    }
                    File.AppendAllText(cmPath + pwPath + pwFileName, DateTime.Now.ToString(";yyyy-MM-dd")  // Creation date
                                                            + ";00"                                         // times PW failed
                                                            + $";{newHashString}"                          // write hashed PW
                                                            + "\n");                                       // new line

                    //CreatePdfFile(pwPath + pwFileName);
                    IsAdminCkb.Checked = false;
                    Finished = true;
                    logAndShow("new " + (IsAdminCkb.Checked ? "Admin " : "") + "user \'" + newUserNameTB.Text + "\' added");
                }
            }
        }
        // =================
        //     master pw
        // =================
        private void masterPWtb_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                username = "master";
                userPassWord = masterPWtb.Text;

                if (userPassWord == "")
                {
                    visibleFalse();
                    PWfileEmptyPnl.Visible = true;
                    logAndShow("Please Login first with admin PassWord");
                }
                else  // ok, not empty
                {
                    pwMaster = DateTime.Now.ToString("yyyy-MM-dd");

                    if (userPassWord != pwMaster)
                    {
                        visibleFalse();
                        PWfileEmptyPnl.Visible = true;
                        logAndShow("wrong PassWord, try again");
                    }
                    else  // master OK
                    {
                        visibleMaster();
                        PWfileEmptyPnl.Visible = false;
                        logAndShow("Welcome master user " + username + "\n\n"
                                           + "   1. Create a new user \n"
                                           + "   2. Restart application");
                    }
                    masterPWtb.Text = "";
                }
            }
        }
        // =================
        //   hash creator
        // =================
        private string getHashString(string pwString)
        {
            string hashString = "";
            byte[] PWhashValue;
            UnicodeEncoding ue = new UnicodeEncoding();      // Create a new instance of the UnicodeEncoding class to
            byte[] messageBytes = ue.GetBytes(pwString);     // Convert the string into an array of Unicode bytes.
            SHA256 shHash = SHA256.Create();                 // Create a new instance of the SHA256 class to create the hash value.
            PWhashValue = shHash.ComputeHash(messageBytes);  // Create the hash value from the array of bytes.
            for (int x = 0; x < PWhashValue.Length; x++)
            {
                hashString += PWhashValue[x];
            }
            return hashString;
        }

        // ==========================
        //   erase user from button
        // ==========================
        private void eraseUserBtn_Click(object sender, EventArgs e)
        {
            bool erasedSuccessfully;

            DialogResult dr1 = MessageBox.Show($"please confirm erasing user: \'{eraseUserTB.Text}\' \n" +
                                               $"Do you want to erase \'{eraseUserTB.Text}\' ?",
                                               "erase existing user?", MessageBoxButtons.YesNo);
            if (dr1 == DialogResult.No)
            {
                return;
            }
            else //  if (dr1 == DialogResult.Yes)
            {
                erasedSuccessfully = eraseUser(eraseUserTB.Text);
                if (!erasedSuccessfully)
                {
                    logAndShow($"Failed to erase user \'{eraseUserTB.Text}\'");
                    return;
                }
            }
        }
        // ==============
        //   erase user
        // ==============
        private bool eraseUser(string user)
        {
            string[] result;
            int i, j;
            try
            {
                // Open the file to read from.
                string[] readPWs = File.ReadAllLines(cmPath + pwPath + pwFileName);
                for (i = 2; i < readPWs.Length; i++)
                {
                    result = readPWs[i].Split(';');
                    if (result[0] == user)             // found the user -> erase line
                    {
                        for (j = i; j < readPWs.Length - 1; j++)
                        {
                            readPWs[j] = readPWs[j + 1];
                        }
                        Array.Resize(ref readPWs, readPWs.Length - 1); ;
                        File.WriteAllLines(cmPath + pwPath + pwFileName, readPWs);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                logAndShow($"Failed to open file \'{cmPath + pwPath + pwFileName}\'\n {e.Message}");
                return false;
            }
            logAndShow($"Erased user \'{user}\'");
            return true;
        }
        // ===============================
        //   increment times of wrong PW
        // ===============================
        private Int32 incFailedTimes(string user)
        {
            string[] result;
            int i;
            int timesFailed = 0;
            try
            {
                // Open the file to read from.
                string[] readPWs = File.ReadAllLines(cmPath + pwPath + pwFileName);
                for (i = 0; i < readPWs.Length; i++)
                {
                    result = readPWs[i].Split(';');
                    if (result[0] == user)             // found the user -> erase line
                    {
                        timesFailed = Convert.ToInt32(result[3]) + 1;
                        result[3] = Convert.ToString(timesFailed); // increment                        
                        readPWs[i] = String.Join(";", result);
                        File.WriteAllLines(cmPath + pwPath + pwFileName, readPWs);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                logAndShow($"failed to edit PW file \n{e.Message}");
            }
            return maxPWtrials - timesFailed;  // return left trials
        }
        // ==================================
        //   reset Failed Times of wrong PW
        // ==================================
        private bool resetFailedTimes(string user)
        {
            string[] result;
            int i;
            try
            {
                // Open the file to read from.
                string[] readPWs = File.ReadAllLines(cmPath + pwPath + pwFileName);
                for (i = 0; i < readPWs.Length; i++)
                {
                    result = readPWs[i].Split(';');
                    if (result[0] == user)               // found the user -> erase line
                    {
                        result[3] = "0"; // increment    // clear failed times               
                        readPWs[i] = String.Join(";", result);
                        File.WriteAllLines(cmPath + pwPath + pwFileName, readPWs);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                logAndShow($"failed to edit PW file \n{e.Message}");
            }
            return true;
        }
        // ======================
        //   create params file
        // ======================
        private void changeParamsBtn_Click(object sender, EventArgs e)
        {
            if (FilesPathTB.Text == "" || maxBadPWtb.Text == "" || monthsForPWtb.Text == "")
            {
                logAndShow("Please fill all text boxes");
                FilesPathTB.Text = cmPath;
                maxBadPWtb.Text = Convert.ToString(maxPWtrials);
                monthsForPWtb.Text = Convert.ToString(maxPWmonths);
                return;
            }
            checkDirectories();
            WriteParamsFile();
            return;
        }
        private void WriteParamsFile()
        {
            File.WriteAllText(cmPath + paramsPath + paramsFileName, " file Location" + "; " + "PW wrong trials" + "; " + "time lap to renew PW" + " \n"
                                                       + " ================================================================================ \n"
                                                       + $"{FilesPathTB.Text}"           // Files Location
                                                       + $";{maxBadPWtb.Text}"           // maxBadPWtb bad pw trials
                                                       + $";{monthsForPWtb.Text}"        // time lap to renew PW
                                                       + "\n");                          // new line

            //CreatePdfFile(paramsPath + paramsFileName);
            readParamsFile();
            logAndShow("new params file " + cmPath + paramsPath + paramsFileName + " created");
            return;
        }
        private void readParamsFile()
        {
            string[] result;
            if (!File.Exists(cmPath + paramsPath + paramsFileName))
            {
                FilesPathTB.Text = cmPath;
                maxBadPWtb.Text = Convert.ToString(maxPWtrials);
                monthsForPWtb.Text = Convert.ToString(maxPWmonths);
                WriteParamsFile();
                return;
            }
            try
            {
                // Open the file to read from.
                string[] readParams = File.ReadAllLines(cmPath + paramsPath + paramsFileName);
                result = readParams[2].Split(';');
                cmPath = result[0];
                maxPWtrials = Convert.ToInt32(result[1]);
                maxPWmonths = Convert.ToInt32(result[2]);
                //writeLogFile($"parameter file {cmPath + paramsPath + paramsFileName} was read");
            }
            catch (Exception e)
            {
                logAndShow($"failed to read params file \n{e.Message}");
            }
            return;
        }
        // ==================
        //   write log file
        // ==================
        private void writeLogFile(string message)
        {
            if (!File.Exists(cmPath + logPath + logFileName))
            {
                FilesPathTB.Text = cmPath;
                File.WriteAllText(cmPath + logPath + logFileName, "** CM Log file \n"
                                                        + "** ==================================================== \n\n");
                // recursion, call this function again after the file was created and report creation
                // logAndShow("new log File \'" + cmPath + logPath + logFileName + "\' Created");
            }
            try
            {
                File.AppendAllText(cmPath + logPath + logFileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm    ")  // Creation date
                                                         + $"user: \'{username}\'  "                      // user
                                                         + message + "\n");                               // maessage & new line
            }
            catch (Exception e)
            {
                logAndShow($"{e.Message}");
            }
            //CreatePdfFile(logPath + logFileName);
            return;
        }
        // ===============
        //   just show
        // ===============
        private void JustShow(string message)
        {
            MessageBox.Show(message, "information", MessageBoxButtons.OK, MessageBoxIcon.Warning,
                                         MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            //this.TopMost = false; 
            return;
        }
        // ===============
        //   log and show
        // ===============
        private void logAndShow(string message)
        {
            writeLogFile(message);
            //this.TopMost = true;   // to display at the top
            JustShow(message);
            //            MessageBox.Show(message, "information", MessageBoxButtons.OK, MessageBoxIcon.Warning,
            //                                         MessageBoxDefaultButton.Button3, MessageBoxOptions.DefaultDesktopOnly);
            //this.TopMost = false; 
            RunTP.Focus();
            return;
        }

        // =====================
        //  select / focus
        // =====================

        //ControlSetFocus(control: CMForm);
        //ControlSetFocus(cmTC);
        //ControlSelect(CMForm);
        //ControlSelect(cmTC);

        public void ControlSetFocus(Control control)
        {
            // Set focus to the control, if it can receive focus.
            if (control.CanFocus)
            {
                control.Focus();
            }
        }

        public void ControlSelect(Control control)
        {
            // Select the control, if it can be selected.
            if (control.CanSelect)
            {
                control.Select();
            }
        }

        // =====================
        //  VIEW & PRINT FILES
        // =====================

        private void logoutBtn_Click(object sender, EventArgs e)
        {
            visibleFalse();
            writeLogFile($"user \'{username} \' logged out");
            username = "";
        }

        private void viewLogBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(cmPath + logPath + logFileName);
            //string pdfFileName = outputFileName(logFileName);
            //System.Diagnostics.Process.Start(cmPath + pdfPath + pdfFileName);    // open file viewer, there I can view & print
        }

        private void viewPwBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(cmPath + pwPath + pwFileName);
            //string pdfFileName = outputFileName(pwFileName);
            //System.Diagnostics.Process.Start(cmPath + pdfPath + pdfFileName);    // open file viewer, there I can view & print
        }

        private void viewParamsBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(cmPath + paramsPath + paramsFileName);
            //string pdfFileName = outputFileName(paramsFileName);
            //System.Diagnostics.Process.Start(cmPath + pdfPath + pdfFileName);    // open file viewer, there I can view & print
        }
        // ********************

        //private void viewRunBtn_Click(object sender, EventArgs e)
        private void CMrunsBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = cmPath + cmRUNpath;
            openFileDialog1.Filter = "(*cmRUN*.txt)|*cmRUN*.txt";

            DialogResult dr = openFileDialog1.ShowDialog();  // choose the file from list 
            if (dr == DialogResult.OK)
            {
                curentPrintFile = openFileDialog1.FileName;
                try
                {
                    System.Diagnostics.Process.Start(curentPrintFile);    // open file viewer, there I can view & print
                }
                catch (Exception ex)
                {
                    logAndShow($"{ex.Message}");
                    //cmTC.Focus();
                    //RunTP.Focus();
                }
            }
        }


        // ====================
        //  check directories
        // ====================
        private void checkDirectories()
        {
            try
            {
                // Determine whether the directories exist.

                if (!Directory.Exists(cmPath)
                    || !Directory.Exists(cmPath + logPath)
                    || !Directory.Exists(cmPath + backupPath)
                    || !Directory.Exists(cmPath + cmRUNpath)
                    || !Directory.Exists(cmPath + paramsPath)
                    || !Directory.Exists(cmPath + pwPath)
                    || !Directory.Exists(cmPath + setupPath)
                   //|| !Directory.Exists(cmPath + pdfPath)
                   )
                {
                    DirectoryInfo di1 = Directory.CreateDirectory(cmPath + logPath);
                    DirectoryInfo di3 = Directory.CreateDirectory(cmPath + backupPath);
                    DirectoryInfo di4 = Directory.CreateDirectory(cmPath + cmRUNpath);
                    DirectoryInfo di5 = Directory.CreateDirectory(cmPath + paramsPath);
                    DirectoryInfo di6 = Directory.CreateDirectory(cmPath + pwPath);
                    DirectoryInfo di7 = Directory.CreateDirectory(cmPath + setupPath);
                    //DirectoryInfo di8 = Directory.CreateDirectory(cmPath + pdfPath);
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }
        private void setRegeditNotepadTextsize80()
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = @"SOFTWARE\Microsoft\Notepad";
            const string keyName = userRoot + "\\" + subkey;

            Registry.SetValue(keyName, "iPointSize", 80);
            //int tInteger = (int)Registry.GetValue(keyName, "iPointSize", -1);
        }

        private void addToLogTB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                writeLogFile(addToLogTB.Text);
            }
        }

        private void viewBackupBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = cmPath + cmRUNpath;
            //openFileDialog1.Filter = "(*Backup*.pdf)|*Backup*.pdf";

            DialogResult dr = openFileDialog1.ShowDialog();  // choose the file from list 
            if (dr == DialogResult.OK)
            {
                curentPrintFile = openFileDialog1.FileName;
                try
                {
                    System.Diagnostics.Process.Start(curentPrintFile);    // open file viewer, there I can view & print
                }
                catch (Exception ex)
                {
                    logAndShow($"{ex.Message}");
                }
            }
        }

        // ***********
        //  home axis
        // ***********

        // ** vertical **
        private void VerticalHomeBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.homeVerticalMotor);
            tstringToRUNtest();
        }

        private void LinearHomeBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.homeLinearMotor);
            tstringToRUNtest();
        }
        private void ArmHomeBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.homeArmMotor);
            tstringToRUNtest();
        }

        private void PistonHomeBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.homePistonMotor);
            tstringToRUNtest();
        }

        private void HeadRotateHomeBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.homeHeadRotateMotor);
            tstringToRUNtest();
        }

        private void Spare51Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.HomeDisposeMotor);
            tstringToRUNtest();
        }

        private void CapHolderHomeBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.homeCapHolderMotor);
            tstringToRUNtest();
        }
        // ******************
        // *** Home setup ***
        // ******************

        // ***********************************
        // *** GET / SET global parameters ***
        // ***********************************

        private void setGBnumberTB_TextChanged(object sender, EventArgs e)
        {
            if (!rgNumber.Match(setGBnumberTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the GB {setGBnumberTB.Text}");
                setGBnumberTB.Text = "";
            }
        }

        private void getGBnumberTB_TextChanged(object sender, EventArgs e)
        {
            if (!rgNumber.Match(getGBnumberTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the GB {getGBnumberTB.Text}");
                getGBnumberTB.Text = "0";
            }
            if (Convert.ToInt32(getGBnumberTB.Text) >= 256)
            {
                getGBnumberTB.Text = "0";
                logAndShow($"No GB allowed above 255");
            }
        }

        private void setGBvalueTB_Leave(object sender, EventArgs e)
        {
            if (!rgMinus.Match(setGBvalueTB.Text).Success)        // did not match, a non number character is there "-" ok
            {
                //logAndShow($"A non-number value for the GB {setGBvalueTB.Text}");
                setGBvalueTB.Text = "";
            }
        }
        private void setGBbtn_Click(object sender, EventArgs e)
        {
            Int32 GBvalue;
            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                logAndShow("The robot is busy or no connected.");
                return;
            }
            if (!rgNumber.Match(setGBnumberTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the GB {setGBnumberTB.Text}");
                setGBnumberTB.Text = "0";
                return;
            }
            if (Convert.ToInt32(setGBnumberTB.Text) >= 256)
            {
                setGBnumberTB.Text = "0";
                logAndShow($"No GB allowed above 255");
                return;
            }
            tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, setGBnumberTB.Text, setGBvalueTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, setGBnumberTB.Text);
            GBvalue = Convert.ToInt32(tResponse.tmcReply.value);
            setGBresultTB.Text = $"{GBvalue}";
            //            this.Invoke((MethodInvoker)delegate { setGBresultTB.Text = $"{GBvalue}"; });
        }
        // *** Run a command
        private void runCommandTB_Click(object sender, EventArgs e)
        {
            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                logAndShow("The robot is busy or no connected.");
                return;
            }
            if (!rgNumber.Match(commandToRunTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the GB {commandToRunTB.Text}");
                commandToRunTB.Text = "0";
                return;
            }
            if (Convert.ToInt32(commandToRunTB.Text) > GeneralFunctions.lastFunction)
            {
                commandToRunTB.Text = "0";
                logAndShow($"No functions allowed above {GeneralFunctions.lastFunction}");
                return;
            }
            tResponse = rTMCConn.RunCommand(commandToRunTB.Text);
            tstringToRUNtest();    // display on "for RUN cmd"
        }

        //  *********************************************************************************

        // *** set vibration time 4 ***
        private void vibrationTime4TB_Leave(object sender, EventArgs e)
        {
            setVibration4();
        }

        private void vibrationTime4TB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                setVibration4();
            }
        }

        private void setVibration4()
        {
            if (rgNumber.Match(vibrationTime4TB.Text).Success)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_4, vibrationTime4TB.Text);
            }
        }

        private void TestVibrate4Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_4, vibrationTime4TB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibration4IsNeeded, "1");
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_56, "0");
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationHz, vibrationHzTB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrStrengthPercentCalc, vibrationStrengthTB.Text);
            tstringToSGPtest();
            tResponse = rTMCConn.RunCommand(GeneralFunctions.Vibrate);
            tstringToRUNtest();    // display on "for RUN cmd"
        }

        // *** set vibration time 56 ***
        private void vibrationTime56TB_Leave(object sender, EventArgs e)
        {
            setVibration56();
        }

        private void vibrationTime56TB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                setVibration56();
            }
        }

        private void setVibration56()
        {
            if (rgNumber.Match(vibrationTime56TB.Text).Success)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_56, vibrationTime56TB.Text);
            }
        }

        private void TestVibrate56Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_4, "0");
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibration56IsNeeded, "1");
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_56, vibrationTime56TB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationHz, vibrationHzTB.Text);
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrStrengthPercentCalc, vibrationStrengthTB.Text);
            tstringToSGPtest();
            tResponse = rTMCConn.RunCommand(GeneralFunctions.Vibrate);
            tstringToRUNtest();    // display on "for RUN cmd"
        }

        // *** set vibration HZ ***
        private void vibrationHzTB_Leave(object sender, EventArgs e)
        {
            setVibrationHZ();
        }

        private void vibrationHzTB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                setVibrationHZ();
            }
        }

        private void setVibrationHZ()
        {
            if (rgNumber.Match(vibrationHzTB.Text).Success
                && Convert.ToInt32(vibrationHzTB.Text) <= 100
                && Convert.ToInt32(vibrationHzTB.Text) >= 4)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationHz, vibrationHzTB.Text);
            }
            else
            {
                vibrationHzTB.Text = "25";
                logAndShow("wrong number, or not between 4 and 100");
            }
        }

        // *** set vibration strength ***
        private void vibrationStrengthTB_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (Convert.ToInt32(e.KeyCode) == (char)13)    //  Enter key pressed?
            {
                setVibrationStrength();
            }
        }

        private void vibrationStrengthTB_Leave(object sender, EventArgs e)
        {
            setVibrationStrength();
        }

        private void setVibrationStrength()
        {
            if (rgNumber.Match(vibrationStrengthTB.Text).Success
                && Convert.ToInt32(vibrationStrengthTB.Text) <= 100
                && Convert.ToInt32(vibrationStrengthTB.Text) >= 10)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationDutyCyclePercent, vibrationStrengthTB.Text);
            }
            else
            {
                vibrationStrengthTB.Text = "40";
                logAndShow("wrong number, or not between 10 and 100");
            }
        }

        // *******************************
        // *** refresh RUN TAB parameters 
        // *******************************
        private void resreshRUNparameters()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_4);  // GB_114
            vibrationTime4 = Convert.ToInt32(tResponse.tmcReply.value);
            vibrationTime4TB.Text = $"{vibrationTime4}";

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationTime_56);  //GB_116
            vibrationTime56 = Convert.ToInt32(tResponse.tmcReply.value);
            vibrationTime56TB.Text = $"{vibrationTime56}";

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationHz);  //GB_125
            vibrationHz = Convert.ToInt32(tResponse.tmcReply.value);
            vibrationHzTB.Text = $"{vibrationHz}";

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_vibrationDutyCyclePercent);  //GB_123
            vibrationStrength = Convert.ToInt32(tResponse.tmcReply.value);
            vibrationStrengthTB.Text = $"{vibrationStrength}";
        }

        // *******************************
        // ** refresh Calibrate Paramters  
        // *******************************
        private void refreshParams()
        {
            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                //logAndShow("The robot is busy or no connected.");
                return;
            }
            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_verticalCapPos, setDockHeightTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_verticalCapPos);  //GB_8
            LoadingHight = Convert.ToInt32(tResponse.tmcReply.value);
            setDockHeightTB.Text = $"{LoadingHight}";
            // this.Invoke((MethodInvoker)delegate { setDockHeightTB.Text = $"{LoadingHight}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointDown, setHeadAtBottomTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointDown); //GB_52
            linearHomePos = Convert.ToInt32(tResponse.tmcReply.value);
            setHeadAtBottomTB.Text = $"{linearHomePos}";
            // this.Invoke((MethodInvoker)delegate { setHeadAtBottomTB.Text = $"{linearHomePos}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial1, setCenterOfVial1TB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial1); //GB_53
            setCenterOfVial1 = Convert.ToInt32(tResponse.tmcReply.value);
            setCenterOfVial1TB.Text = $"{setCenterOfVial1}";
            // this.Invoke((MethodInvoker)delegate { setCenterOfVial1TB.Text = $"{setCenterOfVial1}"; });
            //////////////
            ///
            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtBottom, setVial4BottomTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtBottom); //GB_51
            setVial4BottomLocation = Convert.ToInt32(tResponse.tmcReply.value);
            setVial4BottomTB.Text = $"{setVial4BottomLocation}";
            // this.Invoke((MethodInvoker)delegate { setVial4BottomTB.Text = $"{setVial4BottomLocation}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtTop, setVial4BottomLocation.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtTop); //GB_46
            setVial4BottomLocation = Convert.ToInt32(tResponse.tmcReply.value);
            setVial4TopTB.Text = $"{setVial4BottomLocation}";
            // this.Invoke((MethodInvoker)delegate { setVial4TopTB.Text = $"{setVial4BottomLocation}"; });
            //////////////
            ///
            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearSyringeLoading, setCapLoadingTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearSyringeLoading); //GB_54
            setCapLoading = Convert.ToInt32(tResponse.tmcReply.value);
            setCapLoadingTB.Text = $"{setCapLoading}";
            // this.Invoke((MethodInvoker)delegate { setCapLoadingTB.Text = $"{setCapLoading}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_ArmUnderVialPosition, setArmVialTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_ArmUnderVialPosition); // GB_47
            ArmHomePosition = Convert.ToInt32(tResponse.tmcReply.value);
            setArmVialTB.Text = $"{ArmHomePosition}";
            //  this.Invoke((MethodInvoker)delegate { setArmVialTB.Text = $"{ArmHomePosition}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_ArmDropVials456Pos, setArmDisposeVials456TB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_ArmDropVials456Pos); // GB_24
            ArmDisposePosition = Convert.ToInt32(tResponse.tmcReply.value);
            setArmDisposeVials456TB.Text = $"{ArmDisposePosition}";
            //  this.Invoke((MethodInvoker)delegate { setArmDisposeVials456TB.Text = $"{ArmDisposePosition}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_armAtBottom, setArmAtBotomTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_armAtBottom); // GB_55
            ArmAtBottom = Convert.ToInt32(tResponse.tmcReply.value);
            setArmAtBotomTB.Text = $"{ArmAtBottom}";
            // this.Invoke((MethodInvoker)delegate { setArmAtBotomTB.Text = $"{ArmAtBottom}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_BumpPosVert, setBumpPosVertTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_BumpPosVert); // GB_9
            BumpPosVert = Convert.ToInt32(tResponse.tmcReply.value);
            setBumpPosVertTB.Text = $"{BumpPosVert}";
            // this.Invoke((MethodInvoker)delegate { setBumpPosVertTB.Text = $"{BumpPosVert}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_26, Spare26TB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_setBumpBottom); // GB_13
            BumpBottom = Convert.ToInt32(tResponse.tmcReply.value);
            setBumpBottomTB.Text = $"{BumpBottom}";
            //  this.Invoke((MethodInvoker)delegate { setBumpBottomTB.Text = $"{BumpBottom}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_PistonHomePos, setPistonStartTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_PistonHomePos); // GB_48
            PistonHomePos = Convert.ToInt32(tResponse.tmcReply.value);
            setPistonStartTB.Text = $"{PistonHomePos}";
            //  this.Invoke((MethodInvoker)delegate { setPistonStartTB.Text = $"{PistonHomePos}"; });

            ////////////////////

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_min_vol_laserDist_AVAL); // GB_14
            LD_minVol = Convert.ToInt32(tResponse.tmcReply.value);
            LD_minVolTB.Text = $"{LD_minVol}";

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_max_vol_laserDist_AVAL); // GB_15
            LD_maxVol = Convert.ToInt32(tResponse.tmcReply.value);
            LD_maxVolTB.Text = $"{LD_maxVol}";

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_piston_defined_vol_uL); // GB_16
            LD_definedVol = Convert.ToInt32(tResponse.tmcReply.value);
            LD_definedVolTB.Text = $"{LD_definedVol}";

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_accepted_diviation_range); // GB_17
            LD_acceptedDev = Convert.ToInt32(tResponse.tmcReply.value);
            LD_acceptedDevTB.Text = $"{LD_acceptedDev}";

            //if ((LD_minVol != 0) && (LD_maxVol != 0) && (LD_definedVol != 0))
            //{
            //    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_messuredAmountOfLiquid); // GB_18
            //    messuredAmountOfLiquid = Convert.ToInt32(tResponse.tmcReply.value);
            //    messuredAmountOfLiquidTB.Text = $"{messuredAmountOfLiquid}";
            //}
            //else
            //{
                messuredAmountOfLiquidTB.Text = "syringe data missing";
            //}

            ////////////////////

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointLeft, setHeadRotateStartTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointLeft); // GB_49
            HeadRotateHomePos = Convert.ToInt32(tResponse.tmcReply.value);
            setHeadRotateStartTB.Text = $"{HeadRotateHomePos}";
            //  this.Invoke((MethodInvoker)delegate { setHeadRotateStartTB.Text = $"{HeadRotateHomePos}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointUp, setHeadRotateTopTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointUp); // GB_50
            HeadRotateAtTop = Convert.ToInt32(tResponse.tmcReply.value);
            setHeadRotateTopTB.Text = $"{HeadRotateAtTop}";
            //  this.Invoke((MethodInvoker)delegate { setHeadRotateTopTB.Text = $"{HeadRotateAtTop}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_Spare51Pos, setDisposeStartTB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtBottom);  // GB_51 
            Vial4Bottom = Convert.ToInt32(tResponse.tmcReply.value);
            setVial4BottomTB.Text = $"{Vial4Bottom}";
            //  this.Invoke((MethodInvoker)delegate { setVial4BottomTB.Text = $"{Vial4Bottom}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_DisposeDropVialsPos, setDropVials123TB.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_DisposeDropVialsPos);  // GB_23
            DisposeDropVialPos = Convert.ToInt32(tResponse.tmcReply.value);
            setDropVials123TB.Text = $"{DisposeDropVialPos}";
            //  this.Invoke((MethodInvoker)delegate { setDropVials123TB.Text = $"{DisposeDropVialPos}"; });

            //tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_UnitsToMoveManual, goDistance_um.Text);
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_UnitsToMoveManual);
            unitsToMove = Convert.ToInt32(tResponse.tmcReply.value);
            goDistanceTB.Text = $"{Convert.ToDouble(unitsToMove) / 1000}";
            //  this.Invoke((MethodInvoker)delegate { goDistanceTB.Text = $"{Convert.ToDouble(unitsToMove) / 1000}"; });
        }

        // ***************************
        // *** set start locations ***
        // ***************************

        // _________________BumpPosVert_____________________________________________
        private void setBumpPosVertBtn_Click(object sender, EventArgs e)
        {
            uploadBumpPosVert();
            v = Convert.ToDouble(M_VerticalLocationTb.Text) * StepsPerMM.M_VerticalStepsPerMM;
            setBumpPosVertTB.Text = Convert.ToString(Convert.ToInt32(v));
            setBumpPosVert();
        }
        private void GoToBumpOfVial1Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setBumpPosVertTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.VerticalManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsBumpPosVertBtn_Click(object sender, EventArgs e)
        {
            setBumpPosVertTB.Text = last_setBumpPosVertTB;
            setBumpPosVert();
        }
        private void setBumpPosVertTB_Leave(object sender, EventArgs e)
        {
            uploadBumpPosVert();
            setBumpPosVert();
        }
        private void setBumpPosVert()
        {
            if (rgMinus.Match(setBumpPosVertTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setBumpPosVertTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_BumpPosVert, setBumpPosVertTB.Text);
            }
            refreshParams();
        }
        private void uploadBumpPosVert()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_BumpPosVert); // GB_9
            BumpPosVert = Convert.ToInt32(tResponse.tmcReply.value);
            last_setBumpPosVertTB = $"{BumpPosVert}";
        }

        // ________________DockHeight__________________________________________________
        private void setDockHeightBtn_Click(object sender, EventArgs e)
        {
            uploadDockHeight();
            v = Convert.ToDouble(M_VerticalLocationTb.Text) * StepsPerMM.M_VerticalStepsPerMM;
            setDockHeightTB.Text = Convert.ToString(Convert.ToInt32(v));
            setDockHeight();
        }
        private void GoToDockHightBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setDockHeightTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.VerticalManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsDockHeightBtn_Click(object sender, EventArgs e)
        {
            setDockHeightTB.Text = last_setDockHeightTB;
            setDockHeight();
        }
        private void setDockHeightTB_Leave(object sender, EventArgs e)
        {
            uploadDockHeight();
            setDockHeight();
        }
        private void setDockHeight()
        {
            if (rgMinus.Match(setDockHeightTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setDockHeightTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_verticalCapPos, setDockHeightTB.Text);
            }
            refreshParams();
        }
        private void uploadDockHeight()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_verticalCapPos);  //GB_8
            LoadingHight = Convert.ToInt32(tResponse.tmcReply.value);
            last_setDockHeightTB = $"{LoadingHight}";
        }

        // _____________setBumpBottom_______________________________________________
        private void setBumpBottomBtn_Click(object sender, EventArgs e)
        {
            uploadBumpBottom();
            v = Convert.ToDouble(M_VerticalLocationTb.Text) * StepsPerMM.M_VerticalStepsPerMM;
            setBumpBottomTB.Text = Convert.ToString(Convert.ToInt32(v));
            setBumpBottom();
        }
        private void GoToBumpBottomBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setBumpBottomTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.VerticalManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsBumpBottomBtn_Click(object sender, EventArgs e)
        {
            //setBumpBottomTB.Text = last_setBumpBottomTB;
            //setBumpBottom();
        }
        private void setBumpBottomTB_Leave(object sender, EventArgs e)
        {
            uploadBumpBottom();
            setBumpBottom();
        }
        private void setBumpBottom()
        {
            if (rgMinus.Match(setBumpBottomTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setBumpBottomTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_setBumpBottom, setBumpBottomTB.Text);
            }
            refreshParams();
        }
        private void uploadBumpBottom()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_setBumpBottom); // GB_13
            BumpBottom = Convert.ToInt32(tResponse.tmcReply.value);
            last_setBumpBottomTB = $"{BumpBottom}";
        }

        // _________________CenterOfVial1__________________________________________
        private void setCenterOfVial1Btn_Click(object sender, EventArgs e)
        {
            uploadlinearCenterOfVial1();
            v = Convert.ToDouble(M_LinearLocationTb.Text) * StepsPerMM.M_LinearStepsPerMM;
            setCenterOfVial1TB.Text = Convert.ToString(Convert.ToInt32(v));
            centerOfVial1();
        }
        private void GoToCenterOfVial1Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setCenterOfVial1TB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.LinearMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsCenterOfVial1Btn_Click(object sender, EventArgs e)
        {
            setCenterOfVial1TB.Text = last_setCenterOfVial1TB;
            centerOfVial1();
        }
        private void setCenterOfVial1TB_Leave(object sender, EventArgs e)
        {
            uploadlinearCenterOfVial1();
            centerOfVial1();
        }
        private void centerOfVial1()
        {
            if (rgMinus.Match(setCenterOfVial1TB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setCenterOfVial1TB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial1, setCenterOfVial1TB.Text);
            }
            refreshParams();
        }
        private void uploadlinearCenterOfVial1()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial1); //GB_53
            setCenterOfVial1 = Convert.ToInt32(tResponse.tmcReply.value);
            last_setCenterOfVial1TB = $"{setCenterOfVial1}";
        }

        // _______________CapLoading__________________________________________________
        private void setCapLoadingBtn_Click(object sender, EventArgs e)
        {
            uploadCapLoading();
            v = Convert.ToDouble(M_LinearLocationTb.Text) * StepsPerMM.M_LinearStepsPerMM;
            setCapLoadingTB.Text = Convert.ToString(Convert.ToInt32(v));
            CapLoading();
        }
        private void GoToCapLoadingBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setCapLoadingTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.LinearMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsCapLoadingBtn_Click(object sender, EventArgs e)
        {
            setCapLoadingTB.Text = last_setCapLoadingTB;
            CapLoading();
        }
        private void setCapLoadingTB_Leave(object sender, EventArgs e)
        {
            uploadCapLoading();
            CapLoading();
        }
        private void CapLoading()
        {
            if (rgMinus.Match(setCapLoadingTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setCapLoadingTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearSyringeLoading, setCapLoadingTB.Text);
            }
            refreshParams();
        }
        private void uploadCapLoading()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearSyringeLoading); //GB_54
            setCapLoading = Convert.ToInt32(tResponse.tmcReply.value);
            last_setCapLoadingTB = $"{setCapLoading}";
        }

        // __________________setVial4Bottom_____________________________________________
        private void setVial4BottomBtn_Click(object sender, EventArgs e)
        {
            uploadVial4Bottom();
            v = Convert.ToDouble(M_LinearLocationTb.Text) * StepsPerMM.M_LinearStepsPerMM;
            setVial4BottomTB.Text = Convert.ToString(Convert.ToInt32(v));
            setVial4Bottom();
        }
        private void GoToVial4BottomBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setVial4BottomTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.LinearMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsVial4BottomBtn_Click(object sender, EventArgs e)
        {
            setVial4BottomTB.Text = last_setVial4BottomTB;
            setVial4Bottom();
        }
        private void setVial4BottomTB_Leave(object sender, EventArgs e)
        {
            uploadVial4Bottom();
            setVial4Bottom();
        }
        private void setVial4Bottom()
        {
            if (rgMinus.Match(setVial4BottomTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setArmVialTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtBottom, setVial4BottomTB.Text);
            }
            refreshParams();
        }
        private void uploadVial4Bottom()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtBottom);  // GB_51 
            Vial4Bottom = Convert.ToInt32(tResponse.tmcReply.value);
            last_setVial4BottomTB = $"{Vial4Bottom}";
        }

        // __________________setVial4Top_____________________________________________
        private void setVial4TopBtn_Click(object sender, EventArgs e)
        {
            uploadVial4Top();
            v = Convert.ToDouble(M_LinearLocationTb.Text) * StepsPerMM.M_LinearStepsPerMM;
            setVial4TopTB.Text = Convert.ToString(Convert.ToInt32(v));
            setVial4Top();
        }
        private void GoToVial4TopBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setVial4TopTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.LinearMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsVial4TopBtn_Click(object sender, EventArgs e)
        {
            setVial4TopTB.Text = last_setVial4TopTB;
            setVial4Top();
        }
        private void setVial4TopTB_Leave(object sender, EventArgs e)
        {
            uploadVial4Top();
            setVial4Top();
        }
        private void setVial4Top()
        {
            if (rgMinus.Match(setVial4TopTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setArmVialTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtTop, setVial4TopTB.Text);
            }
            refreshParams();
        }
        private void uploadVial4Top()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_linearCenterOfVial4AtTop);  // GB_51 
            Vial4Top = Convert.ToInt32(tResponse.tmcReply.value);
            last_setVial4TopTB = $"{Vial4Top}";
        }

        // ______________setArmVial__________________________________________________
        private void setArmVialBtn_Click(object sender, EventArgs e)
        {
            uploadArmVial();
            v = Convert.ToDouble(M_ArmLocationTb.Text) * StepsPerMM.M_ArmStepsPerDeg;
            setArmVialTB.Text = Convert.ToString(Convert.ToInt32(v));
            setArmVial();
        }
        private void GoToArmVialBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setArmVialTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.armMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsArmVialBtn_Click(object sender, EventArgs e)
        {
            setArmVialTB.Text = last_setArmVialTB;
            setArmVial();
        }
        private void setArmVialTB_Leave(object sender, EventArgs e)
        {
            uploadArmVial();
            setArmVial();
        }
        private void setArmVial()
        {
            if (rgMinus.Match(setArmVialTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setArmVialTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_ArmUnderVialPosition, setArmVialTB.Text);
            }
            refreshParams();
        }
        private void uploadArmVial()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_ArmUnderVialPosition); // GB_47
            ArmHomePosition = Convert.ToInt32(tResponse.tmcReply.value);
            last_setArmVialTB = $"{ArmHomePosition}";
        }

        // _______________ArmDisposeVials456_______________________________________
        private void setArmDisposeVials456Btn_Click(object sender, EventArgs e)
        {
            uploadArmDisposeVials456();
            v = Convert.ToDouble(M_ArmLocationTb.Text) * StepsPerMM.M_ArmStepsPerDeg;
            setArmDisposeVials456TB.Text = Convert.ToString(Convert.ToInt32(v));
            setArmDisposeVials456();
        }
        private void GoToDropVials456Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setArmDisposeVials456TB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.armMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsArmDisposeVials456Btn_Click(object sender, EventArgs e)
        {
            setArmDisposeVials456TB.Text = last_setArmDisposeVials456TB;
            setArmDisposeVials456();
        }
        private void setArmDisposeVials456TB_Leave(object sender, EventArgs e)
        {
            uploadArmDisposeVials456();
            setArmDisposeVials456();
        }
        private void setArmDisposeVials456()
        {
            if (rgMinus.Match(setArmDisposeVials456TB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setArmAtBotomTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_ArmDropVials456Pos, setArmDisposeVials456TB.Text);
            }
            refreshParams();
        }
        private void uploadArmDisposeVials456()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_ArmDropVials456Pos); // GB_24
            ArmDisposePosition = Convert.ToInt32(tResponse.tmcReply.value);
            last_setArmDisposeVials456TB = $"{ArmDisposePosition}";
        }

        // _______________setArmAtBotom__________________________________________
        private void setArmAtBotomBtn_Click(object sender, EventArgs e)
        {
            uploadArmAtBotom();
            v = Convert.ToDouble(M_ArmLocationTb.Text) * StepsPerMM.M_ArmStepsPerDeg;
            setArmAtBotomTB.Text = Convert.ToString(Convert.ToInt32(v));
            setArmAtBotom();
        }
        private void GoToArmAtBottomBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setArmAtBotomTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.armMotorManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsArmAtBotomBtn_Click(object sender, EventArgs e)
        {
            setArmAtBotomTB.Text = last_setArmAtBotomTB;
            setArmAtBotom();
        }
        private void setArmAtBotomTB_Leave(object sender, EventArgs e)
        {
            uploadArmAtBotom();
            setArmAtBotom();
        }
        private void setArmAtBotom()
        {
            if (rgMinus.Match(setArmAtBotomTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setArmAtBotomTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_armAtBottom, setArmAtBotomTB.Text);
            }
            refreshParams();
        }
        private void uploadArmAtBotom()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_armAtBottom); // GB_55
            ArmAtBottom = Convert.ToInt32(tResponse.tmcReply.value);
            last_setArmAtBotomTB = $"{ArmAtBottom}";
        }

        // _______ piston start __________________________________________
        private void setPistonBtn_Click(object sender, EventArgs e)
        {
            uploadPistonStart();
            v = Convert.ToDouble(M_PistonLocationTb.Text) * StepsPerMM.M_PistonStepsPerMM;
            setPistonStartTB.Text = Convert.ToString(Convert.ToInt32(v));
            setPistonStart();
        }
        private void GoToPistonStartBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setPistonStartTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.PistonManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsPistonStartBtn_Click(object sender, EventArgs e)
        {
            setPistonStartTB.Text = last_setPistonStartTB;
            setPistonStart();
        }
        private void setPistonStartTB_Leave(object sender, EventArgs e)
        {
            uploadPistonStart();
            setPistonStart();
        }
        private void setPistonStart()
        {
            if (rgMinus.Match(setPistonStartTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setPistonStartTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_PistonHomePos, setPistonStartTB.Text);
            }
            refreshParams();
        }
        private void uploadPistonStart()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_PistonHomePos); // GB_48
            PistonHomePos = Convert.ToInt32(tResponse.tmcReply.value);
            last_setPistonStartTB = $"{PistonHomePos}";
        }

        // _____________Laser distance of piston_______________________________________________
        // _____________LD_minVolBtn_______________________________________________

        private void LD_minVolBtn_Click(object sender, EventArgs e)
        {
            uploadLD_minVol();
            LD_minVolTB.Text = Convert.ToString(Convert.ToInt32(thumbRestDistance));
            setLD_minVol();
        }

        private void oopsLD_minVolTB_Click(object sender, EventArgs e)
        {
            LD_minVolTB.Text = last_LD_minVolTB;
            setLD_minVol();
        }

        private void LD_minVolTB_Leave(object sender, EventArgs e)
        {
            uploadLD_minVol();
            setLD_minVol();
        }


        private void setLD_minVol()
        {
            if (rgMinus.Match(LD_minVolTB.Text).Success)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_min_vol_laserDist_AVAL, LD_minVolTB.Text);
            }
            refreshParams();
        }
        private void uploadLD_minVol()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_min_vol_laserDist_AVAL); // GB_14
            LD_minVol = Convert.ToInt32(tResponse.tmcReply.value);
            last_LD_minVolTB = $"{LD_minVol}";
        }

        // _____________LD_maxVolBtn_______________________________________________

        private void LD_maxVolBtn_Click(object sender, EventArgs e)
        {
            uploadLD_maxVol();
            LD_maxVolTB.Text = Convert.ToString(Convert.ToInt32(thumbRestDistance));
            setLD_maxVol();
        }

        private void oopsLD_maxVolTB_Click(object sender, EventArgs e)
        {
            LD_maxVolTB.Text = last_LD_maxVolTB;
            setLD_maxVol();
        }

        private void LD_maxVolTB_Leave(object sender, EventArgs e)
        {
            uploadLD_maxVol();
            setLD_maxVol();
        }


        private void setLD_maxVol()
        {
            if (rgMinus.Match(LD_maxVolTB.Text).Success)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_max_vol_laserDist_AVAL, LD_maxVolTB.Text);
            }
            refreshParams();
        }

        private void uploadLD_maxVol()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_max_vol_laserDist_AVAL); // GB_15
            LD_maxVol = Convert.ToInt32(tResponse.tmcReply.value);
            last_LD_maxVolTB = $"{LD_maxVol}";
        }

        // ________________LD_definedVol_____________

        private void LD_definedVolTB_Leave(object sender, EventArgs e)
        {
            //uploadLD_definedVol();
            setLD_definedVol();
        }

        private void setLD_definedVol()
        {
            if (rgMinus.Match(LD_definedVolTB.Text).Success)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_piston_defined_vol_uL, LD_definedVolTB.Text);
            }
            refreshParams();
        }
        //private void uploadLD_definedVol() // not needed
        //{
        //    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_piston_defined_vol_uL); // GB_16
        //    LD_definedVol = Convert.ToInt32(tResponse.tmcReply.value);
        //    last_setLD_definedVolTB = $"{LD_definedVol}";
        //}


        // ________________LD_acceptedDev_____________
        private void LD_acceptedDevTB_Leave(object sender, EventArgs e)
        {
            //uploadLD_acceptedDev();
            setLD_acceptedDev();
        }

        private void setLD_acceptedDev()
        {
            if (rgMinus.Match(LD_acceptedDevTB.Text).Success)        // did not match, a non number character is there
            {
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_accepted_diviation_range, LD_acceptedDevTB.Text);
            }
            refreshParams();
        }
        //private void uploadLD_acceptedDev() // not needed
        //{
        //    tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_accepted_diviation_range); // GB_17
        //    LD_acceptedDev = Convert.ToInt32(tResponse.tmcReply.value);
        //    last_setLD_acceptedDevTB = $"{LD_acceptedDev}"; // 
        //}

        // _____________ thumbRestDistanceFilter _______________________________________________

        private void thumbRestDistanceFilterSizeTB_TextChanged(object sender, EventArgs e)
        {
            if (rgNumber.Match(thumbRestDistanceFilterSizeTB.Text).Success)        // did not match, a non number character is there or a negative 
            {
                thumbRestDistanceFilterSize = Convert.ToInt32(thumbRestDistanceFilterSizeTB.Text);
                if (thumbRestDistanceFilterSize < 1)
                    thumbRestDistanceFilterSize = 1;

                // after the filter size has been changed - the filtering variables needs to be reset
                thumbRestDistanceAvg = 0;
                thumbRestDistanceSum = 0;
                thumbRestDistanceCount = 1;
            }
            refreshParams();
        }

        // _____________ linearSpaceBetweenVialsuM _______________________________________________

        //private void linearSpaceBetweenVialsuMTB_TextChanged(object sender, EventArgs e)
        //{
        //    if (rgNumber.Match(linearSpaceBetweenVialsuMTB.Text).Success)        // did not match, a non number character is there or a negative 
        //    {
        //        tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_linearSpaceBetweenVialsuM, linearSpaceBetweenVialsuMTB.Text);

        //        //linearSpaceBetweenVialsuM = Convert.ToInt32(linearSpaceBetweenVialsuMTB.Text);

        //    }
        //}



        // _____________ positioning to Laser distance calicration location _______________________________________________

        private void moveToLDcalibLocationBtn_Click(object sender, EventArgs e)
        {
            run_moveToLDcalibLocation();
        }

        private void run_moveToLDcalibLocation()
        {
            // make sure that this can run only right after a HOME (init)
            if (rTMCConn == null || !rTMCConn.TrinamicOK)
            {
                logAndShow("The robot is busy or no connected.");
                return;
            }
            if (RunInProcess)
            {
                logAndShow("The PC did not finish the RUN");
                return;
            }

            RunInProcess = true;                                  // eliminate re-entrance

            this.Invoke((MethodInvoker)delegate { RunParametersTLP.Enabled = false; });
            this.Invoke((MethodInvoker)delegate { calibrateTLP.Enabled = false; });

            if (!readyForNewCommand)
            {
                logAndShow("The Robot is busy, wait and try again");
                goto exit;  // exit
            }
            if (rTMCConn.TrinamicAborted())
            {
                logAndShow(" Robot aborted, please intitiate HOME");
                goto exit;  // exit
            }

            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_CurrentState);
            // GB_CurrentState must be = WAITING_DISPENSE ( = 30)
            if (Convert.ToInt32(tResponse.tmcReply.value) != 30)
            {
                logAndShow("please intitiate HOME before using this function");
                goto exit;  // exit
            }
            else
            {
                tResponse = rTMCConn.RunCommand(GeneralFunctions.moveToLDcalibLocation);
            }
        //RunInProcess = false;
        exit:
            this.Invoke((MethodInvoker)delegate { RunParametersTLP.Enabled = true; });
            this.Invoke((MethodInvoker)delegate { calibrateTLP.Enabled = true; });
            RunInProcess = false;
        }


        private void moveFromLDcalibLocationBtn_Click(object sender, EventArgs e)
        {
            run_moveFromLDcalibLocation();
            inLDcalibLocation = true;
        }

        private void run_moveFromLDcalibLocation()
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.moveFromLDcalibLocation);
            inLDcalibLocation = false;
        }

        // _____________HeadRotateTop_______________________________________________
        private void setHeadRotateTopBtn_Click(object sender, EventArgs e)
        {
            uploadHeadRotateTop();
            v = Convert.ToDouble(M_headRotateLocationTb.Text) * StepsPerMM.M_RotateStepsPerDeg;
            setHeadRotateTopTB.Text = Convert.ToString(Convert.ToInt32(v));
            setHeadRotateTop();
        }

        private void GoToHeadRotateTopBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setHeadRotateTopTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.RotationManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }

        private void oopsHeadRotateTopBtn_Click(object sender, EventArgs e)
        {
            setHeadRotateTopTB.Text = last_setHeadRotateTopTB;
            setHeadRotateTop();
        }

        private void setHeadRotateTopTB_Leave(object sender, EventArgs e)
        {
            uploadHeadRotateTop();
            setHeadRotateTop();
        }

        private void setHeadRotateTop()
        {
            if (rgMinus.Match(setHeadRotateTopTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setHeadRotateTopTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointUp, setHeadRotateTopTB.Text);
            }
            refreshParams();
        }

        private void uploadHeadRotateTop()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointUp); // GB_50
            HeadRotateAtTop = Convert.ToInt32(tResponse.tmcReply.value);
            last_setHeadRotateTopTB = $"{HeadRotateAtTop}";
        }

        // _____________HeadRotateStart_______________________________________________
        private void setHeadRotateStartBtn_Click(object sender, EventArgs e)
        {
            uploadHeadRotateStart();
            v = Convert.ToDouble(M_headRotateLocationTb.Text) * StepsPerMM.M_RotateStepsPerDeg;
            setHeadRotateStartTB.Text = Convert.ToString(Convert.ToInt32(v));
            setHeadRotateStart();
        }
        private void GoToHeadRotateStartBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setHeadRotateStartTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.RotationManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsHeadRotateStartBtn_Click(object sender, EventArgs e)
        {
            setHeadRotateStartTB.Text = last_setHeadRotateStartTB;
            setHeadRotateStart();
        }
        private void setHeadRotateStartTB_Leave(object sender, EventArgs e)
        {
            uploadHeadRotateStart();
            setHeadRotateStart();
        }

        private void setHeadRotateStart()
        {
            if (rgMinus.Match(setHeadRotateStartTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setHeadRotateStartTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointLeft, setHeadRotateStartTB.Text);
            }
            refreshParams();
        }
        private void uploadHeadRotateStart()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointLeft); // GB_49
            HeadRotateHomePos = Convert.ToInt32(tResponse.tmcReply.value);
            last_setHeadRotateStartTB = $"{HeadRotateHomePos}";
        }

        // _________________HeadAtBottom______________________________________________
        private void setHeadAtBottomBtn_Click(object sender, EventArgs e)
        {
            uploadHeadAtBottom();
            v = Convert.ToDouble(M_headRotateLocationTb.Text) * StepsPerMM.M_RotateStepsPerDeg;
            setHeadAtBottomTB.Text = Convert.ToString(Convert.ToInt32(v));
            setHeadAtBottom();
        }
        private void GoToHeadAtBottomBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setHeadAtBottomTB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.RotationManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsHeadAtBottomBtn_Click(object sender, EventArgs e)
        {
            setHeadAtBottomTB.Text = last_setHeadAtBottomTB;
            setHeadAtBottom();
        }
        private void setHeadAtBottomTB_Leave(object sender, EventArgs e)
        {
            uploadHeadAtBottom();
            setHeadAtBottom();
        }
        private void setHeadAtBottom()
        {
            if (rgMinus.Match(setHeadAtBottomTB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setDisposeStartTB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointDown, setHeadAtBottomTB.Text);
            }
            refreshParams();
        }
        private void uploadHeadAtBottom()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_HeadRotatePointDown); //GB_52
            linearHomePos = Convert.ToInt32(tResponse.tmcReply.value);
            last_setHeadAtBottomTB = $"{linearHomePos}";
        }

        // _____________DropVials123_______________________________________________
        private void setDropVials123Btn_Click(object sender, EventArgs e)
        {
            uploadDropVials123m();
            v = Convert.ToDouble(M_DisposeLocationTb.Text) * StepsPerMM.M_disposeMicroStepsPerMM;
            setDropVials123TB.Text = Convert.ToString(Convert.ToInt32(v));
            setDropVials123();
        }
        private void GoToDropVials123Btn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_GoToAbsoluteInSteps, setDropVials123TB.Text);
            tstringToSGPtest();   // display on "for SGP cmd"
            Thread.Sleep(300);  // wait before moving
            tResponse = rTMCConn.RunCommand(GeneralFunctions.DisposeManualAbsolute);
            tstringToRUNtest();    // display on "for RUN cmd"
        }
        private void oopsDropVials123Btn_Click(object sender, EventArgs e)
        {
            setDropVials123TB.Text = last_setDropVials123TB;
            setDropVials123();
        }
        private void setDropVials123TB_Leave(object sender, EventArgs e)
        {
            uploadDropVials123m();
            setDropVials123();
        }
        private void setDropVials123()
        {
            if (rgMinus.Match(setDropVials123TB.Text).Success)        // did not match, a non number character is there
            {
                //logAndShow($"A non-number value for the distance {setDropVials123TB.Text}");
                tResponse = rTMCConn.SetSGPandStore(AddressBank.GetParameterBank, SystemVariables.GB_DisposeDropVialsPos, setDropVials123TB.Text);
            }
            refreshParams();
        }
        private void uploadDropVials123m()
        {
            tResponse = rTMCConn.GetGGP(AddressBank.GetParameterBank, SystemVariables.GB_DisposeDropVialsPos);  // GB_23
            DisposeDropVialPos = Convert.ToInt32(tResponse.tmcReply.value);
            last_setDropVials123TB = $"{DisposeDropVialPos}";
        }

        // *****************************************************************************

        private void ClrAllBtn_Click(object sender, EventArgs e)
        {
            string vialSize;
            string strWithdraw;
            string fillSize;
            uint i;

            for (i = 1; i <= 6; i++)                                // go over 18 bottles
            {
                vialSize = $"Vial{i:D1}SizeMlTB";                           // 1 2 3 volume column
                strWithdraw = $"Vial{i:D1}WithdrawMlTB";                     // 1 2 3 volume column
                fillSize = $"Vial{i:D1}FillMlTB";
                foreach (Control d in RunParametersTLP.Controls)
                {
                    if (d is TextBox && string.Equals(vialSize, d.Name))
                    {
                        d.Text = $"0";
                    }
                    else if (d is TextBox && string.Equals(strWithdraw, d.Name))
																		   
                    {
                        d.Text = "0";    // asVialSize;
                    }
                    else if (d is TextBox && string.Equals(fillSize, d.Name))
                    {
                        d.Text = "0";
                    }
                }
            }

            vibrationTime56TB.Text = "0";
            vibrationTime4TB.Text = "0";

            //// clear volumes in the vials and bag
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_1, "0");   //GB_197
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_2, "0");   //GB_198
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_3, "0");   //GB_199
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_4, "0");   //GB_200
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_5, "0");   //GB_201
            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinVial_6, "0");   //GB_202

            //tResponse = rTMCConn.SetSGP(AddressBank.GetParameterBank, SystemVariables.GB_microLinBAG, "0");   //GB_99
            BagSizeMlTB.Text = "0";
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            tResponse = rTMCConn.RunCommand(GeneralFunctions.screenAllVials);
            tstringToRUNtest();    // display on "for RUN cmd"
                                   //mLbagToFillTB.Text = Convert.ToString(Convert.ToDouble(Vial1WithdrawMlTB.Text) + Convert.ToDouble(Vial2WithdrawMlTB.Text)
                                   //                                    + Convert.ToDouble(Vial3WithdrawMlTB.Text) + Convert.ToDouble(Vial4WithdrawMlTB.Text)
                                   //                                    + Convert.ToDouble(Vial5WithdrawMlTB.Text) + Convert.ToDouble(Vial6WithdrawMlTB.Text) );
        }

        private void ejectSyringeTopBtn_Click(object sender, EventArgs e)
        {
            goDistanceTB.Text = "70";
            setManualDistance();
            VerticalGoDown();

            //tResponse = rTMCConn.RunCommand(GeneralFunctions.ejectSyringeFromTopVial);
            //tstringToRUNtest();
        }

        private void ejectSyingeBottomBtn_Click(object sender, EventArgs e)
        {
            goDistanceTB.Text = "70";
            setManualDistance();
            VerticalGoUp();

            //tResponse = rTMCConn.RunCommand(GeneralFunctions.ejectSyringeFromBottomVial);
            //tstringToRUNtest();
        }
        // *****************************************************************************

        // =============================================================
    }
}
/*

3 references
public partial class Form1 : Form

int txtHeight;
float txtFontSize;
1 reference
public Form1()

InitializeComponent();

txtHeight = textBox1.Size. Height;
txtFontSize = textBox1.Font.Size;
textBox1. Text = "Hey Dude! !";
label1. Text = "What's up ?? ";

I

 
 1 reference
private void textBox1_SizeChanged(object sender, EventArgs e)

float heightMult = Convert. ToInt32(textBox1.Size.Height / txtHeight);

if (heightMult < 1.0)

heightMult = 1.0F;

textBox1. Font = new Font(textBox1.Font. FontFamily, heightMult * txtFontSize);
label1.Font = new Font(label1.Font.FontFamily, heightMult * txtFontSize);

}
 */

