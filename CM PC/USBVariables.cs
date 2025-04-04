﻿// last modified by : LED
// last modified at : 2024-12-29

namespace SpaceUSB
{
    public static class MotorsNum
    {
        public const string M_Vertical = "0";               // refL                                              up down
        public const string M_Linear = "1";                 // refL, refR                                      linear movement between vials
        public const string M_Arm = "2";                   // refL                                               move vials from top to bottom
        public const string M_Piston = "3";                // refL,     syringe(RefR)                   fill syringe
        public const string M_HeadRotate = "4";      // refL                                               push vials out from the machine
        public const string M_Dispose = "5";            // refL                                                push vials out from the machine
        public const string M_CapHolder = "6";       // sensor at In_capHolderHome.       separate motor driver
    }
    public static class TrinamicInputs
    {
        // public const string In_thumbRestDistance = "0";
        public const string In_liquidPresence = "0";
        public const string In_pwrDrawer = "1";
        public const string In_NeedleDetected = "2";
        public const string In_slidingDoor = "3";
        // public const string In_liquidPresence = "4";
        public const string In_thumbRestDistance = "4";
        public const string In_drawerOverflow = "5";
        public const string In_drawerClose = "6";
        public const string In_capHolderHome = "7";
    }
    public static class TrinamicMuxInputs
    // multiplexed by Out_Multiplexer
    {
        public const string InX_salineBag = "0";
        public const string InX_Vial1 = "1";
        public const string InX_vial2 = "2";
        public const string InX_vial3 = "3";
        public const string InX_vial4 = "4";
        public const string InX_vial5 = "5";
        public const string InX_vial6 = "6";
        public const string InX_mux7 = "7";
    }
    public static class SwitchOutputs
    {
        public const string GreenLED = "0";
        public const string RedLED = "1";
        public const string Out_PulseCapHolder = "2";
        public const string Out_VibrateDIR = "3";
        public const string Out_CAPHolderDIR_Down = "4";
        public const string Out_enaVibrate_0 = "5";
        public const string Out_enaVibrate_1 = "6";
        public const string Out_Multiplexer = "7";
    }
    public static class StepsPerMM
    {
        public const double M_VerticalStepsPerMM = 656.141;
        public const double M_LinearStepsPerMM = 125.984;
        public const double M_ArmStepsPerDeg = 88.889;
        public const double M_PistonStepsPerMM = 656.141;
        public const double M_RotateStepsPerDeg = 177.778;
        public const double M_disposeMicroStepsPerMM = 400;
        public const double M_capHolderMicroStepsPerMM = 50;
    }
    public static class Instruction
    {
        public const string RunApplication = "129";
        public const string SetAxisParameter = "5";
        public const string GetAxisParameter = "6";
        public const string StoreAxisParameter = "7";
        public const string SetGlobalParameter = "9";
        public const string GetGlobalParameter = "10";
        public const string StoreGlobalParameter = "11";
        public const string SetSwitchOutput = "14";
        public const string GetGlobalInOut = "15";
        public const string ResetRobotSoftware = "131";
    }
    public static class AddressBank
    {
        public const string Trinamic = "1";             // trinamic module address
        public const string GetSystemBank = "0";
        public const string GetParameterBank = "2";
        public const string CmdSpecificAddress = "1";
        public const string setOutputs = "2";
        public const string getDigitalInputs = "0";
        public const string getAnalogInputs = "1";
        public const string actualPosition = "1";
        public const string RightLimitSwStatus = "10";
    }
    public static class Values
    {
        public const string Forward = "1";              // move forward
        public const string Backward = "-1";            // move backward
        public const int maxAVAL = 4095;            // move backward
    }
    public static class OutputStates                    // OnOff
    {
        public const string ON = "1";
        public const string OFF = "0";
    }
    public static class SystemVariables                 // GP
    {
        //public const string GB_0 = "0";
        public const string GB_currentVersion = "0";

        public const string GB_Syringe_Type = "1";

        public const string GB_skipCheckVial456 = "2";             // temorary variable 5
        public const string GB_skipCheckBag = "3";

        //public const string GB_2 = "2";   
        //public const string GB_3 = "3";
        //public const string GB_4 = "4";
        //public const string GB_5 = "5";

