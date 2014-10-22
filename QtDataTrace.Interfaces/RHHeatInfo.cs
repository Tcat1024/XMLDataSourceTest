using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
     [System.SerializableAttribute]
    public class RHHeatInfo
    {
        public string treatpos;
        public string msg_status;
        public string msg_datetime;
        public string read_time;
        public string pono;
        public string rh_treatment_id;
        public string heat_id;
        public string plan_no;
        public string crew_id;
        public string shift_id;
        public string steel_grade;
        public string srp_count;
        public string div_flag;
        public string arrive_mainpos_time;
        public string hook_arr_ladle_time;
        public string ladle_up_time;
        public string heat_start;
        public string heat_end;
        public string ladle_down_time;
        public string hook_leave_ladle_time;
        public string arr_bwz_time;
        public string arr_departpos_time;
        public string depart_time;
        public string dati_temp_1;
        public string steel_temp_1;
        public string o2_activity_1;
        public string dati_temp_2;
        public string steel_temp_2;
        public string o2_activity_2;
        public string dati_temp_3;
        public string steel_temp_3;
        public string o2_activity_3;
        public string dati_temp_4;
        public string steel_temp_4;
        public string o2_activity_4;
        public string dati_temp_5;
        public string steel_temp_5;
        public string o2_activity_5;
        public string dati_sample_1;
        public string dati_sample_2;
        public string dati_sample_3;
        public string dati_sample_4;
        public string dati_sample_5;
        public string dati_o2_start;
        public string dati_o2_end;
        public string o2_dur;
        public string o2_cons;
        public string dati_des_start;
        public string dati_des_end;
        public string des_dur;
        public string des_gas_cons;
        public string ar_lift_cons;
        public string n2_lift_cons;
        public string steam_cons;
        public string stir_ar_start;
        public string stir_ar_end;
        public string stir_ar_dur;
        public string soft_stir_ar_dur;
        public string ar_bb_cons;
        public string ladle_id;
        public string begin_slag_height;
        public string begin_slag_weight;
        public string begin_net_weight;
        public string end_net_weight;
        public string vac_dur;
        public string treat_time;
        public string min_vacuum;
        public string machine_cooling_water_cons;
        public string condensor_cooling_water_cons;
        public string lance_no;
        public string lance_age;
        public string hot_bend_tube_no;
        public string hot_bend_tube_num;
        public string vacuum_slot_no;
        public string vacuum_slot_num;
        public string updown_num;
        public string ladle_tare_wt;
 
    }

    [System.SerializableAttribute]
    public class RH_KeyEvens
    {
        public string DateAndTime;
        public float Duration;
        public string Decription;
        public string Value;
        public string MatID;
        public string MatCode;
        public string Weight;
        public string Temp;
        public string O2ppm;
            
         public string Ele_Als;
         public string Ele_Alt;
         public string Ele_As;
         public string Ele_B;

         public string Ele_Bi;
         public string Ele_C;
         public string Ele_Ca;
         public string Ele_Ce;
         public string Ele_Ceq;

         public string Ele_Co;
         public string Ele_Cr;
         public string Ele_Cu;
         public string Ele_Mg;
         public string Ele_Mn;

         public string Ele_Mo;
         public string Ele_N;
         public string Ele_Nb;
         public string Ele_Ni;
         public string Ele_P;

         public string Ele_Pb;
         public string Ele_S;
         public string Ele_Sb;
         public string Ele_Si;
         public string Ele_Sn;

         public string Ele_Ti;
         public string Ele_V;
         public string Ele_W;
         public string Ele_Zr;

    }
    public class RH_HisDB
    {
        public DateTime datetime;
        public float Duration;
        public float VacuumValue;
        public float CycArFlowLift;
        public float CycBlowingO2Flow;
        public float FluxAr;
        public float FluxN2;
        public float FluxO2;
        public float FlueGasAr;
        public float FlueGasCO;
        public float FlueGasCO2;
        public float FlueGasFlux;
        public float FlueGasH2;
        public float ProcVolPlug1Stir;
        public float ProcVolPlug2Stir;
    }
}
