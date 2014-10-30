using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
     [System.SerializableAttribute]
    public class RHHeatInfo
    {
        public string treatpos {get; set;}
        public string msg_status { get; set; }
        public string msg_datetime { get; set; }
        public string read_time { get; set; }
        public string pono { get; set; }
        public string rh_treatment_id { get; set; }
        public string heat_id { get; set; }
        public string plan_no { get; set; }
        public string crew_id { get; set; }
        public string shift_id { get; set; }
        public string steel_grade { get; set; }
        public string srp_count { get; set; }
        public string div_flag { get; set; }
        public string arrive_mainpos_time { get; set; }
        public string hook_arr_ladle_time { get; set; }
        public string ladle_up_time { get; set; }
        public string heat_start { get; set; }
        public string heat_end { get; set; }
        public string ladle_down_time { get; set; }
        public string hook_leave_ladle_time { get; set; }
        public string arr_bwz_time { get; set; }
        public string arr_departpos_time { get; set; }
        public string depart_time { get; set; }
        public string dati_temp_1 { get; set; }
        public string steel_temp_1 { get; set; }
        public string o2_activity_1 { get; set; }
        public string dati_temp_2 { get; set; }
        public string steel_temp_2 { get; set; }
        public string o2_activity_2 { get; set; }
        public string dati_temp_3 { get; set; }
        public string steel_temp_3 { get; set; }
        public string o2_activity_3 { get; set; }
        public string dati_temp_4 { get; set; }
        public string steel_temp_4 { get; set; }
        public string o2_activity_4 { get; set; }
        public string dati_temp_5 { get; set; }
        public string steel_temp_5 { get; set; }
        public string o2_activity_5 { get; set; }
        public string dati_sample_1 { get; set; }
        public string dati_sample_2 { get; set; }
        public string dati_sample_3 { get; set; }
        public string dati_sample_4 { get; set; }
        public string dati_sample_5 { get; set; }
        public string dati_o2_start { get; set; }
        public string dati_o2_end { get; set; }
        public string o2_dur { get; set; }
        public string o2_cons { get; set; }
        public string dati_des_start { get; set; }
        public string dati_des_end { get; set; }
        public string des_dur { get; set; }
        public string des_gas_cons { get; set; }
        public string ar_lift_cons { get; set; }
        public string n2_lift_cons { get; set; }
        public string steam_cons { get; set; }
        public string stir_ar_start { get; set; }
        public string stir_ar_end { get; set; }
        public string stir_ar_dur { get; set; }
        public string soft_stir_ar_dur { get; set; }
        public string ar_bb_cons { get; set; }
        public string ladle_id { get; set; }
        public string begin_slag_height { get; set; }
        public string begin_slag_weight { get; set; }
        public string begin_net_weight { get; set; }
        public string end_net_weight { get; set; }
        public string vac_dur { get; set; }
        public string treat_time { get; set; }
        public string min_vacuum { get; set; }
        public string machine_cooling_water_cons { get; set; }
        public string condensor_cooling_water_cons { get; set; }
        public string lance_no { get; set; }
        public string lance_age { get; set; }
        public string hot_bend_tube_no { get; set; }
        public string hot_bend_tube_num { get; set; }
        public string vacuum_slot_no { get; set; }
        public string vacuum_slot_num { get; set; }
        public string updown_num { get; set; }
        public string ladle_tare_wt { get; set; }
 
    }

    [System.SerializableAttribute]
    public class RH_KeyEvens
    {
        public string DateAndTime { get; set; }
        public float Duration { get; set; }
        public string Decription { get; set; }
        public string Value { get; set; }
        public string MatID { get; set; }
        public string MatCode { get; set; }
        public string Weight { get; set; }
        public string Temp { get; set; }
        public string O2ppm { get; set; }

        public string Ele_Als { get; set; }
        public string Ele_Alt { get; set; }
        public string Ele_As { get; set; }
        public string Ele_B { get; set; }

        public string Ele_Bi { get; set; }
        public string Ele_C { get; set; }
        public string Ele_Ca { get; set; }
        public string Ele_Ce { get; set; }
        public string Ele_Ceq { get; set; }

        public string Ele_Co { get; set; }
        public string Ele_Cr { get; set; }
        public string Ele_Cu { get; set; }
        public string Ele_Mg { get; set; }
        public string Ele_Mn { get; set; }

        public string Ele_Mo { get; set; }
        public string Ele_N { get; set; }
        public string Ele_Nb { get; set; }
        public string Ele_Ni { get; set; }
        public string Ele_P { get; set; }

        public string Ele_Pb { get; set; }
        public string Ele_S { get; set; }
        public string Ele_Sb { get; set; }
        public string Ele_Si { get; set; }
        public string Ele_Sn { get; set; }

        public string Ele_Ti { get; set; }
        public string Ele_V { get; set; }
        public string Ele_W { get; set; }
        public string Ele_Zr { get; set; }

    }

    [Serializable]
    public class RH_HisDB
    {
        public DateTime datetime { get; set; }
        public float Duration { get; set; }
        public float VacuumValue { get; set; }
        public float CycArFlowLift { get; set; }
        public float CycBlowingO2Flow { get; set; }
        public float FluxAr { get; set; }
        public float FluxN2 { get; set; }
        public float FluxO2 { get; set; }
        public float FlueGasAr { get; set; }
        public float FlueGasCO { get; set; }
        public float FlueGasCO2 { get; set; }
        public float FlueGasFlux { get; set; }
        public float FlueGasH2 { get; set; }
        public float ProcVolPlug1Stir { get; set; }
        public float ProcVolPlug2Stir { get; set; }
    }
}