        public const string GB_bypassSlidingDoorYN = "4";
        public const string GB_bypassDrawerClosedYN = "5";
        public const string GB_bypassCheckOverFlowYN = "6";
        //public const string  // GB_7 = "7";  

        public const string GB_RobotSerialNumber = "7"; // "6";


        public const string GB_verticalCapPos = "8";
        public const string GB_BumpPosVert = "9";
        public const string GB_needleLength = "10";
        public const string GB_needleGauge = "11";
        public const string GB_cyclesTotal = "12";
        public const string GB_setBumpBottom = "13";
        public const string GB_min_vol_laserDist_AVAL = "14";
        public const string GB_max_vol_laserDist_AVAL = "15";
        public const string GB_piston_defined_vol_uL = "16";
        public const string GB_accepted_diviation_range = "17";
        public const string GB_messuredAmountOfLiquid = "18";
        // public const string GB_current_diviation = "19";
        public const string GB_liquid_detection_border_aval = "19";
        public const string GB_ShowOverride = "20";
        public const string GB_linearSpaceBetweenVialsuM = "21";
        public const string GB_disposeYN = "22";
        public const string GB_DisposeDropVialsPos = "23";
        public const string GB_ArmDropVials456Pos = "24";

        public const string GB_25 = "25";
        public const string GB_26 = "26";
        public const string GB_27 = "27";
        //public const string GB_bypassSlidingDoorYN = "25";
        //public const string GB_bypassDrawerClosedYN = "26";          
        //public const string GB_bypassCheckOverFlowYN = "27";

        public const string GB_errors_syringe_bag = "28";
        /*
 // bit errors for parameter 28:
    BitEr_bagIsMissing         = %00000000001 // bit    1  
    BitEr_syringePoppedOut     = %00000000010 // bit    2  syringe popped out during cycle
    BitEr_volumeExceedsBag     = %00000000100 // bit    4  
    BitEr_SyringeIsIn          = %00000001000 // bit    8  FIND_HOMES error or INIT_DRAW_DOSE, Syringe is in the system
    BitEr_SyringeMissing       = %00000010000 // bit   16  DRAW_DOSE error, missing the syringe
    BitEr_machineAborted       = %00000100000 // bit   32
    BitEr_SyringeNotInBag      = %00001000000 // bit   64
    BitEr_SyringeVolume_Error  = %00010000000 // bit  128
    BitEr_SyringeContent_Error = %00100000000 // bit  256
        */
        public const string GB_any_Error = "29";     //1 = will not check for pig removal (GP 28 error)

        public const string GB_errors_M_verticalMotor = "30";
        public const string GB_errors_M_linearMotor = "31";
        public const string GB_errors_M_armMotor = "32";
        public const string GB_errors_M_pistonMotor = "33";
        public const string GB_errors_M_headRotateMotor = "34";
        public const string GB_errors_M_disposeMotor = "35";
        public const string GB_errors_M_capHolderMotor = "36";

