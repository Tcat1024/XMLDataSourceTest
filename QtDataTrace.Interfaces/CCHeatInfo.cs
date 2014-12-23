using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QtDataTrace.Interfaces
{
    
    [System.SerializableAttribute]
    public class CCHeatInfo
    {
        private string ccm;

        public string CCM
        {
            get { return ccm; }
            set { ccm = value; }
        }
        public string report_counter;
        public string task_counter;
        private string heat_id;

        public string Heat_id
        {
            get { return heat_id; }
            set { heat_id = value; }
        }
        public string po_id;
        public string area_id;
        public string station_code;
        private string steel_grade_id;

        public string Steel_grade_id
        {
            get { return steel_grade_id; }
            set { steel_grade_id = value; }
        }
        private string final_steel_grade_id;

        public string Final_steel_grade_id
        {
            get { return final_steel_grade_id; }
            set { final_steel_grade_id = value; }
        }
        public string alteration_reason_code;
        public string route_id;
        public string practice_id;
        public string ladle_id;
        public string ladle_life;
        public string ladle_tare_wgt;
        private string start_date;

        public string Start_date
        {
            get { return start_date; }
            set { start_date = value; }
        }
        private string stop_date;

        public string Stop_date
        {
            get { return stop_date; }
            set { stop_date = value; }
        }
        private string start_wgt;

        public string Start_wgt
        {
            get { return start_wgt; }
            set { start_wgt = value; }
        }
        private string final_wgt;

        public string Final_wgt
        {
            get { return final_wgt; }
            set { final_wgt = value; }
        }
        public string tapped_wgt;
        public string start_slag_wgt;
        public string final_slag_wgt;
        public string final_temp;
        public string task_note;
        public string team_id;
        public string foreman_id;
        public string shift_code;
        public string shift_responsible;
        public string scheduled_start_date;
        public string hot_heel;
        public string avgel1current;
        public string avgel2current;
        public string avgel3current;
        public string avgactpower;
        public string tap_to_tap;
        public string heat_notes;
        public string l3_heat_id;
        public string profile_model;
        public string eaf_electrode_consumption;
        private string liquidus_temp;

        public string Liquidus_temp
        {
            get { return liquidus_temp; }
            set { liquidus_temp = value; }
        }


        public string ladle_arrival_wgt;
        public string seq_counter;
        public string seq_heat_counter;
        public string seq_total_heats;
        public string seq_sched_heats;
        public string ladle_turret_arm_code;
        private string ladle_arrival_date;

        public string Ladle_arrival_date
        {
            get { return ladle_arrival_date; }
            set { ladle_arrival_date = value; }
        }
        private string ladle_opening_date;

        public string Ladle_opening_date
        {
            get { return ladle_opening_date; }
            set { ladle_opening_date = value; }
        }
        private string ladle_close_date;

        public string Ladle_close_date
        {
            get { return ladle_close_date; }
            set { ladle_close_date = value; }
        }
        private string ladle_opening_wgt;

        public string Ladle_opening_wgt
        {
            get { return ladle_opening_wgt; }
            set { ladle_opening_wgt = value; }
        }
        private string ladle_close_wgt;

        public string Ladle_close_wgt
        {
            get { return ladle_close_wgt; }
            set { ladle_close_wgt = value; }
        }
        public string tundish_id;
        public string tundish_life;
        public string tundish_car_code;
        public string tundish_preheat_time;
        private string tundish_preheat_temperature;

        public string Tundish_preheat_temperature
        {
            get { return tundish_preheat_temperature; }
            set { tundish_preheat_temperature = value; }
        }
        public string tundish_at_ladle_open_wgt;
        public string tundish_skull_wgt;
        public string tundish_powder_type;
        public string tundish_powder_wgt;
    }

    [System.SerializableAttribute]
    public class CC_SlabInfo
    {
        private string slab_no;

        public string Slab_no
        {
            get { return slab_no; }
            set { slab_no = value; }
        }
        private string heat_ID;

        public string Heat_ID
        {
            get { return heat_ID; }
            set { heat_ID = value; }
        }
        private string steel_grade;

        public string Steel_grade
        {
            get { return steel_grade; }
            set { steel_grade = value; }
        }
        private string ccm;

        public string CCM
        {
            get { return ccm; }
            set { ccm = value; }
        }
        private string strand_no;

        public string Strand_no
        {
            get { return strand_no; }
            set { strand_no = value; }
        }
        private string prod_counter;

        public string Prod_counter
        {
            get { return prod_counter; }
            set { prod_counter = value; }
        }
        private string prod_no;

        public string Prod_no
        {
            get { return prod_no; }
            set { prod_no = value; }
        }
        private string width;

        public string Width
        {
            get { return width; }
            set { width = value; }
        }
        private string width_head;

        public string Width_head
        {
            get { return width_head; }
            set { width_head = value; }
        }
        private string width_tail;

        public string Width_tail
        {
            get { return width_tail; }
            set { width_tail = value; }
        }
        private string thickness;

        public string Thickness
        {
            get { return thickness; }
            set { thickness = value; }
        }
        private string taper_start;

        public string Taper_start
        {
            get { return taper_start; }
            set { taper_start = value; }
        }
        private string taper_end;

        public string Taper_end
        {
            get { return taper_end; }
            set { taper_end = value; }
        }
        private string length;

        public string Length
        {
            get { return length; }
            set { length = value; }
        }
        private string weight;

        public string Weight
        {
            get { return weight; }
            set { weight = value; }
        }
        private string start_time;

        public string Start_time
        {
            get { return start_time; }
            set { start_time = value; }
        }
        private string stop_time;

        public string Stop_time
        {
            get { return stop_time; }
            set { stop_time = value; }
        }
        private string start_cast_pos;

        public string Start_cast_pos
        {
            get { return start_cast_pos; }
            set { start_cast_pos = value; }
        }
        private string stop_cast_pos;

        public string Stop_cast_pos
        {
            get { return stop_cast_pos; }
            set { stop_cast_pos = value; }
        }
        private string sample_wgt;

        public string Sample_wgt
        {
            get { return sample_wgt; }
            set { sample_wgt = value; }
        }
        private string defect_level;

        public string Defect_level
        {
            get { return defect_level; }
            set { defect_level = value; }
        }
        private string manual_report_flg;

        public string Manual_report_flg
        {
            get { return manual_report_flg; }
            set { manual_report_flg = value; }
        }
        private string manual_cut_flg;

        public string Manual_cut_flg
        {
            get { return manual_cut_flg; }
            set { manual_cut_flg = value; }
        }
        private string cut_date;

        public string Cut_date
        {
            get { return cut_date; }
            set { cut_date = value; }
        }
        private string weight_real;

        public string Weight_real
        {
            get { return weight_real; }
            set { weight_real = value; }
        }
    }

    [System.SerializableAttribute]
    public class CC_HisData0
    {
        public DateTime datetime {get; set;}
        public float Duration { get; set; }
        public float CastingLength { get; set; }
        public float CastingSpeed { get; set; }
        public float CastingSupperHeatValue { get; set; }
        public float CastingTempture { get; set; }
        public float LD_WEIGHT { get; set; }
        public float MD_LEVAL { get; set; }
        public float MD_LEVAL_DEV { get; set; }
        public float MD_SEN_Immersion { get; set; }
        public float MEMS_Current { get; set; }
        public float MEMS_Frequency { get; set; }
        public float NOZZLE_AR_FLUX { get; set; }
        public float TD_WEIGHT { get; set; }
    }

    public class CC_MDCoolHisDB
    {   //结晶器冷却过程参数
        public DateTime DateAndTime;
        public float Duration;

        public float CoolWaterInTempure;//MDCW_INLET_TEMP 结晶器冷却水入口温度

        public float CoolWaterFlux_Fix;// MD_Fix_face_water_flow 结晶器冷却水流量(固定面)
        public float CoolWaterFlux_Loose;//MD_Loose_face_water_flow;//	结晶器冷却水流量(松开面)
        public float CoolWaterFlux_Right;//	MD_Right_face_water_flow 结晶器冷却水流量(右侧)
        public float CoolWaterFlux_Left;//	MD_Left_face_water_flow结晶器冷却水流量(左侧)

        public float CoolWaterDiffTemp_Fix;//  MD_Fix_face_water_delta_T;//	结晶器冷却水温差(固定面)
        public float CoolWaterDiffTemp_Loose;//MD_Loose_face_water_delta_T;//	结晶器冷却水温差(松散面)
        public float CoolWaterDiffTemp_Right;// MD_Right_face_water_delta_T;//	结晶器冷却水温差(右侧)
        public float CoolWaterDiffTemp_Left;// MD_Left_face_water_delta_T;//	结晶器冷却水温差(左侧)
        
        public float HeatFlux_Fix;//MD_LEFT_FACE_EXTRACT;//	结晶器冷却：固定面热流密度
        public float HeatFlux_Loose;//MD_LOSE_FACE_EXTRACT;//	结晶器冷却：松散面热流密度
        public float HeatFlux_Right;//MD_RIGHT_FACE_EXTRACT	;//结晶器冷却：右侧面热流密度
        public float HeatFlux_Left;//MD_FIX_FACE_EXTRACT;//结晶器冷却：左侧面热流密度        
    }

}