        /*      motor errors for each of parameters 30-36:
    BitEr_leftRefSensor  =  %00000001 // bit   1  left ref sensor
    BitEr_rightRefSensor =  %00000010 // bit   2  right ref sensor
    BitEr_homeNotFound   =  %00000100 // bit   4  did not find home
    BitEr_TimeOut        =  %00001000 // bit   8
//  BitEr_calibrationErr =  %00010000 // bit  16
    BitEr_m6             =  %00100000 // bit  32
    BitEr_m7             =  %01000000 // bit  64
    BitEr_m8             =  %10000000 // bit 128
        */
        public const string GB_errors_Vial_1 = "37";
        public const string GB_errors_Vial_2 = "38";
        public const string GB_errors_Vial_3 = "39";
        public const string GB_errors_Vial_4 = "40";
        public const string GB_errors_Vial_5 = "41";
        public const string GB_errors_Vial_6 = "42";
        // bit errors for parameter 37-42 Vials:
        /*
    BitEr_VialTooSmall      = %00000000001 // bit    1  
    BitEr_VialMissing       = %00000000010 // bit    2  
    BitEr_VialPoppedOut     = %00000000100 // bit    4  vial popped out during cycle
    BitEr_v4                = %00000001000 // bit    8
    BitEr_v5                = %00000010000 // bit   16
    BitEr_v6                = %00000100000 // bit   32
    BitEr_v7                = %00001000000 // bit   64
    BitEr_v8                = %00010000000 // bit  128
        */
        public const string GB_errors_findHome = "43";
        /*
         // bit errors for parameter 43
    BitEr_syringeIsInwhileFindHome      =  %00000001 // bit   1
    BitEr_h2                            =  %00000010 // bit   2
    BitEr_capHolderIsInWhileFindHome    =  %00000100 // bit   4
    BitEr_h4                            =  %00001000 // bit   8
    BitEr_h5                            =  %00010000 // bit  16
    BitEr_h6                            =  %00100000 // bit  32
    BitEr_h7                            =  %01000000 // bit  64
    BitEr_h8                            =  %10000000 // bit 128
                */
        public const string GB_errors_wrong_PC_command = "44";
        /*
// bit errors for parameter 44
    BitEr_expecting_GP5_10_OR_30        =  %00000001 // bit   1
    BitEr_expecting_WAITING_DISPENSE   =  %00000010 // bit   2
    BitEr_c3                            =  %00000100 // bit   4
    BitEr_vibrateParemeterError         =  %00001000 // bit   8
    BitEr_c5                            =  %00010000 // bit  16
    BitEr_c6                            =  %00100000 // bit  32
    BitEr_c7                            =  %01000000 // bit  64
    BitEr_c8                            =  %10000000 // bit 128
        */
        public const string GB_special_Error = "45";
        /*
          //  motor errors for each of parameter 45
    BitEr_slidingDoorIsOpen   =  %00000001 // bit   1  
    BitEr_drawerOverflow      =  %00000010 // bit   2  
    BitEr_No_vials            =  %00000100 // bit   4  DRAW_DOSE error, missing the vial
    BitEr_drawerIsOpen        =  %00001000 // bit   8
    BitEr_vialNotDefined      =  %00010000 // bit  16
    BitEr_s5                  =  %00100000 // bit  32
    BitEr_s6                  =  %01000000 // bit  64
    BitEr_s7                  =  %10000000 // bit 128
        */

        public const string GB_linearCenterOfVial4AtTop = "46";
        public const string GB_ArmUnderVialPosition = "47";
        public const string GB_PistonHomePos = "48";
        public const string GB_HeadRotatePointLeft = "49";
        public const string GB_HeadRotatePointUp = "50";
        public const string GB_linearCenterOfVial4AtBottom = "51";
        public const string GB_HeadRotatePointDown = "52";
        public const string GB_linearCenterOfVial1 = "53";
        public const string GB_linearSyringeLoading = "54";
        public const string GB_armAtBottom = "55";

        // ***********************************************************************
        // from here on, GB cannot be stored (STGP) in the EEPROM
        // ***********************************************************************
        public const string GB_liquidDetected = "56";

        public const string GB_57 = "57";
        public const string GB_58 = "58";
        public const string GB_59 = "59";
        public const string GB_60 = "60";

        public const string GB_Calced_Top_Vert_Needle_Down_Allowed = "61";

        public const string GB_62 = "62";
        public const string GB_63 = "63";
        public const string GB_64 = "64";
        public const string GB_65 = "65";

        //public const string GB_currentVialLinearLocation = "65";
        public const string GB_LinearCenterOfBag = "66";
        //public const string GB_linearSpaceBetweenVialsuS = "67";

        public const string GB_67 = "67";
        public const string GB_68 = "68";
        public const string GB_69 = "69";

        public const string GB_armMicroStepsPerMM = "70";

        // public const string GB_71 = "71";
        // public const string GB_72 = "72";
        public const string GB_limitarmBentMicroS = "71";
        public const string GB_NeedleArmError = "72";
        public const string GB_73 = "73";
        public const string GB_74 = "74";
        // public const string GB_75 = "75";
        public const string GB_DrawWaitTime = "75";

        public const string GB_pistonMicroStepPer100microL = "76";
        public const string GB_thumbRestDistance = "77"; // analog value reading from laser distance sensor
        public const string GB_pistonsRangePercentage = "78";
        public const string GB_liquidPresence = "79";

        public const string GB_80 = "80";

        public const string GB_rotateMicroStepsPerMM = "81";

        public const string GB_82 = "82";

        public const string GB_NeedleVialError = "83";

        public const string GB_84 = "84";

        public const string GB_capHolderMicrostepsTolock = "85";
        public const string GB_CapHolderHolds = "86"; //  "1" = holds,    "0" = open
        public const string GB_CapHolderPulses = "87";
        public const string GB_slowCapHolder = "88";
        public const string GB_capWaitLoops = "89";
        public const string GB_microLtoWithdraw_current = "90";

        // microLtoWithdraw values
        public const string GB_microLtoWithdraw_1 = "91";
        public const string GB_microLtoWithdraw_2 = "92";
        public const string GB_microLtoWithdraw_3 = "93";
        public const string GB_microLtoWithdraw_4 = "94";
        public const string GB_microLtoWithdraw_5 = "95";
        public const string GB_microLtoWithdraw_6 = "96";
        /*
            Bit_vial1      =  %00000001 // bit   1
            Bit_vial2      =  %00000010 // bit   2
            Bit_vial3      =  %00000100 // bit   4
            Bit_vial4      =  %00001000 // bit   8
            Bit_vial5      =  %00010000 // bit  16
            Bit_vial6      =  %00100000 // bit  32
            Bit_bag        =  %01000000 // bit  64
        */

        public const string GB_CmdInProcess = "97";
        public const string GB_CurrentState = "98";
        /*
            WAITING_INIT_CM         = 10       // this the state after the power up as well
            RUNNING_INIT_CM         = 20
            WAITING_DISPENSE     = 30       // at this state it is possible as well to run INIT_CM_DOSE (6)
            RUNNING_DISPENSE     = 40
            STATE_50             = 50
            STATE_60             = 60
            STOPPED_ON_ERROR     = 70
            STOPPED_TIME_OUT     = 80
            ABORTED              = 90
        */
        // public const string GB_99 = "99";
        public const string GB_microLinBAG = "99";
        public const string GB_moveManualBackwards = "100";   // 1 = forward;  -1 = backward
        // vialSize_mL values
        public const string GB_vialSize_microL_1 = "101";
        public const string GB_vialSize_microL_2 = "102";
        public const string GB_vialSize_microL_3 = "103";
        public const string GB_vialSize_microL_4 = "104";
        public const string GB_vialSize_microL_5 = "105";
        public const string GB_vialSize_microL_6 = "106";
        public const string GB_vialSize_microL_current = "107";
        public const string GB_BagSize_microL = "108";

        // public const string GB_101 = "101";    
        // public const string GB_102 = "102";
        // public const string GB_103 = "103";
        // public const string GB_104 = "104";    
        // public const string GB_105 = "105";
        // public const string GB_106 = "106";
        // public const string GB_107 = "107";
        // public const string GB_108 = "108";
        public const string GB_109 = "109";
        public const string GB_110 = "110";
        // public const string GB_111 = "111";

        public const string GB_rotateVialsDown = "111";      // 1600
        public const string GB_vibration4IsNeeded = "112";
        public const string GB_vibration56IsNeeded = "113";
        /*
            Bit_vibrate0      =  %00000001 // bit   1
            Bit_vibrate1      =  %00000010 // bit   2
            Bit_vibrate2      =  %00000100 // bit   4
            Bit_vibrate3      =  %00001000 // bit   8
            Bit_vibrate4      =  %00010000 // bit  16
            Bit_vibrate5      =  %00100000 // bit  32
         */
        // vibration time for vials [seconds]
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public const string GB_vibrationTime_4 = "114";              // [sec] input
        public const string GB_vibrationTime_4_calc = "115";
        public const string GB_vibrationTime_56 = "116";             // [sec] input
        public const string GB_vibrationTime_56_calc = "117";
        public const string GB_vibrationLocation = "118";
        /*
            Bit_vibrateDown      =  %00000001 // bit   1
            Bit_vibrateUp        =  %00000010 // bit   2
            Bit_vibrateHalfway   =  %00000100 // bit   4
            Bit_vibrateLocation3 =  %00001000 // bit   8
            Bit_vibrateLocation4 =  %00010000 // bit  16
            Bit_vibrateLocation5 =  %00100000 // bit  32
        */
        // public const string GB_118 = "118";

        // vibration strength for vials 4,56 -  1 / 2 / 3 / 4 / 5
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public const string GB_vibrStrengthPercentCalc = "119";      // set up %
        public const string GB_PwmDutyCycleMS = "120";               // calculated [ms]
        public const string GB_vibrator4done = "121";
        public const string GB_vibrator56done = "122";
        public const string GB_vibrationDutyCyclePercent = "123";    // 10-100 [%]  input

        // vibration cycle time for vials 4,56 - [ms]
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public const string GB_vibrationCycleMS = "124";              // calculation to ms
        public const string GB_vibrationHz = "125";                   // data input  [Hz]  input
        public const string GB_126 = "126";
        public const string GB_vibrationIsNeeded = "127";
        public const string GB_vibrationTime_4_tmp = "128";
        public const string GB_vibrationTime_56_tmp = "129";

        // microLtoFill values

        public const string GB_microLtoFill_current = "130";
        // public const string GB_131 = "131";
        // public const string GB_132 = "132";
        // public const string GB_133 = "133";
        public const string GB_microLtoFill_1 = "131";
        public const string GB_microLtoFill_2 = "132";
        public const string GB_microLtoFill_3 = "133"; public const string GB_microLtoFill_4 = "134";
        public const string GB_microLtoFill_5 = "135";
        public const string GB_microLtoFill_6 = "136";

        public const string GB_137 = "137";
        public const string GB_138 = "138";
        public const string GB_139 = "139";

        public const string GB_FillingIsNeeded = "140";

        // public const string GB_141 = "141";
        // public const string GB_142 = "142";
        // public const string GB_143 = "143";

        public const string GB_Filling1IsNeeded = "141"; //// L.E.D //// for possible future use
        public const string GB_Filling2IsNeeded = "142"; //// L.E.D //// for possible future use
        public const string GB_Filling3IsNeeded = "143"; //// L.E.D //// for possible future use

        public const string GB_Filling4IsNeeded = "144";
        public const string GB_Filling5IsNeeded = "145";
        public const string GB_Filling6IsNeeded = "146";

        public const string GB_147 = "147";
        public const string GB_148 = "148";
        public const string GB_149 = "149";
        public const string GB_150 = "150";
        public const string GB_151 = "151";
        public const string GB_152 = "152";
        public const string GB_153 = "153";
        public const string GB_154 = "154";
        public const string GB_155 = "155";
        public const string GB_156 = "156";
        public const string GB_157 = "157";
        public const string GB_158 = "158";
        public const string GB_159 = "159";

        // current syringe
        // public const string GB_160 = "160";
        public const string GB_Max_Volume_current = "160";
        public const string GB_microL_per_100mm_current = "161";
        public const string GB_Syring_Length_current = "162";
        // public const string GB_162 = "162";

        public const string GB_163 = "163";
        public const string GB_164 = "164";
        public const string GB_165 = "165";
        public const string GB_166 = "166";
        public const string GB_167 = "167";
        public const string GB_168 = "168";
        public const string GB_169 = "169";
        public const string GB_170 = "170";
        public const string GB_171 = "171";
        public const string GB_172 = "172";
        public const string GB_173 = "173";
        public const string GB_174 = "174";
        public const string GB_175 = "175";
        public const string GB_176 = "176";
        public const string GB_177 = "177";
        public const string GB_178 = "178";
        public const string GB_179 = "179";
        public const string GB_180 = "180";
        public const string GB_181 = "181";

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        public const string GB_GoToAbsoluteInSteps = "182";
        public const string GB_numberOfKicks = "183";              //  10
        public const string GB_rotateBubblesUM = "184";            // 750 = 0.75mm
        public const string GB_pistonBubblesPullMicroL = "185";    // 300 * microL = 0.3ml
        public const string GB_pistonBubblesPushMicroL = "186";    // 150 * microL = 0.3ml

        public const string GB_dispenseCycleTime01s = "187";       // calculates the time of the cycle evry 0.1 s
        public const string GB_dispenseCycleTimeMS = "188";        // calculates the time of the cycle evry ms

        public const string GB_189 = "189";

        public const string GB_PigWasReplaced = "190";
        public const string GB_inHomeCapHolderMotor = "191";
        public const string GB_MulCenterOfVial = "192";            // for running average calculations
        public const string GB_adjustmentsTotal = "193";

        public const string GB_194 = "194";

        public const string GB_TouchedLeftRef = "195";
        public const string GB_dipperInterruptHight = "196";

        // public const string GB_197 = "197";
        // public const string GB_198 = "198";
        // public const string GB_199 = "199";
        // public const string GB_200 = "200";
        // public const string GB_201 = "201";
        // public const string GB_202 = "202";
        // public const string GB_203 = "203";
        public const string GB_microLinVial_1 = "197";
        public const string GB_microLinVial_2 = "198";
        public const string GB_microLinVial_3 = "199";
        public const string GB_microLinVial_4 = "200";
        public const string GB_microLinVial_5 = "201";
        public const string GB_microLinVial_6 = "202";
        public const string GB_microLinVial_current = "203";

        public const string GB_current_Vial = "204";

        public const string GB_205 = "205";
        public const string GB_206 = "206";
        public const string GB_207 = "207";
        public const string GB_208 = "208";
        public const string GB_209 = "209";
        public const string GB_210 = "210";


        public const string GB_vial1Bit = "211";  //  VIAL1_BIT
        public const string GB_vial2Bit = "212";  //  VIAL2_BIT
        public const string GB_vial3Bit = "213";  //  VIAL3_BIT
        public const string GB_vial4Bit = "214";  //  VIAL4_BIT
        public const string GB_vial5Bit = "215";  //  VIAL5_BIT
        public const string GB_vial6Bit = "216";  //  VIAL6_BIT
        public const string GB_BagBit = "217";  //  BAG_BIT
        public const string GB_vialsExist = "218";
        /* 
            Bit_vial1      =  %00000001 // bit   1
            Bit_vial2      =  %00000010 // bit   2
            Bit_vial3      =  %00000100 // bit   4
            Bit_vial4      =  %00001000 // bit   8
            Bit_vial5      =  %00010000 // bit  16
            Bit_vial6      =  %00100000 // bit  32
            Bit_bag        =  %01000000 // bit  64
        */

        public const string GB_219 = "219";
        public const string GB_220 = "220";
        public const string GB_221 = "221";

        public const string GB_222 = "222";

        //public const string GB_currentVersion = "222";

        public const string GB_223 = "223";

        public const string GB_readyToDraw = "224";
        public const string GB_initialVolume = "225";
        public const string GB_MotorIsMoving = "226";

        public const string GB_227 = "227";

        public const string GB_HomingDone = "228";
        public const string GB_UnitsToMoveManual = "229";

        public const string GB_TempNumHolder = "230";

        public const string GB_airToPullBefore = "231";

        public const string GB_232 = "232";
        public const string GB_233 = "233";
        public const string GB_234 = "234";

        public const string GB_motorNumForHome = "235";

        public const string GB_236 = "236";

        public const string GB_microLbagToFill = "237";

        public const string GB_238 = "238";
        public const string GB_239 = "239";
        public const string GB_240 = "240";
        // ----------- shakes and tilts - to replace the vibrators ------------ //
        ///////////                                                       // -- //
        // Tilts //                                                       // -- //
        ///////////                                                       // -- //
        public const string GB_axisAccelerationForTilts      = "241";     // -- //
        public const string GB_axisCurrentForTilts           = "242";     // -- //
        public const string GB_axisSpeedForTilts             = "243";     // -- //
        // -------------------------------------                          // -- //
        public const string GB_numberOfTilts                 = "244";     // -- //
        public const string GB_tiltsCounter                  = "245";     // -- //
        ////////////                                                      // -- //
        // Shakes //                                                      // -- //
        ////////////                                                      // -- //
        public const string GB_axisAccelerationForShakes     = "246";     // -- //
        public const string GB_axisCurrentForShakes          = "247";     // -- //
        public const string GB_axisSpeedForShakes            = "248";     // -- //
        // -------------------------------------                          // -- //
        public const string GB_numberOfShakes                = "249";     // -- //
        public const string GB_shakesCounter                 = "250";     // -- //
        // -------------------------------------                          // -- //
        public const string GB_shakeTravelDistance           = "251";     // -- //
        // -------------------------------------------------------------------- //
        // -------------------------------------------------------------------- //

        //public const string GB_241 = "241";
        //public const string GB_242 = "242";
        //public const string GB_243 = "243";
        //public const string GB_244 = "244";
        //public const string GB_245 = "245";
        //public const string GB_246 = "246";
        //public const string GB_247 = "247";
        //public const string GB_248 = "248";
        //public const string GB_249 = "249";

        //public const string GB_250 = "250";
        //public const string GB_251 = "251";

        //public const string GB_skipCheckVial456 = "250";             // temorary variable 5
        //public const string GB_skipCheckBag = "251";
        public const string GB_foundCenterOfNeedle = "252";
        public const string GB_DrawFromVialWaitTime = "253";
        public const string GB_vialsToDrawFromCounter = "254";

        // public const string GB_255 = "255";
        public const string GB_InterruptCount = "255";


    }
    public static class GeneralFunctions
    {
        public const string FIRST_RUN = "0";                      //Runs on power on
        public const string Draw_654321 = "2";      // for testing
        public const string Eyals_dummy_sub = "4";
        public const string INIT_CM = "6";
        public const string DRAW_DOSE = "8";
        public const string dropVials = "10";
        public const string ABORT = "12";
        public const string VERIFY_READY_DRAW = "14"; //  checks if the vial' syrine ready to draw
        public const string INIT_MOTORS = "16";
        public const string FIND_HOMES = "18";
        public const string HomeCalibration = "20";
        public const string LEDS_OFF = "22";
        public const string RED_ON = "24";
        public const string GREEN_ON = "26";
        public const string YELLOW_ON = "28";
        public const string positionVerticalMotor = "30";
        public const string initInterrups = "32";
        public const string verifyVIAL = "34";
        public const string screenAllVials = "36";
        public const string PositionHeadRotateToLeftMotor = "38";
        public const string verticalMotorTOerr = "40";
        public const string homeLinearMotor = "42";
        public const string capHolderMotorTOerr = "44";
        public const string pistonMotorTOerr = "46";
        public const string headRotateMotorTOerr = "48";
        public const string homeArmMotor = "50";
        public const string HomeDisposeMotor = "52";
        public const string incrementCycles = "54";
        public const string positionLinearMotor = "56";
        public const string homeHeadRotateMotor = "58";
        public const string checkSyringeSensor = "60";
        public const string checkNoSyringe = "62";
        public const string PositionHeadRotateToTopMotor = "64";
        public const string checkSyrPoppedOut = "66";
        public const string moveAboveVial = "68"; // func_68:   CSUB moveAboveVialFromCenter
        public const string PositionHeadRotateToBottomMotor = "70";
        public const string startPullAir70 = "72";
        public const string deCap = "74";
        public const string moveBelowVial = "76";
        public const string insertNeedleIntoVial = "78";
        public const string push70air = "80";
        public const string drawVial = "82";
        public const string ClearRunningErrors = "84";
        public const string moveSlowlyBottom = "86";
        public const string bumpPlunger = "88";
        public const string recapSyringe = "90";
        public const string startHomeDisposeMotor = "92";
        public const string insertNeedleIntoVialBelow = "94";
        public const string positionArmMotor = "96";
        public const string setTestParams = "98";
        public const string drawVialMoreBack = "100";
        public const string homeVerticalMotor = "102";
        public const string homePistonMotor = "104";
        public const string positionPistonMotor = "106";
        //public const string func_108 = "108";
        public const string ejectSyringeFromTopVial = "108";
        //public const string func_110 = "110";
        public const string ejectSyringeFromBottomVial = "110";
        public const string goIntoBag = "112";
        public const string Vibrate = "114";
        public const string checkVialPoppedOut = "116";
        public const string holdCap = "118";
        public const string stopVibrate = "120";
        public const string homeCapHolderMotor = "122";
        public const string startHomePistonMotor = "124";
        public const string defaultVibrate = "126";
        public const string getLaserDistAVAL = "128";
        public const string moveToLDcalibLocation = "130";
        public const string moveFromLDcalibLocation = "132";
        public const string func_134 = "134";
        public const string VerticalManual = "136";
        public const string LinearMotorManual = "138";
        public const string armMotorManual = "140";
        public const string PistonManual = "142";
        public const string RotationManual = "144";
        public const string VerticalManualAbsolute = "146";
        public const string LinearMotorManualAbsolute = "148";
        public const string armMotorManualAbsolute = "150";
        public const string PistonManualAbsolute = "152";
        public const string RotationManualAbsolute = "154";
        public const string DisposeManualAbsolute = "156";
        public const string DisposeManual = "158";
        public const string CapHolderManual = "160";
        public const string testCapHolder = "162";
        public const string draw_321 = "164";
        public const string checkSyringeContent = "166";
        public const string func_168 = "168";
        public const string func_170 = "170";
        public const string func_172 = "172";
        public const string Tilt = "174";
        
        public const int lastFunction = 256;
    }
}
