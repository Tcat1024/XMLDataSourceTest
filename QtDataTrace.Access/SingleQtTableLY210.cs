using QtDataTrace.Interfaces;
using System; 
using System.Collections.Generic; 
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq; 
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using DuHisPic;

namespace QtDataTrace.Access
{
    public class SingleQtTableLY210
    {
        //对于起初编辑的类的引用
        SingleQtTable sqt = new SingleQtTable();

        //生成Pdf文件时要用的两种字体
        private iTextSharp.text.Font FontHei;
        private iTextSharp.text.Font FontSong ;
        private iTextSharp.text.Font FontKai;
        //默认的背景颜色
        private iTextSharp.text.Color color_Name = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
        private iTextSharp.text.Color color_Value = new iTextSharp.text.Color(System.Drawing.Color.White);

        //绘图引用的
        DuHisPic.ClsDuHisPic clsDuHisPic = new DuHisPic.ClsDuHisPic();

        //pdf报告的封面
        public void GetPdfReport_Face(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //封面有默认页
            //pdfDocument.newPage();
            
            //读取相关信息            
            HEAT_TRACK lst =GetHeatTrack(HeatID);                    

            //该炉号对应的铸坯
            List<string> LST_Slab = GetSlabIDListFromHeatID(HeatID);
             
            //表头
            //倘若没有定义字体，就去定义一下
            if (null == FontHei) DefinePdfFont();
            FontHei.Size=20;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph("质量数据追溯", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;            
            pdfDocument.Add(para);

            FontKai.Size = 18;
            para = new iTextSharp.text.Paragraph("(冶炼与连铸)", FontKai);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            pdfDocument.Add(new Paragraph(""));//空行

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(5);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 20, 20,20,20,20 }; // 百分比
            table.setWidths(headerwidths);            
            table.AutoFillEmptyCells = true;            
            //设置边框颜色为白色，即不显示
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            FontHei.Size = 12; FontSong.Size = 12;
            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontHei)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(HeatID, FontSong)); cell.Colspan = 4;
            table.addCell(cell);
             

            cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontHei));table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lst.SteelGrade, FontSong)); cell.Colspan = 4; table.addCell(cell);

            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(220, 220, 220);
            FontKai.Size = 12;
            cell = new iTextSharp.text.Cell(new Paragraph("工序", FontKai));table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("站号", FontKai));table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("进站时间", FontKai)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("离站时间", FontKai)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("在站时长(分钟)", FontKai));table.addCell(cell);

            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(255, 255, 255);
            cell = new iTextSharp.text.Cell(new Paragraph("混铁炉", FontHei));            
            table.addCell(cell);
            if (null != lst.MI_Station)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst.MI_Station, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.MI_Arrive_Time.ToString(), FontSong));table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.MI_Leave_Time.ToString(), FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.LF_Duration, FontSong));table.addCell(cell);
            }
            else
            {
                table.addCell("-"); table.addCell("-"); table.addCell("-"); table.addCell("-");
            }

            cell = new iTextSharp.text.Cell(new Paragraph("脱硫站", FontHei));            
            table.addCell(cell);
            if (lst.KR_Station.Length > 1)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst.KR_Station, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.KR_Arrive_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.KR_Leave_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.KR_Duration , FontSong));table.addCell(cell);
            }
             else
             {
                 table.addCell("-"); table.addCell("-"); table.addCell("-"); table.addCell("-");
             }

            cell = new iTextSharp.text.Cell(new Paragraph("转炉", FontHei));table.addCell(cell);
            if (lst.BOF_Station.Length > 1)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst.BOF_Station, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.BOF_Arrive_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.BOF_Leave_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.BOF_Duration, FontSong)); table.addCell(cell);
            }
            else
            {
                table.addCell("-"); table.addCell("-"); table.addCell("-"); table.addCell("-");
            }
           
            cell = new iTextSharp.text.Cell(new Paragraph("精炼炉", FontHei));            
            table.addCell(cell);
            if (lst.LF_Station.Length > 1)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst.LF_Station, FontSong));table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph(lst.LF_Arrive_Time, FontSong));table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.LF_Leave_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.LF_Duration , FontSong));table.addCell(cell);                
                
            }
            else
            {
                table.addCell("-"); table.addCell("-"); table.addCell("-"); table.addCell("-");
            }

            cell = new iTextSharp.text.Cell(new Paragraph("真空炉", FontHei));            
            table.addCell(cell);
            if (lst.RH_Station.Length > 1)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst.RH_Station, FontSong));table.addCell(cell);                 
                cell = new iTextSharp.text.Cell(new Paragraph(lst.RH_Arrive_Time, FontSong));table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.RH_Leave_Time, FontSong)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph(lst.RH_Duration , FontSong));table.addCell(cell);                 
             }
            else
            {
                table.addCell("-"); table.addCell("-"); table.addCell("-"); table.addCell("-");
            }

            cell = new iTextSharp.text.Cell(new Paragraph("连铸", FontHei)); table.addCell(cell);
            if (lst.CC_Station.Length > 0)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst.CC_Station, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.CC_Arrive_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.CC_Leave_Time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst.CC_Duration, FontSong)); table.addCell(cell);
            }
            else
            {
                table.addCell("-"); table.addCell("-"); table.addCell("-"); table.addCell("-");
            }

            cell = new iTextSharp.text.Cell(new Paragraph("生成铸坯", FontHei));            
            table.addCell(cell);
            if (LST_Slab.Count >= 1)
            {
                para = new iTextSharp.text.Paragraph(LST_Slab[0], FontHei);
                for (int I = 1; I < LST_Slab.Count; I++)
                {
                    para.Add(";" + LST_Slab[I]);
                }
                cell = new iTextSharp.text.Cell(para);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Colspan = 4;                
                table.addCell(cell);
            }
            else
            {
                cell = new iTextSharp.text.Cell("");
                cell.Colspan = 4;
                table.addCell(cell);
            }
           
            //把表加入
            pdfDocument.Add(table);


            //结尾
            //pdfDocument.Add(new Paragraph(" ", FontHei));//空行
            string str = "单位:             "
                        +"签章:              "
                        + "日期:"+DateTime.Now.ToString ("yyyy年MM月dd日 hh:mm:ss") ;
            para = new iTextSharp.text.Paragraph(str, FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            GetPdfReport_Face_Addition(HeatID, ref  pdfDocument);
            GetPdfReport_Face_Tempture(HeatID, ref  pdfDocument);
            GetPdfReport_Face_Elemants(HeatID, ref  pdfDocument);


        }

         private void GetPdfReport_Face_Addition(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
             //新开一个页面
            pdfDocument.newPage();            

            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 冶炼流程加料表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);            

             //以表形式来写入必须的信息
             iTextSharp.text.Table table = new iTextSharp.text.Table(9);
             iTextSharp.text.Cell cell;
             table.WidthPercentage = 100; // 百分比
             table.Padding = 5;
             int[] headerwidths = { 10, 5, 15, 5,10,10 ,5,5,8}; // 百分比
             table.setWidths(headerwidths);
             table.AutoFillEmptyCells = true;
             //设置边框颜色 
             table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
             table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

             table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
             table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
             FontKai.Size = 9; FontSong.Size = 9;

             //表头
             table.DefaultCellBackgroundColor = color_Name;
             cell = new iTextSharp.text.Cell(new Paragraph("工序", FontKai)); table.addCell(cell);
			 cell = new iTextSharp.text.Cell(new Paragraph("工位" , FontKai)); table.addCell(cell);				        
			cell = new iTextSharp.text.Cell(new Paragraph("时间" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("批次" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("物料ID" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("物料名称" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("重量" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("料仓号" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("投放位置" , FontKai)); table.addCell(cell);
            
             // 表格标题结束
            table.endHeaders();

            //数据
            table.DefaultCellBackgroundColor = color_Value;
            List<Addition> lst = GetAddition(HeatID);
            for  (int I=0;I< lst.Count ;I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].DEVICE_NO, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].STATION, FontSong)); table.addCell(cell);				        
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ADD_TIME, FontSong)); table.addCell(cell);                
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ADD_BATCH, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MAT_ID, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MAT_NAME, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].WEIGHT, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].HOPPER_ID, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].PLACE, FontSong)); table.addCell(cell);  
            }
            pdfDocument.Add(table);
        }
        private void GetPdfReport_Face_Tempture(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 冶炼流程测温表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(6);
            iTextSharp.text.Cell cell;
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 15, 20, 15, 15 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;

            //表头
            table.DefaultCellBackgroundColor = color_Name;
            cell = new iTextSharp.text.Cell(new Paragraph("工序", FontKai)); table.addCell(cell);          
			cell = new iTextSharp.text.Cell(new Paragraph("工位", FontKai)); table.addCell(cell);						       
			cell = new iTextSharp.text.Cell(new Paragraph("测量类型", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("测量时间", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("测量次序", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("温度,℃" , FontKai)); table.addCell(cell);
            // 表格标题结束
            table.endHeaders();

            //数据
            table.DefaultCellBackgroundColor = color_Value;
            List<TEMPTURE> lst = GetTEMPTURE(HeatID);
            for (int I = 0; I < lst.Count; I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].DEVICE_NO, FontSong)); table.addCell(cell);                 
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].STATION, FontSong)); table.addCell(cell);					       
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MEASURE_TYPE, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MEASURE_TIME, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MEASURE_NUM, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].TRMPTURE_VALUE, FontSong)); table.addCell(cell);	
            }
            pdfDocument.Add(table);
        }
        
        private void GetPdfReport_Face_Elemants(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 冶炼流程元素化验表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(18);
            iTextSharp.text.Cell cell;
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = {10, 5,15, 5
                                , 5,5,5,5,5
                                , 5,5,5,5,5
                                , 5,5,5,5}; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;            
            FontKai.Size = 9; FontSong.Size = 9;

            //表头
            table.DefaultCellBackgroundColor = color_Name;
            cell = new iTextSharp.text.Cell(new Paragraph("工序", FontKai)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("工位", FontKai)); table.addCell(cell);          
			cell = new iTextSharp.text.Cell(new Paragraph("时间" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("次序" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("C" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Si" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Mn" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("S" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("P" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Ti" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Ca" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Als" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Alt" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Cr" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Ni" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Mo" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("Nb" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("V" , FontKai)); table.addCell(cell);
            // 表格标题结束
            table.endHeaders();

            //数据
            table.DefaultCellBackgroundColor = color_Value;
            List<ELEM_ANA> lst = GetELEM_ANA(HeatID);
            for (int I = 0; I < lst.Count; I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].DEVICE_NO, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].STATION, FontSong)); table.addCell(cell);                
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].SAMPLETIME, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].SAMPLE_NUMBER, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_C, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_SI, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_MN, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_S, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_P, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_TI, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_CA, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_ALS, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_ALT, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_CR, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_NI, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_MO, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_NB, FontSong)); table.addCell(cell);
				cell = new iTextSharp.text.Cell(new Paragraph(lst[I].ELE_V, FontSong)); table.addCell(cell); 	
            }
            pdfDocument.Add(table);
        }

        //获取混铁炉MI的pdf报告
        public void GetReportPdf_MI(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            FontHei.Size = 16;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 混铁炉工序数据追踪", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);
                        
            //现在找到了出铁的基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

             //以表形式来写入必须的信息
             iTextSharp.text.Table table = new iTextSharp.text.Table(6);
             table.WidthPercentage = 100; // 百分比
             table.Padding = 5;
             int[] headerwidths = { 13, 20, 13, 20, 14, 20 }; // 百分比
             table.setWidths(headerwidths);
             table.AutoFillEmptyCells = true;
             //设置边框颜色 
             table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
             table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

             table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
             table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

             FontKai.Size = 9; FontSong.Size = 9;
             iTextSharp.text.Cell cell;
            //这里开始写基本信息
            cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontKai)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(HeatID, FontSong));table.addCell(cell);
            

            //获取基本信息
            List<MIHeatInfo> lst = GetMIHeatInfo(HeatID);
            if (lst.Count > 0)
            {
                cell = new iTextSharp.text.Cell(new Paragraph("铁次", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].IronID, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("铁包号", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].IronLaddleID, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ShiftID, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);


                cell = new iTextSharp.text.Cell(new Paragraph("班别", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].CrewID, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("操作员", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Operator, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);
                
                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("出铁开始", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ChargeTime, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出铁重量,kg", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].HM_Weight, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出铁温度,℃", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].HM_Tempture, FontSong)); table.BackgroundColor = color_Value;  table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("出铁完毕", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].WeightTime, FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontSong)); table.BackgroundColor = color_Value; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); table.BackgroundColor = color_Name; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ")); table.BackgroundColor = color_Value; table.addCell(cell);
                 
            }
            //把表写入文档
            pdfDocument.Add(table);


            //****** 关键事件表 ******// 
            pdfDocument.Add(new Paragraph(" "));//空行
            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("历史铁水信息表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            table = new iTextSharp.text.Table(14);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 3;
            int[] HeaderWidths = { 10,10,5,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5 }; // 百分比
            table.setWidths(HeaderWidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            //这里开始写基本信息
            //**标题行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.LightGray);

            cell = new iTextSharp.text.Cell(new Paragraph("开始", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("结束" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("时长,min", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("进/出铁", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("炉号" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("铁次号" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("铁包号" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("高炉号" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("出铁口" , FontKai)); table.addCell(cell);       
			cell = new iTextSharp.text.Cell(new Paragraph("铁水重" , FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("混铁炉重" , FontKai)); table.addCell(cell);			        
			cell = new iTextSharp.text.Cell(new Paragraph("温度" , FontKai)); table.addCell(cell);     
			cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); table.addCell(cell);
			cell = new iTextSharp.text.Cell(new Paragraph("班别" , FontKai)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("操作员", FontKai)); table.addCell(cell);
             
            // 表格结束
            table.endHeaders();


            //数据行
            //读出数据
            List<MIKeyEvents> LST = GetMIKeyEvents(HeatID);
            
            for (int I=0;I<LST.Count;I++)
            {

                if ("出铁" == LST[I].IN_OUT)
                    table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.Yellow);
                else
                    table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                                
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].DateAndTime, FontSong)); table.addCell(cell);
					cell = new iTextSharp.text.Cell(new Paragraph(LST[I].STOP_TIME, FontSong)); table.addCell(cell);
					cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Duration.ToString ("#0.00"), FontSong)); table.addCell(cell);
					cell = new iTextSharp.text.Cell(new Paragraph(LST[I].IN_OUT, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].HEAT_ID, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].IRON_ID, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].IRON_LADLE_ID, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].BF_ID, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].BF_TAP_ID, FontSong)); table.addCell(cell);       
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].IRON_WEIGHT, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].MIXER_WEIGHT, FontSong)); table.addCell(cell);		        
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].TEMPTURE, FontSong)); table.addCell(cell);    
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Shift_ID, FontSong)); table.addCell(cell);
			        cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Crew_ID, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Operator, FontSong)); table.addCell(cell);

            }       

            //把表写入文档
             pdfDocument.Add(table);
        }

        //获取脱硫站KR的pdf报告
        public void GetReportPdf_KR(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {

            //新开一个页面
            pdfDocument.newPage();

            FontHei.Size = 16;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 脱硫站工序数据追踪", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //现在找到了的基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(8);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 15, 10, 15, 10, 15, 10, 15}; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);
            iTextSharp.text.Color Color_DescCell=   new iTextSharp.text.Color(System.Drawing.Color.LightGray);   
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(255, 255, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;
            

            //获取基本信息
            List<KRHeatInfo> lst =GetKRHeatInfo(HeatID);
            if (lst.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(HeatID, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("铁次", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].IRON_ID, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("脱硫站号", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].DES_STATION_NO, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("铁包号", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].IRON_LADLE_ID, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].SHIFT_ID, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("班别", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].CREW_ID, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STEEL_GRADE, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("目标[S]", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].AIM_S, FontSong)); table.addCell(cell);
                
                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("钢包到达", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].LADLE_ARRIVE, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢包离开", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].LADLE_LEAVE, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢包重量", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].LADLE_WEIGHT, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("搅拌器ID", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_ID, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("脱硫开始", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].DES_START, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("脱硫完毕", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].DES_END, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("脱硫时长,kg", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].DES_DURATION, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("脱硫次数", FontKai)); cell.BackgroundColor =Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].DES_STEP_NUM, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("进站温度,℃", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].INI_TEMP, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出站温度,℃", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].FIN_TEMP, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("进站重量", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].INI_WGT, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出站重量", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].FIN_WGT, FontSong)); table.addCell(cell);
               

                //新行
                table.DefaultCellBackgroundColor = Color_DescCell;
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌时长", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌次数", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌器高度平均", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌器高度最小", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌器高度最大", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌转速平均", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌转速最小", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("搅拌转速最大", FontKai)); table.addCell(cell);

                //新行
                table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);                
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_DURATION, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_TIMES, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_HEIGHT_AVG, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_HEIGHT_MIN, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_HEIGHT_MAX, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_SPEED_AVG, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_SPEED_MIN, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].STIRRER_SPEED_MAX, FontSong)); table.addCell(cell);
         
            }

            //把表写入文档
            pdfDocument.Add(table);
 
            //****** 关键事件表 ******// 
            pdfDocument.Add(new Paragraph(" "));//空行
            FontHei.Size = 14;
             para = new iTextSharp.text.Paragraph("脱硫过程关键事件表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 3;
            int[] HeaderWidths = { 15,5,15,5,5,
                                   5,5,5,5,5}; // 百分比
            table.setWidths(HeaderWidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            //这里开始写基本信息
            //**标题行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            cell = new iTextSharp.text.Cell(new Paragraph("时间", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("时长", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("描述", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("温度", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("重量", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[C]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Si]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mn]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[S]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[P]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Cu]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[As]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Sn]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Cu5As8Sn]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Cr]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Ni]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Mo]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Ti]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Nb]", FontSong)); table.addCell(cell);
            //cell = new iTextSharp.text.Cell(new Paragraph("[Pb]", FontSong)); table.addCell(cell);

            // 表格头行结束
            table.endHeaders();


            //数据行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            if (lst.Count > 0)
            {
                //读出数据
                List<KRKeyEvents> LST = GetKRKeyEvents(HeatID);

                for (int I = 0; I < LST.Count; I++)
                {
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].DateAndTime, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Duration.ToString("#0.0"), FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Descripion, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Tempture, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Weight, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_C, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Si, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Mn, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_S, FontSong)); table.addCell(cell);
                    cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_P, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Cu, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_As, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Sn, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Cu5As8Sn, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Cr, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Ni, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Mo, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Ti, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Nb, FontSong)); table.addCell(cell);
                    //cell = new iTextSharp.text.Cell(new Paragraph(LST[I].Ele_Pb, FontSong)); table.addCell(cell);
                }

                //把表写入文档
                pdfDocument.Add(table);


                //***** 历史His数据 *******//              

                //这里绘图，转速和高度
                //定义
                ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
                //使用系统的初始化函数
                clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

                //更改其中某些配置
                stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
                stuDrawPicInfo.IsDrawEveryYAxis = true;
                stuDrawPicInfo.IsDisplayRelativeTime = true;

                stuDrawPicInfo.TagDescription[1] = "高度";
                stuDrawPicInfo.TagUnit[1] = "m";
                stuDrawPicInfo.YAxisIsLeft[1] = true;
                stuDrawPicInfo.AutoSetRang[1] = false;
                stuDrawPicInfo.YSN[1] = 15;
                stuDrawPicInfo.TagMin[1] = 0;
                stuDrawPicInfo.TagMax[1] = 15;

                stuDrawPicInfo.TagDescription[2] = "转速";
                stuDrawPicInfo.TagUnit[2] = "rad/min";
                stuDrawPicInfo.YAxisIsLeft[2] = false;
                stuDrawPicInfo.AutoSetRang[2] = false;
                stuDrawPicInfo.YSN[2] = 15;
                stuDrawPicInfo.TagMin[2] = 0;
                stuDrawPicInfo.TagMax[2] = 150;

                //绘图的数据
                string[] tags = new string[2];
                tags[0] = "LYQ210.KR" + lst[0].DES_STATION_NO + ".REAL_MIX_HEIGHT";
                tags[1] = "LYQ210.KR" + lst[0].DES_STATION_NO + ".REAL_MIX_SPEED";
                stuDrawPicInfo.dt= sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(lst[0].LADLE_ARRIVE), Convert.ToDateTime(lst[0].LADLE_LEAVE));
                //输出图像
                sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次脱硫站搅拌器的高度和转速", stuDrawPicInfo, ref pdfDocument);  
            }
        }
        
        //获取转炉BOF的pdf报告
        public void GetReportPdf_BOF(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

             iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 转炉工序数据追踪", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);


            //基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("炉次基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;


            //获取基本信息
            List<BOFHeatInfo> lst =GetBOFHeatInfo(HeatID);
            if (lst.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(HeatID, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Steel_grade, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("炉座号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Treatpos, FontSong)); table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].shift_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("班组", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].crew_id, FontSong)); table.addCell(cell);
               
                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("操作员", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].operator_c, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("promodecode", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].promodecode, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("炉代", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].bof_campaign, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("炉龄", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].bof_life, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出钢口", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tappinghole, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("campaign", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tap_hole_campaign, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出钢口寿命", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tap_hole_life, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("氧枪ID", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].mainlance_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("氧枪枪龄", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].mainlance_life, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("副枪ID", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].sublance_id, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("副枪枪龄", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].sublance_life, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("熔池深度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].bath_level, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("铁包ID", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ironid, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢包ID", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].steelladleid, FontSong)); table.addCell(cell);
                //cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ladleid, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("渣重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].slag_net_weight, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("出钢重量", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].weight_act, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出钢温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tem_act, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出钢[C]", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].bofc_act, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("出钢氧活度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].o2ppm_act, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //合金加入
                table.DefaultCellBackgroundColor  = Color_DescCell;
                cell = new iTextSharp.text.Cell(new Paragraph("生石灰", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("cadd" , FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("硅铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("铝", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("锰硅", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("锰铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("铌铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("高碳铬铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("低碳铬铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("中碳铬铁", FontKai)); table.addCell(cell);

                table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);  
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].alloycao_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].cadd_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].fesi_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].al_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].mnsi_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].femn_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].fenb_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].hscrw_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].lscrw_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].sscrw_wgt, FontSong)); table.addCell(cell);

                table.DefaultCellBackgroundColor = Color_DescCell;
                cell = new iTextSharp.text.Cell(new Paragraph("中碳锰铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("低碳锰铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("轻烧白云石", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("预熔渣", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("lfslag", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("硅钙钡", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("硅铝铁", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("金属锰", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); table.addCell(cell);

                table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);  
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].mfemn_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].lfemn_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].rdolo_wgt, FontSong)); table.addCell(cell);        
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].burnslag_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].lfslag_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].sicabei_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].sialfe_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].mn_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);                
                
            }

            //把表写入文档
            pdfDocument.Add(table);


            GetReportPdf_BOF_KeyEvents(HeatID,lst[0].Treatpos, ref   pdfDocument);

            GetReportPdf_BOF_HisDB(HeatID, lst[0].Treatpos, Convert.ToDateTime(lst[0].Ready_time ), Convert.ToDateTime(lst[0].tapping_endtime),ref pdfDocument);
        }

        public void GetReportPdf_BOF_KeyEvents(string HeatID,string TreatPos, ref  iTextSharp.text.Document pdfDocument)
        {

            //******  关键事件 ********//
            //新开一个页面
            pdfDocument.newPage();
                
            pdfDocument.Add(new Paragraph(" "));//空行
            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次转炉过程关键事件表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(21);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 3;
            int[] HeaderWidths = { 15,5,5,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5,5
                                  ,5}; // 百分比
            table.setWidths(HeaderWidths);            
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            //这里开始写基本信息
            //**标题行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(new Paragraph("时间", FontSong)); table.addCell(cell);             
            cell = new iTextSharp.text.Cell(new Paragraph("时长,min", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("描述", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("名称", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("重量", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("温度", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("O2ppm", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[C]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Si]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mn]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[S]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[P]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Cu]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[As]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Sn]", FontSong)); table.addCell(cell);        
            cell = new iTextSharp.text.Cell(new Paragraph("[Cr]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Ni]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mo]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Ti]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Nb]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Pb]", FontSong)); table.addCell(cell);
             

            // 表格头行结束
            table.endHeaders();
            
            //数据行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);            
            //读出数据
            List<BOFKeyEvents> lstKeyEvents = GetBOFKeyEvents(HeatID);
            for (int I = 0; I < lstKeyEvents.Count; I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Datetime, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Duration.ToString("#0.0"), FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Decription, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Mat_Name, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Weight, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Temp, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].O2ppm, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_C, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Si, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Mn, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_S, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_P, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Cu, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_As, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Sn, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Cr, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Ni, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Mo, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Ti, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Nb, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lstKeyEvents[I].Ele_Pb, FontSong)); table.addCell(cell);
            }

            //把表写入文档
            pdfDocument.Add(table);

           
        }
        public void GetReportPdf_BOF_HisDB(string HeatID, string TreatPos, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //***** 历史His数据 *******//
            //新页面
            //pdfDocument.newPage();

            //这里绘图，转速和高度
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            stuDrawPicInfo.TagDescription[1] = "CO含量";
            stuDrawPicInfo.TagUnit[1] = "%";
            stuDrawPicInfo.YAxisIsLeft[1] = true;
            stuDrawPicInfo.AutoSetRang[1] = false;
            stuDrawPicInfo.YSN[1] = 10;
            stuDrawPicInfo.TagMin[1] = 0;
            stuDrawPicInfo.TagMax[1] = 100;

            stuDrawPicInfo.TagDescription[2] = "CO2含量";
            stuDrawPicInfo.TagUnit[2] = "%";
            stuDrawPicInfo.YAxisIsLeft[2] = true;
            stuDrawPicInfo.AutoSetRang[2] = false;
            stuDrawPicInfo.YSN[2] = 10;
            stuDrawPicInfo.TagMin[2] = 0;
            stuDrawPicInfo.TagMax[2] =50;

            stuDrawPicInfo.TagDescription[3] = "O2含量";
            stuDrawPicInfo.TagUnit[3] = "%";
            stuDrawPicInfo.YAxisIsLeft[3] = false;
            stuDrawPicInfo.AutoSetRang[3] = false;
            stuDrawPicInfo.YSN[3] = 10;
            stuDrawPicInfo.TagMin[3] = 0;
            stuDrawPicInfo.TagMax[3] =30;

            //绘图的数据
            string[] tags = new string[3];
            tags[0] = "LYQ210.BOF" + TreatPos + ".ContentCO";
            tags[1] = "LYQ210.BOF" + TreatPos + ".ContentCO2";
            tags[2] = "LYQ210.BOF" + TreatPos + ".ContentO2";
            
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);

            //绘图                
            clsDuHisPic.DrawImage(ref stuDrawPicInfo);

            //绘制到pdf上           
            iTextSharp.text.Image Img = iTextSharp.text.Image.getInstance(stuDrawPicInfo.BMP, System.Drawing.Imaging.ImageFormat.Jpeg);

            //设置图片的大小
            Img.Alignment = iTextSharp.text.Image.MIDDLE;
            float ImgWidth = pdfDocument.PageSize.Width - pdfDocument.LeftMargin - pdfDocument.RightMargin;
            Img.scaleAbsolute(ImgWidth, 100);
            pdfDocument.Add(Img);

            //图标题
            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次转炉废气成分", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);


            /// *****  炉体与吹气 ************/////////////////////////////////////////////////////
            //新建一页
            //pdfDocument.newPage();

            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int RowNo = 1;
            stuDrawPicInfo.TagDescription[RowNo] = "氧枪高度";
            stuDrawPicInfo.TagUnit[RowNo] = "cm";
            stuDrawPicInfo.YAxisIsLeft[RowNo] = true;
            stuDrawPicInfo.AutoSetRang[RowNo] = false ;
            stuDrawPicInfo.YSN[RowNo] = 10;
            stuDrawPicInfo.TagMin[RowNo] = 0;
            stuDrawPicInfo.TagMax[RowNo] = 1500;

            RowNo=RowNo+1;
            stuDrawPicInfo.TagDescription[RowNo] = "炉体倾角";
            stuDrawPicInfo.TagUnit[RowNo] = "°";
            stuDrawPicInfo.YAxisIsLeft[RowNo] = true;            
            stuDrawPicInfo.AutoSetRang[RowNo] = false;
            stuDrawPicInfo.YSN[RowNo] = 10;
            stuDrawPicInfo.TagMin[RowNo] =-150;
            stuDrawPicInfo.TagMax[RowNo] = 150;

            RowNo = RowNo + 1;
            stuDrawPicInfo.TagDescription[RowNo] = "O2流量";
            stuDrawPicInfo.TagUnit[RowNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[RowNo] = false;            
            stuDrawPicInfo.AutoSetRang[RowNo] = false;
            stuDrawPicInfo.YSN[RowNo] = 10;
            stuDrawPicInfo.TagMin[RowNo] = 0;
            stuDrawPicInfo.TagMax[RowNo] = 1000;

            RowNo = RowNo + 1;
            stuDrawPicInfo.TagDescription[RowNo] = "N2流量";
            stuDrawPicInfo.TagUnit[RowNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[RowNo] = false;
            stuDrawPicInfo.AutoSetRang[RowNo] = false;
            stuDrawPicInfo.YSN[RowNo] = 10;
            stuDrawPicInfo.TagMin[RowNo] = 0;
            stuDrawPicInfo.TagMax[RowNo] = 500;

            RowNo = RowNo + 1;
            stuDrawPicInfo.TagDescription[RowNo] = "Ar流量";
            stuDrawPicInfo.TagUnit[RowNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[RowNo] = false;
            stuDrawPicInfo.AutoSetRang[RowNo] = false;
            stuDrawPicInfo.YSN[RowNo] = 10;
            stuDrawPicInfo.TagMin[RowNo] = 0;
            stuDrawPicInfo.TagMax[RowNo] = 1000;
           

            //绘图的数据
            tags = new string[5];
            tags[0] = "LYQ210.BOF" + TreatPos + ".ACT_LANCE_HEIGHT";
            tags[1] = "LYQ210.BOF" + TreatPos + ".ACT_INCLINE_ANGLE";
            tags[2] = "LYQ210.BOF" + TreatPos + ".ACT_O2_FLUX";
            tags[3] = "LYQ210.BOF" + TreatPos + ".ACT_N2_FLUX";
            tags[4] = "LYQ210.BOF" + TreatPos + ".ACT_AR_FLUX";            
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次转炉废气成分", stuDrawPicInfo, ref pdfDocument);


        }

        //获取LF的pdf报告
        public void GetReportPdf_LF(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 LF精炼工序数据追踪", FontHei);
            iTextSharp.text.Chapter chapter=new Chapter(para,10);
            pdfDocument.Add(chapter);


            //基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("炉次基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //获取基本信息
            List<LFHeatInfo> lstHeatInfo = GetLFHeatInfo(HeatID);
            if (lstHeatInfo.Count <= 0)
            {
                FontKai.Size = 10.5f;
                para = new iTextSharp.text.Paragraph("没有找到该炉次信息", FontKai);
                para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
                pdfDocument.Add(para);
                return;
            }


            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;
            
            //这里开始写基本信息
            cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].heat_id , FontSong)); table.addCell(cell);                

            cell = new iTextSharp.text.Cell(new Paragraph("炉座号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].treatpos, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("工位", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].station, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("线路", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].route, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

            //新行
              
            cell = new iTextSharp.text.Cell(new Paragraph("计划钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].strtgrade , FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("终点钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].endgrade, FontSong)); table.addCell(cell);                                

            cell = new iTextSharp.text.Cell(new Paragraph("班组", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].shiftteam, FontSong)); table.addCell(cell);                
                
            cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].shiftnr, FontSong)); table.addCell(cell);
                                
            cell = new iTextSharp.text.Cell(new Paragraph("操作员", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].monitor , FontSong)); table.addCell(cell);

            //新行
            cell = new iTextSharp.text.Cell(new Paragraph("钢包号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].ladleno, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("钢包状态", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].ladlestatus, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("钢包重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].ladlewei, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

            //新行
            cell = new iTextSharp.text.Cell(new Paragraph("进站", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].strttime, FontSong)); cell.Colspan = 2; table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("出站", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].endtime, FontSong)); cell.Colspan = 2; table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("在站时长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            TimeSpan ts= Convert.ToDateTime(lstHeatInfo[0].endtime)-  Convert.ToDateTime(lstHeatInfo[0].strttime) ;
            cell = new iTextSharp.text.Cell(new Paragraph(ts.TotalMinutes.ToString ("#0.0")+"分钟", FontSong)); cell.Colspan = 3; table.addCell(cell);

            //新行
            cell = new iTextSharp.text.Cell(new Paragraph("初始温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].strtsteeltemp, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("终点温度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].endsteeltemp, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("初始重量", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].strtsteelwei, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("终点重量", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].endsteelwei, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("终点渣重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].endslagwei, FontSong)); table.addCell(cell);

            //新行
            cell = new iTextSharp.text.Cell(new Paragraph("滑动门寿命", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].slidgatelife, FontSong)); table.addCell(cell);
                
            cell = new iTextSharp.text.Cell(new Paragraph("滑动门名称", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].slidgatebrname, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("poroz寿命", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].porozlife, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("poroz名称", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].porozbrname, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("空载时长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].emptydur, FontSong)); table.addCell(cell);

            //新行
            cell = new iTextSharp.text.Cell(new Paragraph("电极保持", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].eletrdholdtm, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("原边总能耗", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].totprieng, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("次边总能耗", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].totseceng, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("气体类型", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].gastype, FontSong)); table.addCell(cell);

            cell = new iTextSharp.text.Cell(new Paragraph("总气体消耗", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph(lstHeatInfo[0].totgas, FontSong)); table.addCell(cell);
            

            //把表写入文档
            pdfDocument.Add(table);

            //关键事件
            GetReportPdf_LF_KeyEvents(HeatID, ref   pdfDocument);

            string TreatPos=lstHeatInfo[0].treatpos;
            string TrolleyNo=lstHeatInfo[0].station;
            DateTime StatrTime=Convert.ToDateTime (lstHeatInfo[0].strttime );
            DateTime EndTime = Convert.ToDateTime(lstHeatInfo[0].endtime );
            //吹气过程
            GetReportPdf_LF_HisDB_Gas(HeatID, TreatPos, TrolleyNo, StatrTime, EndTime, ref  pdfDocument);
            //电器过程
            this.GetReportPdf_LF_HisDB_Ele_PriSide( HeatID,TreatPos,StatrTime,EndTime, ref pdfDocument);
            this.GetReportPdf_LF_HisDB_Ele_SecSide_Voltage (HeatID, TreatPos, StatrTime, EndTime, ref pdfDocument);
            this.GetReportPdf_LF_HisDB_Ele_SecSide_Current (HeatID, TreatPos, StatrTime, EndTime, ref pdfDocument);
        }


        public void GetReportPdf_LF_KeyEvents(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {

            //******  关键事件 ********//
            //新开一个页面
            pdfDocument.newPage();

            pdfDocument.Add(new Paragraph(" "));//空行
            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 精炼过程关键事件表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(20);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 3;
            int[] HeaderWidths = { 12,5,8,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5,5}; // 百分比
            table.setWidths(HeaderWidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            //这里开始写基本信息
            //**标题行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(new Paragraph("时间", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("时长,min", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("描述", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("种类", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("重量", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("温度", FontSong)); table.addCell(cell);            
            cell = new iTextSharp.text.Cell(new Paragraph("[C]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Si]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mn]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[S]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[P]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Cu]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[As]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Sn]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Cr]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Ni]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mo]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Ti]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Nb]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Pb]", FontSong)); table.addCell(cell);


            // 表格头行结束
            table.endHeaders();

            //数据行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            //读出数据
            List<LFKeyEvents> lst = GetLF_KeyEvents(HeatID);
            for (int I = 0; I < lst.Count; I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].datetime, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Duration.ToString("#0.0"), FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Decription, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MatCode, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Weight, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Temp, FontSong)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_C, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Si, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Mn, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_S, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_P, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Cu, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_As, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Sn, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Cr, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Ni, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Mo, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Ti, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Nb, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Pb, FontSong)); table.addCell(cell);
            }

            //把表写入文档
            pdfDocument.Add(table);


        }

        public void GetReportPdf_LF_HisDB_Gas(string HeatID, string TreatPos,string TrolleyNo, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //***** 历史His数据 ****** 吹气数据 *******//
            //新页面
            pdfDocument.newPage();

            
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "底吹压力";
            stuDrawPicInfo.TagUnit[TagNo] = "MPa";
            stuDrawPicInfo.YSN[TagNo] = 12;
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 2.4f;
            stuDrawPicInfo.TagFormat[TagNo] = "#0.00";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 2;

            //TagNo = TagNo + 1;
            //stuDrawPicInfo.TagDescription[TagNo] = "底吹种类";
            //stuDrawPicInfo.TagDataType[TagNo] = "Discrete";
            //stuDrawPicInfo.YAxisIsLeft[TagNo] = true;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "底吹流量";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1200;

            //TagNo = TagNo + 1;
            //stuDrawPicInfo.TagDescription[TagNo] = "顶吹种类";            
            //stuDrawPicInfo.TagDataType[TagNo] = "Discrete";
            //stuDrawPicInfo.YAxisIsLeft[TagNo] = false;

            //TagNo = TagNo + 1;
            //stuDrawPicInfo.TagDescription[TagNo] = "顶吹压力";
            //stuDrawPicInfo.TagUnit[TagNo] = "MPa";
            //stuDrawPicInfo.YSN[TagNo] = 12;
            //stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            //stuDrawPicInfo.AutoSetRang[TagNo] = false;
            //stuDrawPicInfo.TagMin[TagNo] = 0;
            //stuDrawPicInfo.TagMax[TagNo] = 2.4f;
            //stuDrawPicInfo.TagFormat[TagNo] = "#0.00";
            //stuDrawPicInfo.TagDecimalPlaces[TagNo] = 2;

            //TagNo = TagNo + 1;
            //stuDrawPicInfo.TagDescription[TagNo] = "顶吹流量";
            //stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            //stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            //stuDrawPicInfo.AutoSetRang[TagNo] = false;
            //stuDrawPicInfo.TagMin[TagNo] = 0;
            //stuDrawPicInfo.TagMax[TagNo] = 1200;

            //绘图的数据
            string[] tags = new string[2];
            //tags[0] = "LYQ210.LF" + TreatPos + ".Trolley" + TrolleyNo + "_BotGasType";
            //tags[1] = "LYQ210.LF" + TreatPos + ".Trolley" + TrolleyNo + "_BotGasPrss";
            //tags[2] = "LYQ210.LF" + TreatPos + ".Trolley" + TrolleyNo + "_BotGasFlux";

            //tags[3] = "LYQ210.LF" + TreatPos + ".Trolley" + TrolleyNo + "_TopGasType";
            tags[0] = "LYQ210.LF" + TreatPos + ".Trolley" + TrolleyNo + "_TopGasPrss";
            tags[1] = "LYQ210.LF" + TreatPos + ".Trolley" + TrolleyNo + "_TopGasFlux";

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次 精炼吹气过程", stuDrawPicInfo, ref pdfDocument);
            
        }

        public void GetReportPdf_LF_HisDB_Ele_PriSide(string HeatID, string TreatPos,   DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "设定档位";
            stuDrawPicInfo.TagUnit[TagNo] = "V";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] =20;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "原边A相电压";
            stuDrawPicInfo.TagUnit[TagNo] = "V";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 36000;
            stuDrawPicInfo.TagMax[TagNo] = 37000;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "原边A相电流";
            stuDrawPicInfo.TagUnit[TagNo] = "A";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1000;
              
            //绘图的数据
            string[] tags = new string[3];
            tags[00] = "LYQ210.LF" + TreatPos + ".SetTapNo";

            tags[01] = "LYQ210.LF" + TreatPos + ".EleTran_PriSide_Voltage_A";
            tags[02] = "LYQ210.LF" + TreatPos + ".EleTran_PriSide_Curr_A";             

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次 精炼电气过程--变压器原边状态", stuDrawPicInfo, ref pdfDocument);

        }


        public void GetReportPdf_LF_HisDB_Ele_SecSide_Voltage(string HeatID, string TreatPos, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        { 
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true ;
            stuDrawPicInfo.IsDisplayRelativeTime = true;
                       
            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "副边A相电压";
            stuDrawPicInfo.TagUnit[TagNo] = "V";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 400;           
            

            TagNo ++;
            stuDrawPicInfo.TagDescription[TagNo] = "副边B相电压";
            stuDrawPicInfo.TagUnit[TagNo] = "V";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 00;
            stuDrawPicInfo.TagMax[TagNo] = 600;

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "副边C相电压";
            stuDrawPicInfo.TagUnit[TagNo] = "V";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 000;
            stuDrawPicInfo.TagMax[TagNo] = 800;

              
            //绘图的数据
            string[] tags = new string[3];

            tags[0] = "LYQ210.LF" + TreatPos + ".EleTran_SecSide_Voltage_A";
            tags[1] = "LYQ210.LF" + TreatPos + ".EleTran_SecSide_Voltage_B";
            tags[2] = "LYQ210.LF" + TreatPos + ".EleTran_SecSide_Voltage_C";

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次 精炼电气过程", stuDrawPicInfo, ref pdfDocument);


        }

        public void GetReportPdf_LF_HisDB_Ele_SecSide_Current(string HeatID, string TreatPos, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        { 
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "副边A相电流";
            stuDrawPicInfo.TagUnit[TagNo] = "A";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] =0;
            stuDrawPicInfo.TagMax[TagNo] = 120000;

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "副边B相电流";
            stuDrawPicInfo.TagUnit[TagNo] = "A";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 100000;

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "副边C相电流";
            stuDrawPicInfo.TagUnit[TagNo] = "A";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false ;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0000;
            stuDrawPicInfo.TagMax[TagNo] = 80000;


            //绘图的数据
            string[] tags = new string[3];

            tags[0] = "LYQ210.LF" + TreatPos + ".EleTran_SecSide_Curr_A";
            tags[1] = "LYQ210.LF" + TreatPos + ".EleTran_SecSide_Curr_B";
            tags[2] = "LYQ210.LF" + TreatPos + ".EleTran_SecSide_Curr_C";

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次 精炼电气过程(变压器副边电压与电流)", stuDrawPicInfo, ref pdfDocument);
            
        }

        //获取RH的pdf报告
        public void GetReportPdf_RH(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 真空RH工序数据追踪", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);


            //基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("炉次基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //获取基本信息
            List<RHHeatInfo> lst =GetRHHeatInfo(HeatID);
            if (lst.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].heat_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].steel_grade, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("炉座号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].treatpos, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢包号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ladle_id, FontSong)); table.addCell(cell);


                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(Shift_IDNum2Chn(lst[0].shift_id), FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("班组", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(Crew_IDNum2Chn(lst[0].crew_id), FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("氧枪编号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].lance_no, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("氧枪寿命", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].lance_age, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("进站", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].arrive_mainpos_time, FontSong)); cell.Colspan = 2; table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("离站", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].depart_time, FontSong)); cell.Colspan = 2; table.addCell(cell);
                                
                cell = new iTextSharp.text.Cell(new Paragraph("在站时长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                TimeSpan ts = Convert.ToDateTime(lst[0].depart_time) - Convert.ToDateTime(lst[0].arrive_mainpos_time);
                cell = new iTextSharp.text.Cell(new Paragraph(ts.TotalMinutes.ToString ("#0.0")+"分钟", FontSong)); cell.Colspan = 3; table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("真空时长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].vac_dur, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("处理时长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].treat_time, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("最小真空度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].min_vacuum, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("机械冷却水消耗", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].machine_cooling_water_cons, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("冷凝器水消耗", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].condensor_cooling_water_cons, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("热弯管号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].hot_bend_tube_no, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("热弯管次数", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].hot_bend_tube_num, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("真空槽号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].vacuum_slot_no, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("真空槽使用次数", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].vacuum_slot_num, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("上升下降次数", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].updown_num, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("ladle_tare_wt", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ladle_tare_wt, FontSong)); table.addCell(cell);
                 
            }

            //把表写入文档
            pdfDocument.Add(table);

            ////关键事件
            GetReportPdf_RH_KeyEvents(HeatID, ref pdfDocument);

            if (lst.Count > 0)
            {
                string Station = lst[0].treatpos;
                DateTime StatrTime = Convert.ToDateTime(lst[0].arrive_mainpos_time);
                DateTime EndTime = Convert.ToDateTime(lst[0].depart_time);
                //吹气过程
                GetReportPdf_RH_HisDB_Vacuum_CycGas(HeatID, Station, StatrTime, EndTime, ref pdfDocument);
                //废气过程
                GetReportPdf_RH_HisDB_Flue(HeatID, Station, StatrTime, EndTime, ref   pdfDocument);
            }
        }

        public void GetReportPdf_RH_KeyEvents(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {

            //******  关键事件 ********//
            //新开一个页面
            pdfDocument.newPage();

            pdfDocument.Add(new Paragraph(" "));//空行
            FontHei.Size = 14;
            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 真空RH过程关键事件表", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(21);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 3;
            int[] HeaderWidths = { 12,5,8,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5,5,
                                   5,5,5,5,5,
                                   5}; // 百分比
            table.setWidths(HeaderWidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 255, 255);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(255, 0, 255);

            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;

            //这里开始写基本信息
            //**标题行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            iTextSharp.text.Cell cell = new iTextSharp.text.Cell(new Paragraph("时间", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("时长,min", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("描述", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("种类", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("重量", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("温度", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[O]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[C]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Si]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mn]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[S]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[P]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Cu]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[As]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Sn]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Cr]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Ni]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Mo]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Ti]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Nb]", FontSong)); table.addCell(cell);
            cell = new iTextSharp.text.Cell(new Paragraph("[Pb]", FontSong)); table.addCell(cell);


            // 表格头行结束
            table.endHeaders();

            //数据行
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            //读出数据
            List<RH_KeyEvens> lst =this.GetRHKeyEvents (HeatID);
            for (int I = 0; I < lst.Count; I++)
            {
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].DateAndTime , FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Duration.ToString("#0.0"), FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Decription, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].MatCode, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Weight, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Temp, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].O2ppm, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_C, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Si, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Mn, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_S, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_P, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Cu, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_As, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Sn, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Cr, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Ni, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Mo, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Ti, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Nb, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[I].Ele_Pb, FontSong)); table.addCell(cell);
            }

            //把表写入文档
            pdfDocument.Add(table);


        }

        //获取MI基本信息
        public List<MIHeatInfo> GetMIHeatInfo(string HeatID)
        {   
            List<MIHeatInfo> LST = new List<MIHeatInfo>();
            MIHeatInfo lst = new MIHeatInfo(); MIHeatInfo_Ini(ref lst);
            string str = "";
            
            //在BOFHEAT查找对应的入炉时间和铁水包号
            DateTime HM_ChargingStartTime=DateTime.Now ;
            string IronLadleID ="";
            string strSQL="SELECT t.charging_starttime,t.iron_ladle_id FROM SM_bof_heat t where t.heat_id='"+ HeatID+"'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count >0)
            {
                HM_ChargingStartTime=Convert.ToDateTime(dt.Rows[0]["charging_starttime"]);
                IronLadleID =dt.Rows[0]["iron_ladle_id"].ToString();
            }
            else {return LST;}

            //然后从MI_heat中查找最接近的炉次
            strSQL="SELECT * FROM (SELECT * FROM sm_mi_heat"
                 + " WHERE iron_ladle_id='"+ IronLadleID +"'"
                 + " AND start_time <= to_date('" + HM_ChargingStartTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss')"
                 + " AND in_out='OUT'"                             
                 + " ORDER BY start_time desc) t"
                 + " WHERE RowNUM<=1";
            dt = sqt.ReadDatatable_OraDB(strSQL);
            
            if (dt.Rows.Count > 0)
            {
                int RowIndex = 0;
                lst = new MIHeatInfo(); MIHeatInfo_Ini(ref lst);

                lst.HeatID = HeatID;
                str = dt.Rows[RowIndex]["Iron_ID"].ToString(); if (str.Length > 0) lst.IronID = str;
                str = dt.Rows[RowIndex]["Shift_ID"].ToString(); if (str.Length > 0) lst.ShiftID =Shift_IDNum2Chn (str);                    
                str = dt.Rows[RowIndex]["Crew_ID"].ToString(); if (str.Length > 0)  lst.CrewID =Crew_IDNum2Chn ( str);                 
                str = dt.Rows[RowIndex]["Operator"].ToString(); if (str.Length > 0) lst.Operator = str;
                str = dt.Rows[RowIndex]["Iron_Ladle_ID"].ToString(); if (str.Length > 0) lst.IronLaddleID = str;
                str = dt.Rows[RowIndex]["WEIGHT_TIME"].ToString(); if (str.Length > 0) lst.WeightTime = str;

                str = dt.Rows[RowIndex]["IRON_WEIGHT"].ToString(); if (str.Length > 0) lst.HM_Weight = str;
                str = dt.Rows[RowIndex]["TEMPTURE"].ToString(); if (str.Length > 0) lst.HM_Tempture = str;
                str = dt.Rows[RowIndex]["START_TIME"].ToString(); if (str.Length > 0) lst.ChargeTime = str;
                str = dt.Rows[RowIndex]["STOP_TIME"].ToString(); if (str.Length > 0)  lst.HM_SendPlace =HMSendTo_Num2Chn(str);                   
        
            }
            LST.Add(lst);
            dt.Dispose();

            return LST;
        }
         private void  MIHeatInfo_Ini(ref MIHeatInfo lst) 
        {
            lst.HeatID=" ";        lst.IronID=" ";        lst.ShiftID=" ";        lst.CrewID=" ";
            lst.Operator=" ";        lst.IronLaddleID=" ";        lst.WeightTime=" ";        lst.HM_Weight=" ";
            lst.HM_Tempture=" ";        lst.ChargeTime=" ";

            lst.HM_SendPlace=" ";        lst.HM_C=" ";        lst.HM_Si=" ";        lst.HM_Mn=" ";
            lst.HM_S=" ";        lst.HM_P=" ";        lst.HM_Ti=" ";
        }

        //获取MI的关键事件列表        
        public List<MIKeyEvents> GetMIKeyEvents(string HeatID)
        {
            List<MIKeyEvents> LST = new List<MIKeyEvents>();
            MIKeyEvents lst = new MIKeyEvents();MIKeyEvents_Ini(ref lst);
            string str = "";


            //在BOFHEAT查找对应的入炉时间和铁水包号
            DateTime HM_ChargingStartTime = DateTime.Now;
            string IronLadleID = "";
            string strSQL = "SELECT t.charging_starttime,t.iron_ladle_id FROM SM_bof_heat t where t.heat_id='" + HeatID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count > 0)
            {
                HM_ChargingStartTime = Convert.ToDateTime(dt.Rows[0]["charging_starttime"]);
                IronLadleID = dt.Rows[0]["iron_ladle_id"].ToString();
            }
            else { return LST; }

            //然后从sm_MI_heat中查找最接近的铁次时间
            strSQL = "SELECT * FROM (SELECT * FROM sm_mi_heat"
                 + " WHERE iron_ladle_id='" + IronLadleID + "'"
                 + " AND start_time <= to_date('" + HM_ChargingStartTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss')"
                 + " AND in_out='OUT'"
                 + " ORDER BY start_time desc) t"
                 + " WHERE RowNUM<=1";
            dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count > 0)
            {
                HM_ChargingStartTime = Convert.ToDateTime(dt.Rows[0]["start_time"]);                
            }
            else { return LST; }

            //查询出铁表，求其信息；追溯时间范围为过去12小时
            double SearchRangeHours = -12.0;
            DateTime dtDateTimeStart = Convert.ToDateTime(HM_ChargingStartTime);
            DateTime dtDateTimeEnd = dtDateTimeStart.AddHours(SearchRangeHours);
            strSQL = "SELECT * FROM sm_mi_heat"
                            + " WHERE start_time <= to_date('" + dtDateTimeStart.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')"
                            + " AND start_time >= to_date('" + dtDateTimeEnd.ToString("yyyy-MM-dd HH:mm:ss") + "', 'yyyy-mm-dd hh24:mi:ss')";
            dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new MIKeyEvents();
                MIKeyEvents_Ini(ref lst);

                str = dt.Rows[I]["start_time"].ToString(); if (str.Length > 0) lst.DateAndTime = str;
                str = dt.Rows[I]["stop_time"].ToString(); if (str.Length > 0) lst.STOP_TIME = str;

                str = dt.Rows[I]["IN_OUT"].ToString(); if (str.Length > 0) { if ("IN" == str) lst.IN_OUT = "入铁"; else lst.IN_OUT = "出铁"; }
                str = dt.Rows[I]["HEAT_ID"].ToString(); if (str.Length > 0) lst.HEAT_ID = str;
                str = dt.Rows[I]["IRON_ID"].ToString(); if (str.Length > 0) lst.IRON_ID = str;
                str = dt.Rows[I]["IRON_LADLE_ID"].ToString(); if (str.Length > 0) { lst.IRON_LADLE_ID = str;}

                str = dt.Rows[I]["BF_ID"].ToString(); if (str.Length > 0) lst.BF_ID = str;
                str = dt.Rows[I]["BF_TAP_ID"].ToString(); if (str.Length > 0) lst.BF_TAP_ID = str;
                str = dt.Rows[I]["IRON_WEIGHT"].ToString(); if (str.Length > 0) { lst.IRON_WEIGHT = str; if (lst.IN_OUT == "出铁") lst.IRON_WEIGHT ="-"+ lst.IRON_WEIGHT; }
                str = dt.Rows[I]["MIXER_WEIGHT"].ToString(); if (str.Length > 0) lst.MIXER_WEIGHT = str;
                str = dt.Rows[I]["BOF_ID"].ToString(); if (str.Length > 0) lst.BOF_ID = str;
                str = dt.Rows[I]["SEND_PLACE"].ToString(); if (str.Length > 0) lst.SEND_PLACE =HMSendTo_Num2Chn( str);
                str = dt.Rows[I]["TEMPTURE"].ToString(); if (str.Length > 0) lst.TEMPTURE = str;
                str = dt.Rows[I]["Shift_ID"].ToString(); if (str.Length > 0) lst.Shift_ID =Shift_IDNum2Chn(str);
                str = dt.Rows[I]["Crew_ID"].ToString(); if (str.Length > 0) lst.Crew_ID =Crew_IDNum2Chn(str);
                str = dt.Rows[I]["Operator"].ToString(); if (str.Length > 0) lst.Operator = str;   

                LST.Add(lst);
            }
            dt.Dispose();

            DateTime cDateTime = new DateTime();
            TimeSpan ts = new TimeSpan();
            for (int I = 0; I < LST.Count; I++)
            {
                cDateTime = Convert.ToDateTime(LST[I].DateAndTime);
                ts = dtDateTimeStart - cDateTime;
                LST[I].Duration =Convert.ToSingle(ts.TotalMinutes.ToString("#0.0"));
            }

            //按照时长排序
            LST.Sort(delegate(MIKeyEvents a, MIKeyEvents b) { return a.Duration.CompareTo(b.Duration); });

            return LST;
        }

        public void MIKeyEvents_Ini(ref MIKeyEvents lst)
        {
            lst.DateAndTime=" ";            lst.STOP_TIME=" ";            lst.Duration=0;            lst. IN_OUT=" ";
            lst. HEAT_ID=" ";            lst.IRON_ID=" ";            lst.IRON_LADLE_ID=" ";            lst.BF_ID=" ";
            lst.BF_TAP_ID=" ";            lst.IRON_WEIGHT=" ";            lst.MIXER_WEIGHT=" ";            lst.BOF_ID=" ";
            lst.SEND_PLACE=" ";            lst.TEMPTURE=" ";            lst.Shift_ID=" ";            lst.Crew_ID=" ";
            lst.Operator=" ";   
        }

        //把由1、2、3编码的班次改为 白、中、晚
        public string Shift_IDNum2Chn(string Crew_ID )
        { 
            string str=Crew_ID;
            if ("1" == str) return "早";
            if ("2" == str) return "中";
            if ("3" == str) return "晚";
           
            return str; 
        }

        //把由1、2、3、4编码的班组改为 甲、乙、丙、丁
        public string Crew_IDNum2Chn(string Crew_ID )
        { 
            string str=Crew_ID;
            if ("1" == str) return "甲";
            if ("2" == str) return "乙";
            if ("3" == str) return "丙";
            if ("4" == str) return "丁";
            return str; 
        }

        //把由10、31、40....编码的铁水发往位置信息改为 1#转炉、3#脱硫站等
        public string HMSendTo_Num2Chn(string PosiNum)
        {
           switch (PosiNum)
           {
               case "10": return "混铁炉";

               case "11":    return "折罐位1号";
               case "12":    return "折罐位2号";
               case "13":    return "折罐位3号";

               case "21":    return "脱硫 1号";
               case "22":    return "脱硫 2号";
               case "23":    return "脱硫 3号";

               case "31":    return "高炉";
               case "40":    return "翻铁";
               default: return PosiNum;
           }           
        }

        public List<KRHeatInfo> GetKRHeatInfo(string HeatID)
        {
            List<KRHeatInfo> LST = new List<KRHeatInfo>();
            KRHeatInfo lst = new KRHeatInfo(); KRHeatInfo_Ini(ref lst);
            object obj = new object();
 
            /////// 各种事件/////////////////
            string strSQL = "SELECT * FROM sm_KR_Heat WHERE Heat_ID= '" + HeatID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count > 0)
            {
                int RowIndex = 0;
                lst = new KRHeatInfo(); KRHeatInfo_Ini(ref lst);

                obj = dt.Rows[RowIndex]["Heat_ID"]; if (obj.ToString().Length > 0) lst.HEAT_ID = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["IRON_ID"]; if (obj.ToString().Length > 0) lst.IRON_ID  = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["DES_STATION_NO"]; if (obj.ToString().Length > 0) lst.DES_STATION_NO = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["PLAN_NO"]; if (obj.ToString().Length > 0) lst.PLAN_NO = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["PONO"]; if (obj.ToString().Length > 0) lst.PONO = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["STEEL_GRADE"]; if (obj.ToString().Length > 0) lst.STEEL_GRADE = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["AIM_S"]; if (obj.ToString().Length > 0) lst.AIM_S = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["DES_STEP_NUM"]; if (obj.ToString().Length > 0) lst.DES_STEP_NUM = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["IRON_LADLE_ID"]; if (obj.ToString().Length > 0) lst.IRON_LADLE_ID = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["INI_TEMP"]; if (obj.ToString().Length > 0) lst.INI_TEMP = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["INI_WGT"]; if (obj.ToString().Length > 0) lst.INI_WGT = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["INI_C"]; if (obj.ToString().Length > 0) lst.INI_C = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["INI_SI"]; if (obj.ToString().Length > 0) lst.INI_SI = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["INI_MN"]; if (obj.ToString().Length > 0) lst.INI_MN = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["INI_P"]; if (obj.ToString().Length > 0) lst.INI_P = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["INI_S"]; if (obj.ToString().Length > 0) lst.INI_S = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["INI_TI"]; if (obj.ToString().Length > 0) lst.INI_TI = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["FIN_TEMP"]; if (obj.ToString().Length > 0) lst.FIN_TEMP = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["FIN_WGT"]; if (obj.ToString().Length > 0) lst.FIN_WGT = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["MATERIALID_ACT"]; if (obj.ToString().Length > 0) lst.MATERIALID_ACT = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["ADDWGT_ACT"]; if (obj.ToString().Length > 0) lst.ADDWGT_ACT = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_DURATION"]; if (obj.ToString().Length > 0) lst.STIRRER_DURATION = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_SPEED_MAX"]; if (obj.ToString().Length > 0) lst.STIRRER_SPEED_MAX = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_SPEED_MIN"]; if (obj.ToString().Length > 0) lst.STIRRER_SPEED_MIN = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_SPEED_AVG"]; if (obj.ToString().Length > 0) lst.STIRRER_SPEED_AVG = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["STIRRER_HEIGHT_MAX"]; if (obj.ToString().Length > 0) lst.STIRRER_HEIGHT_MAX = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_HEIGHT_MIN"]; if (obj.ToString().Length > 0) lst.STIRRER_HEIGHT_MIN = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_HEIGHT_AVG"]; if (obj.ToString().Length > 0) lst.STIRRER_HEIGHT_AVG = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_ID"]; if (obj.ToString().Length > 0) lst.STIRRER_ID = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["STIRRER_TIMES"]; if (obj.ToString().Length > 0) lst.STIRRER_TIMES = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["LADLE_ARRIVE"]; if (obj.ToString().Length > 0) lst.LADLE_ARRIVE = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["LADLE_LEAVE"]; if (obj.ToString().Length > 0) lst.LADLE_LEAVE = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["DES_START"]; if (obj.ToString().Length > 0) lst.DES_START = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["DES_END"]; if (obj.ToString().Length > 0) lst.DES_END = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["RESIDUE_FIRST_S"]; if (obj.ToString().Length > 0) lst.RESIDUE_FIRST_S = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["RESIDUE_FIRST_E"]; if (obj.ToString().Length > 0) lst.RESIDUE_FIRST_E = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["RESIDUE_FIRST_DURATION"]; if (obj.ToString().Length > 0) lst.RESIDUE_FIRST_DURATION = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["RESIDUE_FIRST_SLAG_WGT"]; if (obj.ToString().Length > 0) lst.RESIDUE_FIRST_SLAG_WGT = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["RESIDUE_LAST_S"]; if (obj.ToString().Length > 0) lst.RESIDUE_LAST_S = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["RESIDUE_LAST_E"]; if (obj.ToString().Length > 0) lst.RESIDUE_LAST_E = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["RESIDUE_LAST_DURATION"]; if (obj.ToString().Length > 0) lst.RESIDUE_LAST_DURATION = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["RESIDUE_LAST_SLAG_WGT"]; if (obj.ToString().Length > 0) lst.RESIDUE_LAST_SLAG_WGT = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["CALEFACIENT_USED"]; if (obj.ToString().Length > 0) lst.CALEFACIENT_USED = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["DES_DURATION"]; if (obj.ToString().Length > 0) lst.DES_DURATION = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["PRODUCE_DATE"]; if (obj.ToString().Length > 0) lst.PRODUCE_DATE = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["CREW_ID"]; if (obj.ToString().Length > 0) lst.CREW_ID = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["SHIFT_ID"]; if (obj.ToString().Length > 0) lst.SHIFT_ID = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["VALID_FLAG"]; if (obj.ToString().Length > 0) lst.VALID_FLAG = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["PERIOD_ID"]; if (obj.ToString().Length > 0) lst.PERIOD_ID = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["LADLE_WEIGHT"]; if (obj.ToString().Length > 0) lst.LADLE_WEIGHT = Convert.ToString(obj);

                obj = dt.Rows[RowIndex]["TEMP_TIME_F"]; if (obj.ToString().Length > 0) lst.TEMP_TIME_F = Convert.ToString(obj);
                obj = dt.Rows[RowIndex]["TEMP_TIME_E"]; if (obj.ToString().Length > 0) lst.TEMP_TIME_E = Convert.ToString(obj);

                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }
        public void KRHeatInfo_Ini(ref KRHeatInfo lst)
        {
            lst.HEAT_ID  = " ";            lst.IRON_ID = " ";            lst.DES_STATION_NO = " ";            lst.PLAN_NO = " ";
            lst.PONO = " ";            lst.STEEL_GRADE = " ";            lst.AIM_S = " ";            lst.DES_STEP_NUM = " ";
            lst.IRON_LADLE_ID = " ";            lst.INI_TEMP = " ";            lst.INI_WGT = " ";            lst.INI_C = " ";
            lst.INI_SI = " ";            lst.INI_MN = " ";            lst.INI_P = " ";            lst.INI_S = " ";
            lst.INI_TI = " ";            lst.FIN_TEMP = " ";            lst.FIN_WGT = " ";            lst.MATERIALID_ACT = " ";
            lst.ADDWGT_ACT = " ";            lst.STIRRER_DURATION = " ";            lst.STIRRER_SPEED_MAX = " ";            lst.STIRRER_SPEED_MIN = " ";
            lst.STIRRER_SPEED_AVG = " ";            lst.STIRRER_HEIGHT_MAX = " ";            lst.STIRRER_HEIGHT_MIN = " ";
            lst.STIRRER_HEIGHT_AVG = " ";            lst.STIRRER_ID = " ";            lst.STIRRER_TIMES = " ";            lst.LADLE_ARRIVE = " ";
            lst.LADLE_LEAVE = " ";            lst.DES_START = " ";            lst.DES_END = " ";            lst.RESIDUE_FIRST_S = " ";
            lst.RESIDUE_FIRST_E = " ";            lst.RESIDUE_FIRST_DURATION = " ";            lst.RESIDUE_FIRST_SLAG_WGT = " ";
            lst.RESIDUE_LAST_S = " ";            lst.RESIDUE_LAST_E = " ";            lst.RESIDUE_LAST_DURATION = " ";
            lst.RESIDUE_LAST_SLAG_WGT = " ";            lst.CALEFACIENT_USED = " ";            lst.DES_DURATION = " ";
            lst.PRODUCE_DATE = " ";            lst.CREW_ID = " ";            lst.SHIFT_ID = " ";
            lst.VALID_FLAG = " ";            lst.PERIOD_ID = " ";            lst.LADLE_WEIGHT = " ";            lst.TEMP_TIME_F = " ";
            lst.TEMP_TIME_E = " ";
        }

        //获取脱硫站KR 化学分析、测温、加料等关键事件
        public List<KRKeyEvents> GetKRKeyEvents(string HeatID)
        {
            List<KRKeyEvents> LST = new List<KRKeyEvents>();
            KRKeyEvents lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
            string str = "";

            //为了计算Duration而设置
            DateTime StartDateTime = new DateTime();

            //获取化验成分事件
            string strSQL = "select * FROM sm_elem_ana where Heat_ID='" + HeatID + "' and DEVICE_NO like 'LY210_KR%'";                       
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            
            if(dt.Rows.Count>0)
            {
                int RowIndex = 0;

                lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                lst.Descripion = "化验值" + RowIndex.ToString();

                //str = dt.Rows[RowIndex][""].ToString();if (str.Length>0) lst=str;
                str = dt.Rows[RowIndex]["sampletime"].ToString(); if (str.Length > 0) lst.DateAndTime = str;
                str = dt.Rows[RowIndex]["Ele_C"].ToString(); if (str.Length > 0) lst.Ele_C = str;
                str = dt.Rows[RowIndex]["Ele_Si"].ToString(); if (str.Length > 0) lst.Ele_Si = str;
                str = dt.Rows[RowIndex]["Ele_Mn"].ToString(); if (str.Length > 0) lst.Ele_Mn = str;

                str = dt.Rows[RowIndex]["Ele_S"].ToString(); if (str.Length > 0) lst.Ele_S = str;
                str = dt.Rows[RowIndex]["Ele_P"].ToString(); if (str.Length > 0) lst.Ele_P = str;
                str = dt.Rows[RowIndex]["Ele_Cu"].ToString(); if (str.Length > 0) lst.Ele_Cu = str;
                str = dt.Rows[RowIndex]["Ele_As"].ToString(); if (str.Length > 0) lst.Ele_As = str;
                str = dt.Rows[RowIndex]["Ele_Sn"].ToString(); if (str.Length > 0) lst.Ele_Sn = str;

                lst.Ele_Cu5As8Sn = (Convert.ToSingle(lst.Ele_Cu) + 5 * Convert.ToSingle(lst.Ele_As) + 8 * Convert.ToSingle(lst.Ele_Sn)).ToString();
                str = dt.Rows[RowIndex]["Ele_Cr"].ToString(); if (str.Length > 0) lst.Ele_Cr = str;
                str = dt.Rows[RowIndex]["Ele_Ni"].ToString(); if (str.Length > 0) lst.Ele_Ni = str;
                str = dt.Rows[RowIndex]["Ele_Mo"].ToString(); if (str.Length > 0) lst.Ele_Mo = str;

                str = dt.Rows[RowIndex]["Ele_Ti"].ToString(); if (str.Length > 0) lst.Ele_Ti = str;
                str = dt.Rows[RowIndex]["Ele_Nb"].ToString(); if (str.Length > 0) lst.Ele_Nb = str;
                str = dt.Rows[RowIndex]["Ele_Pb"].ToString(); if (str.Length > 0) lst.Ele_Pb = str;

                LST.Add(lst);
            }
            dt.Dispose();

            //物料添加表---主要是石灰
             strSQL = "select * FROM sm_addition where Heat_ID='" + HeatID + "' and DEVICE_NO like 'LY210_KR'";
             dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                lst.Descripion = "脱硫剂" + RowIndex.ToString();
                str = dt.Rows[RowIndex]["add_time"].ToString(); if (str.Length > 0) lst.DateAndTime = str;
                str = dt.Rows[RowIndex]["weight"].ToString(); if (str.Length > 0) lst.Weight = str;

                LST.Add(lst);
            }
            dt.Dispose();

            /////// 各种事件/////////////////
            strSQL = "SELECT * FROM sm_KR_HEAT WHERE heat_ID= '" + HeatID + "'";
            dt = sqt.ReadDatatable_OraDB(strSQL);
            //获取化验成分表，是一个常规纵表
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);

                lst.Descripion = "初始铁水";
                str = dt.Rows[RowIndex]["LADLE_ARRIVE"].ToString(); if (str.Length > 0) { lst.DateAndTime = str; StartDateTime = Convert.ToDateTime(str); }

                str = dt.Rows[RowIndex]["INI_TEMP"].ToString(); if (str.Length > 0) lst.Tempture = str;
                str = dt.Rows[RowIndex]["INI_WGT"].ToString(); if (str.Length > 0) lst.Weight = str;
                str = dt.Rows[RowIndex]["INI_C"].ToString(); if (str.Length > 0) lst.Ele_C = str;
                str = dt.Rows[RowIndex]["INI_SI"].ToString(); if (str.Length > 0) lst.Ele_Si = str;
                str = dt.Rows[RowIndex]["INI_MN"].ToString(); if (str.Length > 0) lst.Ele_Mn = str;
                str = dt.Rows[RowIndex]["INI_P"].ToString(); if (str.Length > 0) lst.Ele_P = str;
                str = dt.Rows[RowIndex]["INI_S"].ToString(); if (str.Length > 0) lst.Ele_S = str;
                str = dt.Rows[RowIndex]["INI_TI"].ToString(); if (str.Length > 0) lst.Ele_Ti = str;

                LST.Add(lst);

                lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                lst.Descripion = "终点铁水";
                str = dt.Rows[RowIndex]["LADLE_LEAVE"].ToString(); if (str.Length > 0) lst.DateAndTime = str;
                str = dt.Rows[RowIndex]["FIN_TEMP"].ToString(); if (str.Length > 0) lst.Tempture = str;
                str = dt.Rows[RowIndex]["FIN_WGT"].ToString(); if (str.Length > 0) lst.Weight = str;
                LST.Add(lst);

                lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                lst.Descripion = "脱硫开始";
                str = dt.Rows[RowIndex]["DES_START"].ToString();
                if (str.Length > 0)
                {
                    lst.DateAndTime = str;
                }
                LST.Add(lst);

                lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                lst.Descripion = "脱硫结束";
                str = dt.Rows[RowIndex]["DES_END"].ToString();
                if (str.Length > 0)
                {
                    lst.DateAndTime = str;
                }
                LST.Add(lst);

                str = dt.Rows[RowIndex]["RESIDUE_FIRST_S"].ToString();
                if (str.Length > 0)
                {
                    lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                    lst.Descripion = "去前渣开始";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }

                str = dt.Rows[RowIndex]["RESIDUE_FIRST_E"].ToString();
                if (str.Length > 0)
                {
                    lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                    lst.Descripion = "去前渣结束";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }

                str = dt.Rows[RowIndex]["RESIDUE_LAST_S"].ToString();
                if (str.Length > 0)
                {
                    lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                    lst.Descripion = "去后渣开始";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }
                str = dt.Rows[RowIndex]["RESIDUE_LAST_E"].ToString();
                if (str.Length > 0)
                {
                    lst = new KRKeyEvents(); KRKeyEvents_Ini(ref lst);
                    lst.Descripion = "去后渣结束";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }
            }
            
            dt.Dispose();

            DateTime cDateTime = new DateTime();
            TimeSpan ts = new TimeSpan();
            for (int I = 0; I < LST.Count; I++)
            {                
                if (DateTime.TryParse(LST[I].DateAndTime, out cDateTime))
                {
                    ts = cDateTime - StartDateTime;
                    LST[I].Duration = float.Parse((ts.TotalSeconds / 60.0).ToString("#0.00"));
                }                
            }

            //按照时长排序
            LST.Sort(delegate(KRKeyEvents a, KRKeyEvents b) { return a.Duration.CompareTo(b.Duration); });

            return LST;
        }
        public void KRKeyEvents_Ini(ref KRKeyEvents lst)
        {
            lst.DateAndTime = " ";  lst.Duration = 0;       lst.Descripion = " ";   lst.Tempture = " ";
            lst.Weight = " ";       lst.Ele_C = " ";        lst.Ele_Si = " ";       lst.Ele_Mn = " ";
            lst.Ele_S = " ";        lst.Ele_P = " ";        lst.Ele_Cu = " ";       lst.Ele_As = " ";
            lst.Ele_Sn = " ";       lst.Ele_Cu5As8Sn = " "; lst.Ele_Cr = " ";       lst.Ele_Ni = " ";
            lst.Ele_Mo = " ";       lst.Ele_Ti = " ";       lst.Ele_Nb = " ";       lst.Ele_Pb = " ";
        }

        public List<KRMixerHeightSpeed> GetKRMixerHeightSpeed(string des_station_no, string StartDateTime, string EndDateTime)
        {
            List<KRMixerHeightSpeed> LST = new List<KRMixerHeightSpeed>();

            string[] tags = new string[2];
            tags[0] = "LYQ210.KR" + des_station_no + ".REAL_MIX_HEIGHT";
            tags[1] = "LYQ210.KR" + des_station_no + ".REAL_MIX_SPEED";
            System.Data.DataTable dt =sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));

            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                KRMixerHeightSpeed MHS = new KRMixerHeightSpeed();

                MHS.DateAndTime = Convert.ToDateTime(dt.Rows[I][0]);
                TimeSpan ts = MHS.DateAndTime - TimeStart;
                MHS.sngDuration = (float)ts.TotalMilliseconds / 60000;
                MHS.DateTimeDuration = MHS.DateAndTime.Year + "/" + MHS.DateAndTime.Month + "/" + MHS.DateAndTime.Day
                                       + " " + MHS.DateAndTime.Hour + ":" + MHS.DateAndTime.Minute + ":" + MHS.DateAndTime.Second
                                           + "(" + MHS.sngDuration + ")";
                MHS.Height = Convert.ToSingle(dt.Rows[I][1]);
                MHS.Speed = Convert.ToSingle(dt.Rows[I][2]);

                LST.Add(MHS);
            }
            return LST;
        }

        public List<BOFHeatInfo> GetBOFHeatInfo(string HeatID)
        {
            List<BOFHeatInfo> LST = new List<BOFHeatInfo>();
            BOFHeatInfo lst = new BOFHeatInfo(); BOFHeatInfo_Ini(ref lst);

            object obj=new object();
            string str = "";

            string strSQL = "SELECT * FROM SM_bof_heat WHERE heat_id='" + HeatID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL); 
            if (dt.Rows.Count >0)            
            {
                int RowIndex=0;
                lst = new BOFHeatInfo(); BOFHeatInfo_Ini(ref lst);

                obj=dt.Rows[RowIndex]["heat_id"];if (obj.ToString().Length>0) lst.Heat_id=obj.ToString();                
     			obj=dt.Rows[RowIndex]["station"];if (obj.ToString().Length>0) lst.Treatpos=obj.ToString();
     			obj=dt.Rows[RowIndex]["plan_no"];if (obj.ToString().Length>0) lst.plan_no=obj.ToString();
     			obj=dt.Rows[RowIndex]["pono"];if (obj.ToString().Length>0) lst.pono=obj.ToString();
     			obj=dt.Rows[RowIndex]["steel_grade"];if (obj.ToString().Length>0) lst.Steel_grade=obj.ToString();

     			obj=dt.Rows[RowIndex]["promodecode"];if (obj.ToString().Length>0) lst.promodecode=obj.ToString();
     			obj=dt.Rows[RowIndex]["bof_campaign"];if (obj.ToString().Length>0) lst.bof_campaign=obj.ToString();
     			obj=dt.Rows[RowIndex]["bof_life"];if (obj.ToString().Length>0) lst.bof_life=obj.ToString();
     			obj=dt.Rows[RowIndex]["tappinghole"];if (obj.ToString().Length>0) lst.tappinghole=obj.ToString();
     			obj=dt.Rows[RowIndex]["tap_hole_campaign"];if (obj.ToString().Length>0) lst.tap_hole_campaign=obj.ToString();

     			obj=dt.Rows[RowIndex]["tap_hole_life"];if (obj.ToString().Length>0) lst.tap_hole_life=obj.ToString();
     			obj=dt.Rows[RowIndex]["mainlance_id"];if (obj.ToString().Length>0) lst.mainlance_id=obj.ToString();
     			obj=dt.Rows[RowIndex]["mainlance_life"];if (obj.ToString().Length>0) lst.mainlance_life=obj.ToString();
     			obj=dt.Rows[RowIndex]["sublance_id"];if (obj.ToString().Length>0) lst.sublance_id=obj.ToString();
     			obj=dt.Rows[RowIndex]["sublance_life"];if (obj.ToString().Length>0) lst.sublance_life=obj.ToString();

     			obj=dt.Rows[RowIndex]["bath_level"];if (obj.ToString().Length>0) lst.bath_level=obj.ToString();
                obj = dt.Rows[RowIndex]["STEEL_LADLE_ID"]; if (obj.ToString().Length > 0) lst.steelladleid = obj.ToString();
     		
     			obj=dt.Rows[RowIndex]["slag_net_weight"];if (obj.ToString().Length>0) lst.slag_net_weight=obj.ToString();
     		
     			obj=dt.Rows[RowIndex]["weight_act"];if (obj.ToString().Length>0) lst.weight_act=obj.ToString();
     			obj=dt.Rows[RowIndex]["weighting_time"];if (obj.ToString().Length>0) lst.weighting_time=obj.ToString();
                 
     			//obj=dt.Rows[RowIndex]["metal_act"];if (obj.ToString().Length>0) lst.metal_act=obj.ToString();
     			obj=dt.Rows[RowIndex]["cao_weight"];if (obj.ToString().Length>0) lst.cao_weight=obj.ToString();

     			obj=dt.Rows[RowIndex]["dolo_weight"];if (obj.ToString().Length>0) lst.dolo_weight=obj.ToString();
     			obj=dt.Rows[RowIndex]["rdolo_weight"];if (obj.ToString().Length>0) lst.o2_act=obj.ToString();
     			obj=dt.Rows[RowIndex]["mgo_weight"];if (obj.ToString().Length>0) lst.mgo_weight=obj.ToString();
     			obj=dt.Rows[RowIndex]["caf2_weight"];if (obj.ToString().Length>0) lst.caf2_weight=obj.ToString();
     			obj=dt.Rows[RowIndex]["iron_weight"];if (obj.ToString().Length>0) lst.iron_weight=obj.ToString();

                obj = dt.Rows[RowIndex]["O2_ConSUM_VOlume"]; if (obj.ToString().Length > 0) lst.o2_act = obj.ToString();
     			obj=dt.Rows[RowIndex]["ar_act"];if (obj.ToString().Length>0) lst.ar_act=obj.ToString();
     			obj=dt.Rows[RowIndex]["n2_act"];if (obj.ToString().Length>0) lst.n2_act=obj.ToString();

     			obj=dt.Rows[RowIndex]["tsc_tem"];if (obj.ToString().Length>0) lst.tsc_tem=obj.ToString();
     			obj=dt.Rows[RowIndex]["tsc_c"];if (obj.ToString().Length>0) lst.tsc_c=obj.ToString(); 
               
     			obj=dt.Rows[RowIndex]["tso_tem"];if (obj.ToString().Length>0) lst.tso_tem=obj.ToString();
     			obj=dt.Rows[RowIndex]["tso_c"];if (obj.ToString().Length>0) lst.tso_c=obj.ToString();
     			obj=dt.Rows[RowIndex]["tso_o2ppm"];if (obj.ToString().Length>0) lst.tso_o2ppm=obj.ToString();
     			
     			obj=dt.Rows[RowIndex]["o2_duration"];if (obj.ToString().Length>0) lst.o2_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["ar_duration"];if (obj.ToString().Length>0) lst.ar_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["n2_duration"];if (obj.ToString().Length>0) lst.n2_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["after_stiring_duration"];if (obj.ToString().Length>0) lst.after_stiring_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow_num"];if (obj.ToString().Length>0) lst.reblow_num=obj.ToString();

     			obj=dt.Rows[RowIndex]["reblow1_tem"];if (obj.ToString().Length>0) lst.reblow1_tem=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow2_tem"];if (obj.ToString().Length>0) lst.reblow2_tem=obj.ToString();
     			obj=dt.Rows[RowIndex]["deslag_num"];if (obj.ToString().Length>0) lst.deslag_num=obj.ToString();
     			obj=dt.Rows[RowIndex]["slag_splash_n2"];if (obj.ToString().Length>0) lst.slag_splash_n2=obj.ToString();
     			obj=dt.Rows[RowIndex]["ready_time"];if (obj.ToString().Length>0) lst.Ready_time=obj.ToString();

     			obj=dt.Rows[RowIndex]["charging_starttime"];if (obj.ToString().Length>0) lst.charging_starttime=obj.ToString();     			   			 
     			obj=dt.Rows[RowIndex]["charging_endtime"];if (obj.ToString().Length>0) lst.charging_endtime=obj.ToString();
                obj = dt.Rows[RowIndex]["blow_starttime"]; if (obj.ToString().Length > 0) lst.blow_starttime = obj.ToString();

     			obj=dt.Rows[RowIndex]["blow_endtime"];if (obj.ToString().Length>0) lst.blow_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow1_starttime"];if (obj.ToString().Length>0) lst.reblow1_starttime=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow1_endtime"];if (obj.ToString().Length>0) lst.reblow1_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow1_duration"];if (obj.ToString().Length>0) lst.reblow1_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow2_starttime"];if (obj.ToString().Length>0) lst.reblow2_starttime=obj.ToString();

     			obj=dt.Rows[RowIndex]["reblow2_endtime"];if (obj.ToString().Length>0) lst.reblow2_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["reblow2_duration"];if (obj.ToString().Length>0) lst.reblow2_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["slag_nr"];if (obj.ToString().Length>0) lst.slag_nr=obj.ToString();
     			obj=dt.Rows[RowIndex]["tapping_starttime"];if (obj.ToString().Length>0) lst.tapping_starttime=obj.ToString();
     			obj=dt.Rows[RowIndex]["tapping_endtime"];if (obj.ToString().Length>0) lst.tapping_endtime=obj.ToString();

     			obj=dt.Rows[RowIndex]["tapping_duration"];if (obj.ToString().Length>0) lst.tapping_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["slag_starttime"];if (obj.ToString().Length>0) lst.slag_starttime=obj.ToString();
     			obj=dt.Rows[RowIndex]["slag_endtime"];if (obj.ToString().Length>0) lst.slag_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["slag_duration"];if (obj.ToString().Length>0) lst.slag_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["product_day"];if (obj.ToString().Length>0) lst.Product_day=obj.ToString();

     			obj=dt.Rows[RowIndex]["shift_id"];if (obj.ToString().Length>0) lst.shift_id=obj.ToString();
     			obj=dt.Rows[RowIndex]["crew_id"];if (obj.ToString().Length>0) lst.crew_id=obj.ToString();
     			    			

     			obj=dt.Rows[RowIndex]["ge_no"];if (obj.ToString().Length>0) lst.ge_no=obj.ToString();
     			obj=dt.Rows[RowIndex]["tsc_starttime"];if (obj.ToString().Length>0) lst.tsc_starttime=obj.ToString();
     			obj=dt.Rows[RowIndex]["tsc_endtime"];if (obj.ToString().Length>0) lst.tsc_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["tso_starttime"];if (obj.ToString().Length>0) lst.tso_starttime=obj.ToString();
     			obj=dt.Rows[RowIndex]["tso_endtime"];if (obj.ToString().Length>0) lst.tso_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["splash_starttime"];if (obj.ToString().Length>0) lst.splash_starttime=obj.ToString();

     			obj=dt.Rows[RowIndex]["splash_endtime"];if (obj.ToString().Length>0) lst.splash_endtime=obj.ToString();
     			obj=dt.Rows[RowIndex]["splash_duration"];if (obj.ToString().Length>0) lst.splash_duration=obj.ToString();
     			obj=dt.Rows[RowIndex]["o2_press"];if (obj.ToString().Length>0) lst.o2_press=obj.ToString();
     			obj=dt.Rows[RowIndex]["o2_flux"];if (obj.ToString().Length>0) lst.o2_flux=obj.ToString();
     			obj=dt.Rows[RowIndex]["n2_press"];if (obj.ToString().Length>0) lst.n2_press=obj.ToString();

     			obj=dt.Rows[RowIndex]["n2_flux"];if (obj.ToString().Length>0) lst.n2_flux=obj.ToString();
     			obj=dt.Rows[RowIndex]["sheetiron_wgt"];if (obj.ToString().Length>0) lst.sheetiron_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["restrin_wgt"];if (obj.ToString().Length>0) lst.restrin_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["alloycao_wgt"];if (obj.ToString().Length>0) lst.alloycao_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["cadd_wgt"];if (obj.ToString().Length>0) lst.cadd_wgt=obj.ToString();

     			obj=dt.Rows[RowIndex]["fesi_wgt"];if (obj.ToString().Length>0) lst.fesi_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["al_wgt"];if (obj.ToString().Length>0) lst.al_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["mnsi_wgt"];if (obj.ToString().Length>0) lst.mnsi_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["femn_wgt"];if (obj.ToString().Length>0) lst.femn_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["fenb_wgt"];if (obj.ToString().Length>0) lst.fenb_wgt=obj.ToString();

     			obj=dt.Rows[RowIndex]["hscrw_wgt"];if (obj.ToString().Length>0) lst.hscrw_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["lscrw_wgt"];if (obj.ToString().Length>0) lst.lscrw_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["sscrw_wgt"];if (obj.ToString().Length>0) lst.sscrw_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["mfemn_wgt"];if (obj.ToString().Length>0) lst.mfemn_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["lfemn_wgt"];if (obj.ToString().Length>0) lst.lfemn_wgt=obj.ToString();

     			obj=dt.Rows[RowIndex]["duststeam_vol"];if (obj.ToString().Length>0) lst.duststeam_vol=obj.ToString();
     			obj=dt.Rows[RowIndex]["dustwater_vol"];if (obj.ToString().Length>0) lst.dustwater_vol=obj.ToString();
     			obj=dt.Rows[RowIndex]["recyclesteam_vol"];if (obj.ToString().Length>0) lst.recyclesteam_vol=obj.ToString();
     			obj=dt.Rows[RowIndex]["outsteam_vol"];if (obj.ToString().Length>0) lst.outsteam_vol=obj.ToString();
     			
     			obj=dt.Rows[RowIndex]["ladlear_act"];if (obj.ToString().Length>0) lst.ladlear_act=obj.ToString();     			
     			obj=dt.Rows[RowIndex]["rdolo_wgt"];if (obj.ToString().Length>0) lst.rdolo_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["change_wgt"];if (obj.ToString().Length>0) lst.change_wgt=obj.ToString();

     			obj=dt.Rows[RowIndex]["burnslag_wgt"];if (obj.ToString().Length>0) lst.burnslag_wgt=obj.ToString();
                obj = dt.Rows[RowIndex]["lfslag_wgt"]; if (obj.ToString().Length > 0) lst.lfslag_wgt = obj.ToString();
     			obj=dt.Rows[RowIndex]["sicabei_wgt"];if (obj.ToString().Length>0) lst.sicabei_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["sialfe_wgt"];if (obj.ToString().Length>0) lst.sialfe_wgt=obj.ToString();
     			obj=dt.Rows[RowIndex]["mn_wgt"];if (obj.ToString().Length>0) lst.mn_wgt=obj.ToString();

                str = dt.Rows[RowIndex]["IRON_LADLE_ID"].ToString(); if (str.Length > 0) lst.IRON_LADLE_ID = str;
                str = dt.Rows[RowIndex]["IRON_ID"].ToString(); if (str.Length > 0) lst.IRON_ID = str;
                str = dt.Rows[RowIndex]["HM_WEIGHT"].ToString(); if (str.Length > 0) lst.HM_WEIGHT = str;
                str = dt.Rows[RowIndex]["HM_TRPMTURE"].ToString(); if (str.Length > 0) lst.HM_TRPMTURE = str;
                str = dt.Rows[RowIndex]["HM_TIME"].ToString(); if (str.Length > 0) lst.HM_TIME = str;
                str = dt.Rows[RowIndex]["HM_SOUREC"].ToString(); if (str.Length > 0) lst.HM_SOUREC = str;

                str = dt.Rows[RowIndex]["SCRAP_BUCKET_ID"].ToString(); if (str.Length > 0) lst.SCRAP_BUCKET_ID = str;
                str = dt.Rows[RowIndex]["SCRAP_ID"].ToString(); if (str.Length > 0) lst.SCRAP_ID = str;
                str = dt.Rows[RowIndex]["SCRAP_WEIGHT"].ToString(); if (str.Length > 0) lst.SCRAP_WEIGHT = str;
                str = dt.Rows[RowIndex]["HSCRW_WEIGHT"].ToString(); if (str.Length > 0) lst.HSCRW_WEIGHT = str;
                str = dt.Rows[RowIndex]["LSCRW_WEIGHT"].ToString(); if (str.Length > 0) lst.LSCRW_WEIGHT = str;
                str = dt.Rows[RowIndex]["SSCRW_WEIGHT"].ToString(); if (str.Length > 0) lst.SSCRW_WEIGHT = str;
    
                LST.Add(lst);                 
            }
            dt.Dispose();
            
            return LST;
        }

         public void BOFHeatInfo_Ini(ref BOFHeatInfo lst)
         { 
            lst.Heat_id=" ";        lst.Treatpos=" ";        lst.plan_no=" ";        lst.pono=" ";        lst.Steel_grade=" ";
            lst.promodecode=" ";        lst.bof_campaign=" ";        lst.bof_life=" ";        lst.tappinghole=" ";        lst.tap_hole_campaign=" ";
            lst.tap_hole_life=" ";        lst.mainlance_id=" ";        lst.mainlance_life=" ";        lst.sublance_id=" ";        lst.sublance_life=" ";
            lst.bath_level=" ";        lst.steelladleid=" ";        lst.slag_cal_weight=" ";        lst.slag_net_weight=" ";        lst.weight_cal=" ";
            lst.weight_act=" ";        lst.weighting_time=" ";        lst.tem_act=" ";        lst.tem_time=" ";
            lst.bofc_act=" ";        lst.o2ppm_act=" ";        lst.ladleid=" ";        lst.hmw_act=" ";
            lst.hm_tem=" ";        lst.hm_c=" ";        lst.hm_si=" ";        lst.hm_mn=" ";        lst.hm_p=" ";
            lst.hm_s=" ";        lst.bucketid=" ";        lst.scrw_act=" ";        lst.pi_act=" ";        lst.return_act=" ";
            lst.metal_act=" ";        lst.cao_weight=" ";        lst.dolo_weight=" ";        lst.rdolo_weight=" ";
            lst.mgo_weight=" ";        lst.caf2_weight=" ";        lst.iron_weight=" ";        lst.o2_act=" ";        lst.ar_act=" ";
            lst.n2_act=" ";        lst.tsc_tem=" ";        lst.tsc_c=" ";        lst.tsc_duration=" ";
            lst.tso_tem=" ";        lst.tso_c=" ";        lst.tso_o2ppm=" ";        lst.tso_duration=" ";
            lst.o2_duration=" ";        lst.ar_duration=" ";        lst.n2_duration=" ";        lst.after_stiring_duration=" ";
            lst.reblow_num=" ";        lst.reblow1_tem=" ";        lst.reblow2_tem=" ";        lst.deslag_num=" ";
            lst.slag_splash_n2=" ";        lst.Ready_time=" ";        lst.charging_starttime=" ";        lst.hm_time=" ";
            lst.scrap_time=" ";        lst.charging_endtime=" ";        lst.blow_starttime=" ";        lst.blow_endtime=" ";
            lst.reblow1_starttime=" ";  lst.reblow1_endtime=" ";        lst.reblow1_duration=" ";        lst.reblow2_starttime=" ";
            lst.reblow2_endtime=" ";    lst.reblow2_duration=" ";        lst.slag_nr=" ";        lst.tapping_starttime=" ";
            lst.tapping_endtime=" ";    lst.tapping_duration=" ";        lst.slag_starttime=" ";        lst.slag_endtime=" ";
            lst.slag_duration=" ";      lst.Product_day=" ";        lst.shift_id=" ";        lst.crew_id=" ";
            lst.operator_c=" ";         lst.checkdate=" ";        lst.checkoperator=" ";        lst.checkflag=" ";        lst.ge_no=" ";
            lst.tsc_starttime=" ";      lst.tsc_endtime=" ";        lst.tso_starttime=" ";        lst.tso_endtime=" ";
            lst.splash_starttime=" ";   lst.splash_endtime=" ";        lst.splash_duration=" ";
            lst.o2_press=" ";           lst.o2_flux=" ";        lst.n2_press=" ";        lst.n2_flux=" ";        lst.sheetiron_wgt=" ";
            lst.restrin_wgt=" ";        lst.alloycao_wgt=" ";        lst.cadd_wgt=" ";        lst.fesi_wgt=" ";        lst.al_wgt=" ";
            lst.mnsi_wgt=" ";           lst.femn_wgt=" ";        lst.fenb_wgt=" ";        lst.hscrw_wgt=" ";        lst.lscrw_wgt=" ";
            lst.sscrw_wgt=" ";          lst.mfemn_wgt=" ";        lst.lfemn_wgt=" ";        lst.duststeam_vol=" ";
            lst.dustwater_vol=" ";      lst.recyclesteam_vol=" ";        lst.outsteam_vol=" ";        lst.mainlance_id1=" ";
            lst.mainlance_life1=" ";    lst.ladlear_act=" ";        lst.ironid=" ";        lst.rdolo_wgt=" ";
            lst.change_wgt=" ";        lst.burnslag_wgt=" ";        lst.lfslag_wgt=" ";        lst.sicabei_wgt=" ";
            lst.sialfe_wgt=" ";        lst.mn_wgt=" ";

            lst.IRON_LADLE_ID=" ";        lst.IRON_ID=" ";        lst.HM_WEIGHT=" ";        lst.HM_TRPMTURE=" ";
            lst.HM_TIME=" ";        lst.HM_SOUREC=" ";

            lst.SCRAP_BUCKET_ID=" ";         lst.SCRAP_ID=" ";        lst.SCRAP_WEIGHT=" ";        lst.HSCRW_WEIGHT=" ";
            lst.LSCRW_WEIGHT=" ";        lst.SSCRW_WEIGHT=" ";
         }

         public List<BOFKeyEvents> GetBOFKeyEvents(string HeatID)
        {
            List<BOFKeyEvents> LST = new List<BOFKeyEvents>();
            BOFKeyEvents lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);

            string str="";

            DateTime StartDateTime=DateTime.Now;
            //工序起始时间            
            string strSQL = "SELECT ready_time FROM SM_BOF_HEAT WHERE heat_id='" + HeatID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count > 0) str = dt.Rows[0]["ready_time"].ToString(); if (str.Length > 0) StartDateTime = Convert.ToDateTime(str);
            
             //物料添加表
            strSQL = "SELECT * FROM SM_BOF_HEAT WHERE heat_id='" + HeatID + "'";
            dt = sqt.ReadDatatable_OraDB(strSQL);
            if (dt.Rows.Count > 0) 
            {
                lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);
                lst.Decription="铁水";
                str = dt.Rows[0]["hm_time"].ToString(); if (str.Length > 0) { lst.Datetime = str; } else { lst.Datetime = StartDateTime.ToString(); }
                str = dt.Rows[0]["hm_weight"].ToString(); if (str.Length > 0) lst.Weight = str;
                str = dt.Rows[0]["hm_trpmture"].ToString(); if (str.Length > 0) lst.Temp = str;
                LST.Add(lst);

                lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);
                lst.Decription = "废钢";
                lst.Datetime = StartDateTime.ToString();
                str = dt.Rows[0]["scrap_weight"].ToString(); if (str.Length > 0) lst.Weight = str;
                LST.Add(lst);

                lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);
                lst.Decription = "TSC";
                str = dt.Rows[0]["tsc_starttime"].ToString(); if (str.Length > 0) lst.Datetime = str;
                str = dt.Rows[0]["tsc_tem"].ToString();if (str.Length >0) lst.Temp = str;
                str = dt.Rows[0]["tsc_c"].ToString(); if (str.Length > 0) lst.Ele_C = Convert.ToDouble(str).ToString("#0.0000");
                LST.Add(lst);

                lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);
                lst.Decription = "TSO";
                str = dt.Rows[0]["tso_starttime"].ToString(); if (str.Length > 0) lst.Datetime = str;
                str = dt.Rows[0]["tso_tem"].ToString();if (str.Length >0) lst.Temp  = str;
                str = dt.Rows[0]["tso_c"].ToString(); if (str.Length > 0) lst.Ele_C = Convert.ToDouble(str).ToString("#0.0000");
                str = dt.Rows[0]["tso_o2ppm"].ToString(); if (str.Length > 0) lst.O2ppm = str.Split(new char[] { '.' })[0];
                LST.Add(lst);
            }

             //化验值
            strSQL = "SELECT * FROM  sm_addition WHERE heat_id='" + HeatID + "' AND device_no='LY210_BOF'";
            dt= sqt.ReadDatatable_OraDB (strSQL );
             
            for  (int RowIndex=0;RowIndex <dt.Rows.Count;RowIndex++)
            {
                 lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);
                 lst.Decription = "加料";
                 lst.Datetime = dt.Rows[RowIndex]["add_time"].ToString();
                 lst.Mat_Name = dt.Rows[RowIndex]["mat_ID"].ToString();
                 lst.Weight = dt.Rows[RowIndex]["weight"].ToString();

                 LST.Add(lst);
            }

            //化验值
            strSQL = "SELECT * FROM  sm_bof_element WHERE heat_id='" + HeatID + "'"; 
            dt= sqt.ReadDatatable_OraDB (strSQL );
             
            for  (int RowIndex=0;RowIndex <dt.Rows.Count;RowIndex++)
            {
                 lst = new BOFKeyEvents(); BOFKeyEvents_Ini(ref lst);

                 lst.Decription = "化验值";
                 lst.Datetime = dt.Rows[RowIndex]["sampletime"].ToString();

                 lst.Ele_C = dt.Rows[RowIndex]["ele_c"].ToString();
                 lst.Ele_Si = dt.Rows[RowIndex]["ele_si"].ToString();
                 lst.Ele_Mn = dt.Rows[RowIndex]["ele_mn"].ToString();

                 lst.Ele_S = dt.Rows[RowIndex]["ele_s"].ToString();
                 lst.Ele_P = dt.Rows[RowIndex]["ele_p"].ToString();
                 lst.Ele_Cu = dt.Rows[RowIndex]["ele_cu"].ToString();

                lst.Ele_As = dt.Rows[RowIndex]["ele_as"].ToString();
                lst.Ele_Sn = dt.Rows[RowIndex]["ele_Sn"].ToString();
                lst.Ele_Cr = dt.Rows[RowIndex]["ele_Cr"].ToString();

                lst.Ele_Ni = dt.Rows[RowIndex]["ele_Ni"].ToString();
                lst.Ele_Mo = dt.Rows[RowIndex]["ele_Mo"].ToString();
                lst.Ele_Ti = dt.Rows[RowIndex]["ele_Ti"].ToString();
                lst.Ele_Nb = dt.Rows[RowIndex]["ele_Nb"].ToString();
                lst.Ele_Pb = dt.Rows[RowIndex]["ele_Pb"].ToString();                 

                LST.Add(lst);                
            }
            
            
            //计算时长 
            DateTime cDateTime = new DateTime();
            TimeSpan ts = new TimeSpan();
            for (int I = 0; I < LST.Count; I++)
            {
                if (DateTime.TryParse(LST[I].Datetime , out cDateTime))
                {
                    ts = cDateTime - StartDateTime;
                    LST[I].Duration = float.Parse((ts.TotalSeconds / 60.0).ToString("#0.00"));
                }
            }

            //按照时长排序
            LST.Sort(delegate(BOFKeyEvents a, BOFKeyEvents b) { return a.Duration.CompareTo(b.Duration); });

            return LST;
        }

        //获取LF的基本信息，炉次报告
        public List<LFHeatInfo> GetLFHeatInfo(string HeatID)
        {
            List<LFHeatInfo> LST = new List<LFHeatInfo>();
            LFHeatInfo lst;
            string  str="";

            string strSQL = "SELECT * FROM SM_LF_HEAT WHERE heat_id='" + HeatID + "'";
            DataTable dt=sqt.ReadDatatable_OraDB(strSQL);
                        
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new LFHeatInfo(); LFHeatInfo_Ini(ref lst);

                str=dt.Rows[RowIndex]["heat_id"].ToString ();if (str.Length>0) lst.heat_id =str;  
                str=dt.Rows[RowIndex]["treatpos"].ToString ();if (str.Length>0) lst.treatpos=str;
                str=dt.Rows[RowIndex]["strttime"].ToString ();if (str.Length>0) lst.strttime=str; 
                str=dt.Rows[RowIndex]["endtime"].ToString ();if (str.Length>0) lst.endtime=str;
                str = dt.Rows[RowIndex]["station"].ToString(); if (str.Length > 0) lst.station = str;
                str = dt.Rows[RowIndex]["route"].ToString(); if (str.Length > 0) lst.route = str;

                str = dt.Rows[RowIndex]["strtgrade"].ToString(); if (str.Length > 0) lst.strtgrade = str;
                str = dt.Rows[RowIndex]["endgrade"].ToString(); if (str.Length > 0) lst.endgrade = str;
                str = dt.Rows[RowIndex]["strtsteeltemp"].ToString(); if (str.Length > 0) lst.strtsteeltemp = str;

                str = dt.Rows[RowIndex]["endsteeltemp"].ToString(); if (str.Length > 0) lst.endsteeltemp = str;
                str = dt.Rows[RowIndex]["strtsteelwei"].ToString(); if (str.Length > 0) lst.strtsteelwei = str;
                str = dt.Rows[RowIndex]["endsteelwei"].ToString(); if (str.Length > 0) lst.endsteelwei = str;

                str = dt.Rows[RowIndex]["endslagwei"].ToString(); if (str.Length > 0) lst.endslagwei = str;
                str = dt.Rows[RowIndex]["ladleno"].ToString(); if (str.Length > 0) lst.ladleno = str;
                str = dt.Rows[RowIndex]["ladlestatus"].ToString(); if (str.Length > 0) lst.ladlestatus = str;
                str=dt.Rows[RowIndex]["ladlewei"].ToString ();if (str.Length>0) lst.ladlewei=str;
					 	
                str=dt.Rows[RowIndex]["pon"].ToString ();if (str.Length>0) lst.pon=str;
					 	 
                str=dt.Rows[RowIndex]["slidgatelife"].ToString ();if (str.Length>0) lst.slidgatelife=str;
                str=dt.Rows[RowIndex]["slidgatebrname"].ToString ();if (str.Length>0) lst.slidgatebrname=str;
                str=dt.Rows[RowIndex]["porozlife"].ToString ();if (str.Length>0) lst.porozlife=str;
                str=dt.Rows[RowIndex]["porozbrname"].ToString ();if (str.Length>0) lst.porozbrname=str;
                str=dt.Rows[RowIndex]["emptydur"].ToString ();if (str.Length>0) lst.emptydur=str;					 	 
                str=dt.Rows[RowIndex]["eletrdholdtm"].ToString ();if (str.Length>0) lst.eletrdholdtm=str;
                str=dt.Rows[RowIndex]["totprieng"].ToString ();if (str.Length>0) lst.totprieng=str;
                str=dt.Rows[RowIndex]["totseceng"].ToString ();if (str.Length>0) lst.totseceng=str;
                str=dt.Rows[RowIndex]["gastype"].ToString ();if (str.Length>0) lst.gastype=str;
                str=dt.Rows[RowIndex]["totgas"].ToString ();if (str.Length>0) lst.totgas=str;
                str=dt.Rows[RowIndex]["shiftnr"].ToString ();if (str.Length>0) lst.shiftnr=str;
                str=dt.Rows[RowIndex]["shiftteam"].ToString ();if (str.Length>0) lst.shiftteam=str;
                str=dt.Rows[RowIndex]["monitor"].ToString ();if (str.Length>0) lst.monitor=str;
                str=dt.Rows[RowIndex]["mainoptr1"].ToString ();if (str.Length>0) lst.mainoptr1=str;
                str=dt.Rows[RowIndex]["mainoptr2"].ToString ();if (str.Length>0) lst.mainoptr2=str;

                LST.Add(lst);
             
            }
            dt.Dispose();         

            return LST;
        }

        public List<LFKeyEvents> GetLF_KeyEvents(string HeatID)
        {
            List<LFKeyEvents> LST = new List<LFKeyEvents>();
            LFKeyEvents lst = new LFKeyEvents(); LFKeyEvents_Ini(ref lst);
            string strSQL = "";
            DataTable dt = new DataTable();

            //保存开始时间
           DateTime  StartDateTime=DateTime.Now;
            
           //查找入炉时间,并从中提取事件
           strSQL = "SELECT * FROM SM_LF_HEAT WHERE heat_id='" + HeatID + "'";
           dt=sqt.ReadDatatable_OraDB(strSQL);
           if (dt.Rows.Count > 0)
           {
               StartDateTime = Convert.ToDateTime(dt.Rows[0]["strttime"]);

               lst = new LFKeyEvents(); LFKeyEvents_Ini(ref lst);
               lst.Decription = "到站";
               lst.datetime = dt.Rows[0]["strttime"].ToString();
               LST.Add(lst);

               lst = new LFKeyEvents(); LFKeyEvents_Ini(ref lst);
               lst.Decription = "离站";
               lst.datetime = dt.Rows[0]["endtime"].ToString();
               LST.Add(lst);

           }

            //测温事件
           strSQL = "select * FROM sm_TEMPTURE WHERE heat_id='" + HeatID + "' AND device_no='LY210_LF'";
            dt =sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new LFKeyEvents(); LFKeyEvents_Ini(ref lst);

                lst.Decription = "第" + dt.Rows[RowIndex]["measure_num"].ToString() + "次测温";
                lst.datetime = dt.Rows[RowIndex]["measure_time"].ToString();
                lst.Temp = dt.Rows[RowIndex]["trmpture_value"].ToString();

                LST.Add(lst);
            }
            dt.Dispose();


            //** 加料事件 **//
            strSQL = "SELECT *  FROM sm_addition WHERE heat_id='" + HeatID + "' AND device_no='LY210_LF'";
            dt =sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new LFKeyEvents(); LFKeyEvents_Ini(ref lst);

                lst.Decription = "第" + dt.Rows[RowIndex]["add_batch"].ToString() + "批加料";
                lst.datetime = dt.Rows[RowIndex]["add_time"].ToString();
                lst.MatName = dt.Rows[RowIndex]["mat_name"].ToString();
                lst.MatCode = dt.Rows[RowIndex]["mat_ID"].ToString();                
                lst.Weight = dt.Rows[RowIndex]["weight"].ToString();

                LST.Add(lst);
            }
            dt.Dispose();


            //** 化验值 **//
            strSQL = "SELECT * FROM sm_lf_element WHERE heat_id='" + HeatID + "'";
            dt =sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new LFKeyEvents(); LFKeyEvents_Ini(ref lst);

                lst.Decription = "第" + dt.Rows[RowIndex]["sample_number"].ToString() + "次化验";
                lst.datetime = dt.Rows[RowIndex]["sampletime"].ToString();
                
                lst.Ele_C = dt.Rows[RowIndex]["ele_c"].ToString();
                lst.Ele_Si = dt.Rows[RowIndex]["ele_si"].ToString();
                lst.Ele_Mn = dt.Rows[RowIndex]["ele_mn"].ToString();

                lst.Ele_S = dt.Rows[RowIndex]["ele_s"].ToString();
                lst.Ele_P = dt.Rows[RowIndex]["ele_p"].ToString();
                lst.Ele_Cu = dt.Rows[RowIndex]["ele_cu"].ToString();

                lst.Ele_As = dt.Rows[RowIndex]["ele_as"].ToString();
                lst.Ele_Sn = dt.Rows[RowIndex]["ele_Sn"].ToString();
                lst.Ele_Cr = dt.Rows[RowIndex]["ele_Cr"].ToString();

                lst.Ele_Ni = dt.Rows[RowIndex]["ele_Ni"].ToString();
                lst.Ele_Mo = dt.Rows[RowIndex]["ele_Mo"].ToString();
                lst.Ele_Ti = dt.Rows[RowIndex]["ele_Ti"].ToString();
                lst.Ele_Nb = dt.Rows[RowIndex]["ele_Nb"].ToString();
                lst.Ele_Pb = dt.Rows[RowIndex]["ele_Pb"].ToString();       

                LST.Add(lst);
                
            }
            dt.Dispose();

            //计算时长 
            DateTime cDateTime = new DateTime();
            TimeSpan ts = new TimeSpan();
            for (int I = 0; I < LST.Count; I++)
            {
                if (DateTime.TryParse(LST[I].datetime, out cDateTime))
                {
                    ts = cDateTime - StartDateTime;
                    LST[I].Duration = float.Parse((ts.TotalSeconds / 60.0).ToString("#0.00"));
                }
            }

            //按照时长排序
            LST.Sort(delegate(LFKeyEvents a, LFKeyEvents b) { return a.Duration.CompareTo(b.Duration); });

            return LST;
        }
         public void BOFKeyEvents_Ini(ref BOFKeyEvents lst)
         { 
            lst.Datetime=" ";       lst.Duration=0; lst.Decription=" ";    lst.Weight=" ";        lst.Temp=" ";
            lst.O2ppm=" ";          lst.Ele_C=" ";  lst.Ele_Si=" ";        lst.Ele_Mn=" ";        lst.Ele_S=" ";
            lst.Ele_P=" ";          lst.Ele_Cu=" "; lst.Ele_As=" ";        lst.Ele_Sn=" ";        lst.Ele_Cr=" ";
            lst.Ele_Ni=" ";         lst.Ele_Mo=" "; lst.Ele_Ti=" ";        lst.Ele_Nb=" ";        lst.Ele_Pb=" ";
            lst.Mat_Name = " ";
         }

        public void LFHeatInfo_Ini(ref LFHeatInfo lst)
        {          
            lst.heat_id=" ";        lst.treatpos=" ";       lst.strttime=" ";       lst.endtime=" ";        lst.station=" ";
            lst.route=" ";          lst.strtgrade=" ";      lst.endgrade=" ";       lst.strtsteeltemp=" ";  lst.endsteeltemp=" ";
            lst.strtsteelwei=" ";   lst.endsteelwei=" ";    lst.endslagwei=" ";     lst.ladleno=" ";
            lst.ladlestatus=" ";    lst.ladlewei=" ";       lst.pon=" ";            lst.slidgatelife=" ";
            lst.slidgatebrname=" "; lst.porozlife=" ";      lst.porozbrname=" ";    lst.emptydur=" ";
            lst.eletrdholdtm=" ";   lst.totprieng=" ";      lst.totseceng=" ";      lst.gastype=" ";
            lst.totgas=" ";         lst.shiftnr=" ";        lst.shiftteam=" ";      lst.monitor=" ";        lst.mainoptr1=" ";
            lst.mainoptr2=" ";     
        }

        public void LFKeyEvents_Ini(ref LFKeyEvents lst)
        {
            lst.datetime=" ";           lst.Duration=0;            lst.Decription=" ";       lst.MatCode=" ";
            lst.Weight=" ";            lst.Temp=" ";              lst.O2ppm=" ";            lst.Ele_C=" ";
            lst.Ele_Si=" ";            lst.Ele_Mn=" ";            lst.Ele_S=" ";            lst.Ele_P=" ";
            lst.Ele_Cu=" ";            lst.Ele_As=" ";            lst.Ele_Sn=" ";           lst.Ele_Cr=" ";
            lst.Ele_Ni=" ";            lst.Ele_Mo=" ";            lst.Ele_Ti=" ";           lst.Ele_Nb=" ";
            lst.Ele_Pb=" ";
        }
        //获取RH的基本信息，炉次报告
        public List<RHHeatInfo> GetRHHeatInfo(string HeatID)
        {
            List<RHHeatInfo> LST = new List<RHHeatInfo>();
            RHHeatInfo lst = new RHHeatInfo(); RHHeatInfo_Ini(ref lst);
            string str = "";

            string strSQL = "SELECT * FROM SM_RH_HEAT WHERE heat_id='" + HeatID + "'";
            DataTable dt =sqt.ReadDatatable_OraDB (strSQL);

            if ( dt.Rows.Count>0)
            {
                int RowIndex = 0;
                str = dt.Rows[RowIndex]["station"].ToString(); if (str.Length > 0) lst.treatpos = str;
                str = dt.Rows[RowIndex]["read_time"].ToString(); if (str.Length > 0) lst.read_time = str;
                str = dt.Rows[RowIndex]["pono"].ToString(); if (str.Length > 0) lst.pono = str;

                str = dt.Rows[RowIndex]["rh_treatment_id"].ToString(); if (str.Length > 0) lst.rh_treatment_id = str;
                str = dt.Rows[RowIndex]["heat_id"].ToString(); if (str.Length > 0) lst.heat_id = str;
                str = dt.Rows[RowIndex]["plan_no"].ToString(); if (str.Length > 0) lst.plan_no = str;
                str = dt.Rows[RowIndex]["crew_id"].ToString(); if (str.Length > 0) lst.crew_id = str;
                str = dt.Rows[RowIndex]["shift_id"].ToString(); if (str.Length > 0) lst.shift_id = str;

                str = dt.Rows[RowIndex]["steel_grade"].ToString(); if (str.Length > 0) lst.steel_grade = str;
                str = dt.Rows[RowIndex]["srp_count"].ToString(); if (str.Length > 0) lst.srp_count = str;
                str = dt.Rows[RowIndex]["div_flag"].ToString(); if (str.Length > 0) lst.div_flag = str;
                str = dt.Rows[RowIndex]["arrive_mainpos_time"].ToString(); if (str.Length > 0) lst.arrive_mainpos_time =str;
                str = dt.Rows[RowIndex]["hook_arr_ladle_time"].ToString(); if (str.Length > 0) lst.hook_arr_ladle_time =str;
                str = dt.Rows[RowIndex]["ladle_up_time"].ToString(); if (str.Length > 0) lst.ladle_up_time =str;

                str = dt.Rows[RowIndex]["heat_start"].ToString(); if (str.Length > 0) lst.heat_start =str;
                str = dt.Rows[RowIndex]["heat_end"].ToString(); if (str.Length > 0) lst.heat_end =str;
                str = dt.Rows[RowIndex]["ladle_down_time"].ToString(); if (str.Length > 0) lst.ladle_down_time =str;
                str = dt.Rows[RowIndex]["hook_leave_ladle_time"].ToString(); if (str.Length > 0) lst.hook_leave_ladle_time =str;
                str = dt.Rows[RowIndex]["arr_bwz_time"].ToString(); if (str.Length > 0) lst.arr_bwz_time =str;

                str = dt.Rows[RowIndex]["arr_departpos_time"].ToString(); if (str.Length > 0) lst.arr_departpos_time =str;
                str = dt.Rows[RowIndex]["depart_time"].ToString(); if (str.Length > 0) lst.depart_time =str;

                str = dt.Rows[RowIndex]["dati_temp_1"].ToString(); if (str.Length > 0) lst.dati_temp_1 =str;
                str = dt.Rows[RowIndex]["steel_temp_1"].ToString(); if (str.Length > 0) lst.steel_temp_1 = str;
                str = dt.Rows[RowIndex]["o2_activity_1"].ToString(); if (str.Length > 0) lst.o2_activity_1 = str;

                str = dt.Rows[RowIndex]["dati_temp_2"].ToString(); if (str.Length > 0) lst.dati_temp_2 =str;
                str = dt.Rows[RowIndex]["steel_temp_2"].ToString(); if (str.Length > 0) lst.steel_temp_2 = str;
                str = dt.Rows[RowIndex]["o2_activity_2"].ToString(); if (str.Length > 0) lst.o2_activity_2 = str;

                str = dt.Rows[RowIndex]["dati_temp_3"].ToString(); if (str.Length > 0) lst.dati_temp_3 =str;
                str = dt.Rows[RowIndex]["steel_temp_3"].ToString(); if (str.Length > 0) lst.steel_temp_3 = str;
                str = dt.Rows[RowIndex]["o2_activity_3"].ToString(); if (str.Length > 0) lst.o2_activity_3 = str;

                str = dt.Rows[RowIndex]["dati_temp_4"].ToString(); if (str.Length > 0) lst.dati_temp_4 =str;
                str = dt.Rows[RowIndex]["steel_temp_4"].ToString(); if (str.Length > 0) lst.steel_temp_4 = str;
                str = dt.Rows[RowIndex]["o2_activity_4"].ToString(); if (str.Length > 0) lst.o2_activity_4 = str;

                str = dt.Rows[RowIndex]["dati_temp_5"].ToString(); if (str.Length > 0) lst.dati_temp_5 =str;
                str = dt.Rows[RowIndex]["steel_temp_5"].ToString(); if (str.Length > 0) lst.steel_temp_5 = str;
                str = dt.Rows[RowIndex]["o2_activity_5"].ToString(); if (str.Length > 0) lst.o2_activity_5 = str;

                str = dt.Rows[RowIndex]["dati_sample_1"].ToString(); if (str.Length > 0) lst.dati_sample_1 =str;
                str = dt.Rows[RowIndex]["dati_sample_2"].ToString(); if (str.Length > 0) lst.dati_sample_2 =str;
                str = dt.Rows[RowIndex]["dati_sample_3"].ToString(); if (str.Length > 0) lst.dati_sample_3 =str;
                str = dt.Rows[RowIndex]["dati_sample_4"].ToString(); if (str.Length > 0) lst.dati_sample_4 =str;
                str = dt.Rows[RowIndex]["dati_sample_5"].ToString(); if (str.Length > 0) lst.dati_sample_5 =str;

                str = dt.Rows[RowIndex]["dati_o2_start"].ToString(); if (str.Length > 0) lst.dati_o2_start =str;
                str = dt.Rows[RowIndex]["dati_o2_end"].ToString(); if (str.Length > 0) lst.dati_o2_end =str;

                str = dt.Rows[RowIndex]["o2_dur"].ToString(); if (str.Length > 0) lst.o2_dur = str;
                str = dt.Rows[RowIndex]["o2_cons"].ToString(); if (str.Length > 0) lst.o2_cons = str;
                str = dt.Rows[RowIndex]["dati_des_start"].ToString(); if (str.Length > 0) lst.dati_des_start =str;
                str = dt.Rows[RowIndex]["dati_des_end"].ToString(); if (str.Length > 0) lst.dati_des_end =str;
                str = dt.Rows[RowIndex]["des_dur"].ToString(); if (str.Length > 0) lst.des_dur = str;
                str = dt.Rows[RowIndex]["des_gas_cons"].ToString(); if (str.Length > 0) lst.des_gas_cons = str;
                str = dt.Rows[RowIndex]["ar_lift_cons"].ToString(); if (str.Length > 0) lst.ar_lift_cons = str;

                str = dt.Rows[RowIndex]["n2_lift_cons"].ToString(); if (str.Length > 0) lst.n2_lift_cons = str;
                str = dt.Rows[RowIndex]["steam_cons"].ToString(); if (str.Length > 0) lst.steam_cons = str;
                str = dt.Rows[RowIndex]["stir_ar_start"].ToString(); if (str.Length > 0) lst.stir_ar_start =str;
                str = dt.Rows[RowIndex]["stir_ar_end"].ToString(); if (str.Length > 0) lst.stir_ar_end =str;
                str = dt.Rows[RowIndex]["stir_ar_dur"].ToString(); if (str.Length > 0) lst.stir_ar_dur = str;

                str = dt.Rows[RowIndex]["soft_stir_ar_dur"].ToString(); if (str.Length > 0) lst.soft_stir_ar_dur = str;
                str = dt.Rows[RowIndex]["ar_bb_cons"].ToString(); if (str.Length > 0) lst.ar_bb_cons = str;
                str = dt.Rows[RowIndex]["ladle_id"].ToString(); if (str.Length > 0) lst.ladle_id = str;
                str = dt.Rows[RowIndex]["begin_slag_height"].ToString(); if (str.Length > 0) lst.begin_slag_height = str;
                str = dt.Rows[RowIndex]["begin_slag_weight"].ToString(); if (str.Length > 0) lst.begin_slag_weight = str;
                str = dt.Rows[RowIndex]["begin_net_weight"].ToString(); if (str.Length > 0) lst.begin_net_weight = str;

                str = dt.Rows[RowIndex]["end_net_weight"].ToString(); if (str.Length > 0) lst.end_net_weight = str;
                str = dt.Rows[RowIndex]["vac_dur"].ToString(); if (str.Length > 0) lst.vac_dur = str;
                str = dt.Rows[RowIndex]["treat_time"].ToString(); if (str.Length > 0) lst.treat_time = str;
                str = dt.Rows[RowIndex]["min_vacuum"].ToString(); if (str.Length > 0) lst.min_vacuum = str;

                str = dt.Rows[RowIndex]["machine_cooling_water_cons"].ToString(); if (str.Length > 0) lst.machine_cooling_water_cons = str;
                str = dt.Rows[RowIndex]["condensor_cooling_water_cons"].ToString(); if (str.Length > 0) lst.condensor_cooling_water_cons = str;
                str = dt.Rows[RowIndex]["lance_no"].ToString(); if (str.Length > 0) lst.lance_no = str;
                str = dt.Rows[RowIndex]["lance_age"].ToString(); if (str.Length > 0) lst.lance_age = str;
                str = dt.Rows[RowIndex]["hot_bend_tube_no"].ToString(); if (str.Length > 0) lst.hot_bend_tube_no = str;

                str = dt.Rows[RowIndex]["hot_bend_tube_num"].ToString(); if (str.Length > 0) lst.hot_bend_tube_num = str;
                str = dt.Rows[RowIndex]["vacuum_slot_no"].ToString(); if (str.Length > 0) lst.vacuum_slot_no = str;
                str = dt.Rows[RowIndex]["vacuum_slot_num"].ToString(); if (str.Length > 0) lst.vacuum_slot_num = str;
                str = dt.Rows[RowIndex]["updown_num"].ToString(); if (str.Length > 0) lst.updown_num = str;
                str = dt.Rows[RowIndex]["ladle_tare_wt"].ToString(); if (str.Length > 0) lst.ladle_tare_wt = str;

                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }

        public void RHHeatInfo_Ini(ref RHHeatInfo lst)
        {
            lst.treatpos = " ";                         lst.msg_status = " "; lst.msg_datetime = " "; lst.read_time = " ";
            lst.pono = " ";                             lst.rh_treatment_id = " "; lst.heat_id = " "; lst.plan_no = " ";
            lst.crew_id = " ";                          lst.shift_id = " "; lst.steel_grade = " "; lst.srp_count = " ";
            lst.div_flag = " ";                         lst.arrive_mainpos_time = " "; lst.hook_arr_ladle_time = " "; lst.ladle_up_time = " ";
            lst.heat_start = " ";                       lst.heat_end = " "; lst.ladle_down_time = " "; lst.hook_leave_ladle_time = " ";
            lst.arr_bwz_time = " ";                     lst.arr_departpos_time = " "; lst.depart_time = " "; lst.dati_temp_1 = " ";
            lst.steel_temp_1 = " ";                     lst.o2_activity_1 = " "; lst.dati_temp_2 = " "; lst.steel_temp_2 = " ";
            lst.o2_activity_2 = " ";                    lst.dati_temp_3 = " "; lst.steel_temp_3 = " "; lst.o2_activity_3 = " ";
            lst.dati_temp_4 = " ";                      lst.steel_temp_4 = " "; lst.o2_activity_4 = " "; lst.dati_temp_5 = " ";
            lst.steel_temp_5 = " ";                     lst.o2_activity_5 = " "; lst.dati_sample_1 = " "; lst.dati_sample_2 = " ";
            lst.dati_sample_3 = " ";                    lst.dati_sample_4 = " "; lst.dati_sample_5 = " "; lst.dati_o2_start = " ";
            lst.dati_o2_end = " ";                      lst.o2_dur = " "; lst.o2_cons = " "; lst.dati_des_start = " ";
            lst.dati_des_end = " ";         lst.des_dur = " ";              lst.des_gas_cons = " "; lst.ar_lift_cons = " ";
            lst.n2_lift_cons = " ";         lst.steam_cons = " ";           lst.stir_ar_start = " "; lst.stir_ar_end = " ";
            lst.stir_ar_dur = " ";          lst.soft_stir_ar_dur = " ";     lst.ar_bb_cons = " "; lst.ladle_id = " ";
            lst.begin_slag_height = " ";    lst.begin_slag_weight = " ";    lst.begin_net_weight = " "; lst.end_net_weight = " ";
            lst.vac_dur = " ";              lst.treat_time = " ";           lst.min_vacuum = " "; lst.machine_cooling_water_cons = " ";
            lst.ladle_tare_wt = " ";        lst.lance_no = " ";             lst.lance_age = " "; lst.hot_bend_tube_no = " ";
            lst.hot_bend_tube_num = " ";    lst.vacuum_slot_no = " ";       lst.vacuum_slot_num = " "; lst.updown_num = " ";
             lst.condensor_cooling_water_cons = " ";
        }

        //RH关键事件
        public List<RH_KeyEvens> GetRHKeyEvents(string HeatID)
        {
            List<RH_KeyEvens> LST = new List<RH_KeyEvens>();
            RH_KeyEvens lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
            string str = "";

            DateTime StartDateTime=DateTime.Now;           

            //报告中的各项事件
            string strSQL = "SELECT * FROM SM_RH_HEAT WHERE heat_id='" + HeatID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int I = 0; I < dt.Rows.Count; I++)
            {
                str = dt.Rows[I]["arrive_mainpos_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "到达时间";
                    lst.DateAndTime =str;                    
                    LST.Add(lst);
                }
                //就绪时间
                StartDateTime = Convert.ToDateTime(lst.DateAndTime);

                str = dt.Rows[I]["hook_arr_ladle_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "吊钩到达时间";
                    lst.DateAndTime =str;                                        
                    LST.Add(lst);
                }


                str = dt.Rows[I]["ladle_up_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "钢包上升时间";
                    lst.DateAndTime =str;                   
                    LST.Add(lst);
                }

                str = dt.Rows[I]["heat_start"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "开始加热时间";
                    lst.DateAndTime =str;                
                    LST.Add(lst);
                }



                str = dt.Rows[I]["heat_end"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "结束加热时间";
                    lst.DateAndTime =str;                    
                    LST.Add(lst);
                }



                str = dt.Rows[I]["ladle_down_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "钢包下降时间";
                    lst.DateAndTime =str;                  
                    LST.Add(lst);
                }

                str = dt.Rows[I]["hook_leave_ladle_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "吊钩离开钢包时间";
                    lst.DateAndTime =str;                 
                    LST.Add(lst);
                }

                str = dt.Rows[I]["arr_bwz_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "到达bwz时间";
                    lst.DateAndTime =str;                   
                    LST.Add(lst);
                }



                str = dt.Rows[I]["arr_departpos_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "到达离开位时间";
                    lst.DateAndTime =str;                  
                    LST.Add(lst);
                }



                str = dt.Rows[I]["depart_time"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "离开时间";
                    lst.DateAndTime =str;                  
                    LST.Add(lst);
                }


                for (int J = 1; J <= 5; J++)
                {
                    str = dt.Rows[I]["dati_temp_" + J].ToString();
                    if (str.Length > 0)
                    {
                        lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                        lst.Decription = "第" + J + "定温定氧";
                        lst.DateAndTime =str;
                        str = dt.Rows[I]["steel_temp_" + J].ToString(); if (str.Length > 0) lst.Temp = str;
                        str = dt.Rows[I]["o2_activity_" + J].ToString(); if (str.Length > 0) lst.O2ppm = str;
                        LST.Add(lst);
                    }

                }


                str = dt.Rows[I]["dati_o2_start"].ToString(); if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "吹氧开始";
                    lst.DateAndTime =str;                   
                    LST.Add(lst);
                }
                str = dt.Rows[I]["dati_o2_end"].ToString(); if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "吹氧结束";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }

                str = dt.Rows[I]["dati_des_start"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "处理开始";
                    lst.DateAndTime =str;
                    LST.Add(lst);
                }


                str = dt.Rows[I]["dati_des_end"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "处理结束";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }

                str = dt.Rows[I]["stir_ar_start"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "搅拌开始";
                    lst.DateAndTime =str;
                    LST.Add(lst);
                }

                str = dt.Rows[I]["stir_ar_end"].ToString();
                if (str.Length > 0)
                {
                    lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                    lst.Decription = "搅拌结束";
                    lst.DateAndTime = str;
                    LST.Add(lst);
                }
            }
            dt.Dispose();


            //化验成分
            strSQL = "SELECT * FROM  sm_rh_element  WHERE heat_id='" + HeatID + "'";
            dt =sqt.ReadDatatable_OraDB(strSQL);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new RH_KeyEvens(); RH_KeyEvens_Ini(ref lst);
                lst.Decription = "化验值";
                lst.DateAndTime = dt.Rows[I]["sampletime"].ToString();

                str = dt.Rows[I]["Ele_C"].ToString(); if (str.Length > 0) lst.Ele_C = str;
                str = dt.Rows[I]["Ele_ALS"].ToString(); if (str.Length > 0) lst.Ele_Als = str;
                str = dt.Rows[I]["Ele_ALT"].ToString(); if (str.Length > 0) lst.Ele_Alt = str;
                str = dt.Rows[I]["Ele_AS"].ToString(); if (str.Length > 0) lst.Ele_As = str;
                str = dt.Rows[I]["Ele_B"].ToString(); if (str.Length > 0) lst.Ele_B = str;

                str = dt.Rows[I]["Ele_BI"].ToString(); if (str.Length > 0) lst.Ele_Bi = str;
                str = dt.Rows[I]["Ele_C"].ToString(); if (str.Length > 0) lst.Ele_C = str;
                str = dt.Rows[I]["Ele_CA"].ToString(); if (str.Length > 0) lst.Ele_Ca = str;
                str = dt.Rows[I]["Ele_CE"].ToString(); if (str.Length > 0) lst.Ele_Ce = str;
                str = dt.Rows[I]["Ele_CEQ"].ToString(); if (str.Length > 0) lst.Ele_Ceq = str;

                str = dt.Rows[I]["Ele_CO"].ToString(); if (str.Length > 0) lst.Ele_Co = str;
                str = dt.Rows[I]["Ele_CR"].ToString(); if (str.Length > 0) lst.Ele_Cr = str;
                str = dt.Rows[I]["Ele_CU"].ToString(); if (str.Length > 0) lst.Ele_Cu = str;
                str = dt.Rows[I]["Ele_MG"].ToString(); if (str.Length > 0) lst.Ele_Mg = str;
                str = dt.Rows[I]["Ele_MN"].ToString(); if (str.Length > 0) lst.Ele_Mn = str;

                str = dt.Rows[I]["Ele_MO"].ToString(); if (str.Length > 0) lst.Ele_Mo = str;
                str = dt.Rows[I]["Ele_N"].ToString(); if (str.Length > 0) lst.Ele_N = str;
                str = dt.Rows[I]["Ele_NB"].ToString(); if (str.Length > 0) lst.Ele_Nb = str;
                str = dt.Rows[I]["Ele_NI"].ToString(); if (str.Length > 0) lst.Ele_Ni = str;
                str = dt.Rows[I]["Ele_P"].ToString(); if (str.Length > 0) lst.Ele_P = str;

                str = dt.Rows[I]["Ele_PB"].ToString(); if (str.Length > 0) lst.Ele_Pb = str;
                str = dt.Rows[I]["Ele_S"].ToString(); if (str.Length > 0) lst.Ele_S = str;
                str = dt.Rows[I]["Ele_SB"].ToString(); if (str.Length > 0) lst.Ele_Sb = str;
                str = dt.Rows[I]["Ele_SI"].ToString(); if (str.Length > 0) lst.Ele_Si = str;
                str = dt.Rows[I]["Ele_SN"].ToString(); if (str.Length > 0) lst.Ele_Sn = str;

                str = dt.Rows[I]["Ele_TI"].ToString(); if (str.Length > 0) lst.Ele_Ti = str;
                str = dt.Rows[I]["Ele_V"].ToString(); if (str.Length > 0) lst.Ele_V = str;
                str = dt.Rows[I]["Ele_W"].ToString(); if (str.Length > 0) lst.Ele_W = str;
                str = dt.Rows[I]["Ele_ZR"].ToString(); if (str.Length > 0) lst.Ele_Zr = str;

                LST.Add(lst);
            }

            //计算时长 
            DateTime cDateTime = new DateTime();
            TimeSpan ts = new TimeSpan();
            for (int I = 0; I < LST.Count; I++)
            {
                if (DateTime.TryParse(LST[I].DateAndTime , out cDateTime))
                {
                    ts  = cDateTime - StartDateTime;
                    LST[I].Duration =float.Parse (ts.TotalMinutes.ToString("#0.0"));
                }
            }

            //按照时长排序
            LST.Sort(delegate(RH_KeyEvens a, RH_KeyEvens b) { return a.Duration.CompareTo(b.Duration); });

            return LST;
        }
        //由于在RH的表中，时间是按照字符串给出的“20140310152337”，因此需要变为日期格式"2014/03/10 15:23:37"
        public string ChageString2Date(string strDateTime)
        {

            string ReDate = "";
            try
            {
                if (strDateTime.Length >= 4) ReDate = strDateTime.Substring(0, 4);
                if (strDateTime.Length >= 6) ReDate = ReDate + "/" + strDateTime.Substring(4, 2);
                if (strDateTime.Length >= 8) ReDate = ReDate + "/" + strDateTime.Substring(6, 2);

                if (strDateTime.Length >= 10) ReDate = ReDate + " " + strDateTime.Substring(8, 2);
                if (strDateTime.Length >= 12) ReDate = ReDate + ":" + strDateTime.Substring(10, 2);
                if (strDateTime.Length >= 14) ReDate = ReDate + ":" + strDateTime.Substring(12, 2);

                return ReDate;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return strDateTime;
            }
        }

        public void RH_KeyEvens_Ini(ref RH_KeyEvens lst)
        {
            lst.DateAndTime = " ";  lst.Duration = 0;   lst.Decription = " ";   lst.MatCode = " ";
            lst.Weight = " ";       lst.Temp = " ";     lst.O2ppm = " ";        lst.Ele_C = " ";
            lst.Ele_Si = " ";       lst.Ele_Mn = " ";   lst.Ele_S = " ";        lst.Ele_P = " ";
            lst.Ele_Cu = " ";       lst.Ele_As = " ";   lst.Ele_Sn = " ";       lst.Ele_Cr = " ";
            lst.Ele_Ni = " ";       lst.Ele_Mo = " ";   lst.Ele_Ti = " ";       lst.Ele_Nb = " ";
            lst.Ele_Pb = " ";
        }

        public void GetReportPdf_RH_HisDB_Vacuum_CycGas(string HeatID, string TreatPos, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        { 
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "真空度";
            stuDrawPicInfo.TagUnit[TagNo] = "KPa";
            stuDrawPicInfo.YSN[TagNo] = 12;
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 120; 

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "循环Ar流量";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 300;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "循环O2流量";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 3000;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "底吹透气砖流量";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 400;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "底吹透气砖流量";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;             
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 400;


            //绘图的数据
            string[] tags = new string[5];
            tags[0] = "LYQ210.RH" + TreatPos + ".VacuumValue";
            tags[1] = "LYQ210.RH" + TreatPos + ".CycArFlowLift";
            tags[2] = "LYQ210.RH" + TreatPos + ".CycBlowingO2Flow";
            tags[3] = "LYQ210.RH" + TreatPos + ".ProcVolPlug1Stir";
            tags[4] = "LYQ210.RH" + TreatPos + ".ProcVolPlug2Stir";
                        
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次 真空与循环吹气过程", stuDrawPicInfo, ref pdfDocument);
        }        

        public void GetReportPdf_RH_HisDB_Flue(string HeatID, string TreatPos, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        { 
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "废气流量";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1500;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "Ar含量";
            stuDrawPicInfo.TagUnit[TagNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 300;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "CO含量";
            stuDrawPicInfo.TagUnit[TagNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 3000;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "CO2含量";
            stuDrawPicInfo.TagUnit[TagNo] = "%";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = true;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1200;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "H含量";
            stuDrawPicInfo.TagUnit[TagNo] = "%";
            stuDrawPicInfo.YSN[TagNo] = 12;
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 120;

            //绘图的数据
            string[] tags = new string[5];
            //tags[3] = "LYQ210.RH" + TreatPos + ".FluxAr";
            //tags[4] = "LYQ210.RH" + TreatPos + ".FluxN2";
            //tags[5] = "LYQ210.RH" + TreatPos + ".FluxO2";

            tags[0] = "LYQ210.RH" + TreatPos + ".FlueGasFlux";
            tags[1] = "LYQ210.RH" + TreatPos + ".FlueGasAr";
            tags[2] = "LYQ210.RH" + TreatPos + ".FlueGasCO";
            tags[3] = "LYQ210.RH" + TreatPos + ".FlueGasCO2";
            tags[4] = "LYQ210.RH" + TreatPos + ".FlueGasH2";
            

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(HeatID + "炉次 真空与循环吹气过程", stuDrawPicInfo, ref pdfDocument);

        }

        public void GetReportPdf_CC(string HeatID, ref  iTextSharp.text.Document pdfDocument)
        {
            //转为竖直放置的页面
            pdfDocument.setPageSize(PageSize.A4);

            //新开一个页面
            pdfDocument.newPage();

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(HeatID + "炉次 连铸工序数据追踪（综合）", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);


            //基本信息了
            pdfDocument.Add(new Paragraph(" "));//空行

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //获取基本信息
            List<CCHeatInfo> lst =GetCCHeatInfo(HeatID);
            if (lst.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("炉次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Heat_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("计划钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Steel_grade_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("最终钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Final_steel_grade_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("改判原因", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].alteration_reason_code, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("铸机号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].CCM, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("班次", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].shift_code , FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("班组", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].team_id , FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("液相线", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Liquidus_temp, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("route_id", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].route_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("practice_id", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].practice_id, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("seq_counter", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].seq_counter, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("seq_heat_counter", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].seq_heat_counter, FontSong)); table.addCell(cell);
                
                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("seq_total_heats", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].seq_total_heats, FontSong)); table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("tapped_wgt", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tapped_wgt, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("task_counter", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].task_counter, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);
                

                //新行，标题
                table.DefaultCellBackgroundColor  = Color_DescCell;
                cell = new iTextSharp.text.Cell(new Paragraph("钢包ID", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("包龄", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢包到达", FontKai));  table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢包到重", FontKai)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph("钢包打开", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢包开重", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢包关闭", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢包闭重", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("浇铸时长", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("钢包自开", FontKai)); table.addCell(cell);

                //新行，数据
                table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White); ;
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ladle_id, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ladle_life, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Ladle_arrival_date, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].ladle_arrival_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Ladle_opening_date, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Ladle_opening_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Ladle_close_date, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Ladle_close_wgt, FontSong)); table.addCell(cell);
                TimeSpan ts = Convert.ToDateTime(lst[0].Ladle_close_date) - Convert.ToDateTime(lst[0].Ladle_opening_date);
                cell = new iTextSharp.text.Cell(new Paragraph(ts.TotalMinutes.ToString ("#0.0")+"分钟" , FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("----", FontSong)); table.addCell(cell);

                //新行，标题
                table.DefaultCellBackgroundColor = Color_DescCell;
                cell = new iTextSharp.text.Cell(new Paragraph("中包ID", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("中包包龄", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("台车代号", FontKai)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph("预热时间", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("预热温度", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("重量", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("skull_wgt", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("覆盖剂类型", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("覆盖剂重量", FontKai)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph("", FontKai)); table.addCell(cell);

                //新行，数据
                table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_id, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_life, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_car_code, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_preheat_time, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Tundish_preheat_temperature, FontSong)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_at_ladle_open_wgt, FontSong)); table.addCell(cell);                
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_skull_wgt , FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_powder_type, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].tundish_powder_wgt, FontSong)); table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);
            }

            cell = new iTextSharp.text.Cell(new Paragraph("生成铸坯", FontKai)); table.addCell(cell);
            List<string> LST_SlabID = GetSlabIDListFromHeatID(HeatID);
            string SlabList = "";
            for (int I = 0; I < LST_SlabID.Count; I++)
            {
                SlabList=SlabList + LST_SlabID[I]+";";
            }
            cell = new iTextSharp.text.Cell(new Paragraph(SlabList, FontSong)); cell.HorizontalAlignment =Element.ALIGN_LEFT; cell.Colspan = 9; table.addCell(cell);
            

            //把表写入文档
            pdfDocument.Add(table);

            //然后逐铸坯写入报告
            for (int I = 0; I < LST_SlabID.Count; I++)
            {
                GetReportPdf_Slab(LST_SlabID[I], ref pdfDocument);
            }

        }

        public List<string> GetSlabIDListFromHeatID(string HeatID)
        {

            //记录有哪些炉号列表
            List<string> LST = new List<string>();

            //先从 中得出铸机号、开始、结束时间
            string strSQL = "SELECT ccm,ladle_opening_date,ladle_close_date FROM sm_CCM_HEAT WHERE heat_id='" + HeatID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);

            //放置以外发生
            if (dt.Rows.Count <= 0) return LST;

            string CCM = dt.Rows[0][0].ToString();
            DateTime  StartTime  =Convert.ToDateTime ( dt.Rows[0][1]);
            DateTime  StopTime = Convert.ToDateTime (dt.Rows[0][2]);

            strSQL = "SELECT slab_no FROM SLAB_L2_REPORTS_VIEW"
                    + " WHERE ccm='"+CCM +"'"
                    + " AND ( "
                    + "    ( start_time <=to_date('" + StartTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') AND stop_time >=to_date('" + StartTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') )"
                    + " OR ( start_time >=to_date('" + StartTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') AND stop_time <=to_date('" + StopTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') )"
                    + " OR ( start_time <=to_date('" + StopTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') AND stop_time >=to_date('" + StopTime.ToString() + "', 'yyyy-mm-dd hh24:mi:ss') )"
                    + "    )";
            dt = sqt.ReadDatatable_OraDB(strSQL);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                LST.Add(dt.Rows[I][0].ToString());
            }

            return LST;
        }

        public List<CCHeatInfo> GetCCHeatInfo(string HeatID)
        {
            List<CCHeatInfo> LST = new List<CCHeatInfo>();
            CCHeatInfo lst = new CCHeatInfo(); CCHeatInfo_Ini(ref lst);
            string str = "";

            string strSQL = "SELECT * FROM sm_ccm_heat WHERE heat_id='" + HeatID + "'";
            DataTable dt =sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new CCHeatInfo(); CCHeatInfo_Ini(ref lst);


                lst.Heat_id = dt.Rows[RowIndex]["heat_id"].ToString();
                lst.CCM = dt.Rows[RowIndex]["ccm"].ToString();
                lst.station_code = dt.Rows[RowIndex]["station_code"].ToString();
                lst.Steel_grade_id = dt.Rows[RowIndex]["steel_grade_id"].ToString();
                lst.Final_steel_grade_id = dt.Rows[RowIndex]["final_steel_grade_id"].ToString();
                lst.alteration_reason_code = dt.Rows[RowIndex]["alteration_reason_code"].ToString(); 
                lst.shift_code = dt.Rows[RowIndex]["shift_code"].ToString();
                lst.shift_responsible = dt.Rows[RowIndex]["shift_responsible"].ToString();
                lst.team_id = dt.Rows[RowIndex]["team_id"].ToString();


                lst.Liquidus_temp = dt.Rows[RowIndex]["liquidus_temp"].ToString();
                lst.route_id = dt.Rows[RowIndex]["route_id"].ToString();
                lst.practice_id = dt.Rows[RowIndex]["practice_id"].ToString();
                lst.seq_counter = dt.Rows[RowIndex]["seq_counter"].ToString();
                lst.seq_heat_counter = dt.Rows[RowIndex]["seq_heat_counter"].ToString();
                lst.seq_total_heats = dt.Rows[RowIndex]["seq_total_heats"].ToString();


                lst.ladle_id = dt.Rows[RowIndex]["ladle_id"].ToString();
                lst.ladle_life = dt.Rows[RowIndex]["ladle_life"].ToString();
                lst.Ladle_arrival_date = dt.Rows[RowIndex]["ladle_arrival_date"].ToString();
                str = dt.Rows[RowIndex]["ladle_arrival_wgt"].ToString();lst.ladle_arrival_wgt =str.Split(new char []{'.'})[0];
                str = dt.Rows[RowIndex]["ladle_tare_wgt"].ToString(); lst.ladle_tare_wgt = str.Split(new char[] { '.' })[0];
                lst.Ladle_opening_date = Convert.ToDateTime(dt.Rows[RowIndex]["ladle_opening_date"]).ToString("yyyy/MM/dd HH:mm:ss");
                str = dt.Rows[RowIndex]["ladle_opening_wgt"].ToString(); lst.Ladle_opening_wgt = str.Split(new char[] { '.' })[0];
                lst.Ladle_close_date = dt.Rows[RowIndex]["ladle_close_date"].ToString();
                str = dt.Rows[RowIndex]["ladle_close_wgt"].ToString(); lst.Ladle_close_wgt = str.Split(new char[] { '.' })[0];
                 

                lst.Start_date = dt.Rows[RowIndex]["start_date"].ToString();
                str = dt.Rows[RowIndex]["start_wgt"].ToString(); lst.Start_wgt=str;//lst.start_wgt= Convert.ToInt32 (str).ToString();
                lst.Stop_date = dt.Rows[RowIndex]["stop_date"].ToString();

                str = dt.Rows[RowIndex]["tapped_wgt"].ToString(); lst.tapped_wgt = str;//lst.tapped_wgt = Convert.ToInt32(str).ToString(); 
                lst.task_counter = dt.Rows[RowIndex]["task_counter"].ToString();

                str = dt.Rows[RowIndex]["tundish_at_ladle_open_wgt"].ToString(); lst.tundish_at_ladle_open_wgt = str.Split(new char[] { '.' })[0];
                lst.tundish_car_code = dt.Rows[RowIndex]["tundish_car_code"].ToString();
                lst.tundish_life = dt.Rows[RowIndex]["tundish_life"].ToString();
                str = dt.Rows[RowIndex]["tundish_id"].ToString(); lst.tundish_id = str;
                str = dt.Rows[RowIndex]["tundish_preheat_time"].ToString(); lst.tundish_preheat_time = str;
                str = dt.Rows[RowIndex]["tundish_preheat_temperature"].ToString(); lst.Tundish_preheat_temperature = str;
                str = dt.Rows[RowIndex]["tundish_skull_wgt"].ToString(); lst.tundish_skull_wgt = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["tundish_powder_type"].ToString(); lst.tundish_powder_type = str;
                str = dt.Rows[RowIndex]["tundish_powder_wgt"].ToString(); lst.tundish_powder_wgt = str.Split(new char[] { '.' })[0];

                LST.Add(lst);
            }

            dt.Dispose();
            return LST;
        }

        public void CCHeatInfo_Ini(ref CCHeatInfo lst)
        { 
            lst.report_counter="-";             lst.task_counter="-";

            lst.Heat_id = "-"; lst.CCM = "-"; lst.po_id = "-"; lst.area_id = "-"; lst.station_code = "-"; lst.Steel_grade_id = "-"; lst.Final_steel_grade_id = "-";
            lst.alteration_reason_code="-";     lst.route_id="-";        lst.practice_id="-";

            lst.team_id = "-"; lst.shift_code = "-"; lst.shift_responsible = "-";

            lst.Start_date = "-";   lst.Start_wgt = "-";        lst.start_slag_wgt="-";
            lst.Stop_date = "-";
            lst.Final_wgt="-";      lst.final_slag_wgt="-";  
            lst.tapped_wgt="-";     lst.final_temp="-"; 

            lst.task_note="-";        
            lst.foreman_id="-";                 lst.scheduled_start_date="-";
            lst.hot_heel="-";                   lst.avgel1current="-";        lst.avgel2current="-";        lst.avgel3current="-";
            lst.avgactpower="-";                lst.tap_to_tap="-";        lst.heat_notes="-";        lst.l3_heat_id="-";
            lst.profile_model="-";              lst.eaf_electrode_consumption="-";        
            
            lst.Liquidus_temp="-";
      
            lst.seq_counter="-";                lst.seq_heat_counter="-";        lst.seq_total_heats="-";        lst.seq_sched_heats="-";

            lst.ladle_id = "-";         lst.ladle_life = "-";       lst.ladle_tare_wgt = "-"; lst.ladle_turret_arm_code="-";
            lst.Ladle_arrival_date = "-"; lst.Ladle_opening_date = "-"; lst.Ladle_close_date = "-"; lst.ladle_arrival_wgt = "-";
            lst.Ladle_opening_wgt="-";  lst.Ladle_close_wgt="-";        
            
            lst.tundish_id="-";        lst.tundish_life="-";
            lst.tundish_car_code="-";           lst.tundish_preheat_time="-";        lst.Tundish_preheat_temperature="-";
            lst.tundish_at_ladle_open_wgt="-";  lst.tundish_skull_wgt="-";        lst.tundish_powder_type="-";        lst.tundish_powder_wgt="-";
        }

        public void GetReportPdf_Slab(string SlabID, ref  iTextSharp.text.Document pdfDocument)
        {
            //新开一个页面
            pdfDocument.newPage();

            iTextSharp.text.Paragraph para = new iTextSharp.text.Paragraph(SlabID + "铸坯 数据追溯", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            FontHei.Size = 14;
            para = new iTextSharp.text.Paragraph("基本信息", FontHei);
            para.Alignment = iTextSharp.text.Element.ALIGN_CENTER;
            pdfDocument.Add(para);

            //以表形式来写入必须的信息
            iTextSharp.text.Table table = new iTextSharp.text.Table(10);
            table.WidthPercentage = 100; // 百分比
            table.Padding = 5;
            int[] headerwidths = { 10, 10, 10, 10, 10, 10, 10, 10, 10, 10 }; // 百分比
            table.setWidths(headerwidths);
            table.AutoFillEmptyCells = true;
            //设置边框颜色 
            table.BorderColor = new iTextSharp.text.Color(0, 0, 0);
            table.DefaultCellBorderColor = new iTextSharp.text.Color(0, 0, 255);
            iTextSharp.text.Color Color_DescCell = new iTextSharp.text.Color(System.Drawing.Color.LightGray);
            table.DefaultCellBackgroundColor = new iTextSharp.text.Color(System.Drawing.Color.White);
            table.DefaultVerticalAlignment = Element.ALIGN_MIDDLE;
            table.DefaultHorizontalAlignment = Element.ALIGN_CENTER;
            FontKai.Size = 9; FontSong.Size = 9;
            iTextSharp.text.Cell cell;

            //获取基本信息
            List<CC_SlabInfo> lst =GetCC_SlabInfo(SlabID);
            if (lst.Count > 0)
            {
                //这里开始写基本信息
                cell = new iTextSharp.text.Cell(new Paragraph("铸坯号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell); 
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Slab_no, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("炉号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Heat_ID, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("钢种", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Steel_grade, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("铸机号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].CCM, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("流号", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Strand_no, FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("PROD_COUNTER", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Prod_counter, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("PROD_NO", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Prod_no, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("坯头锥度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Taper_start, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("坯尾锥度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Taper_end, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("坯长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Length, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("坯宽", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Width, FontSong)); table.addCell(cell);

                //cell = new iTextSharp.text.Cell(new Paragraph("坯头宽度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                //cell = new iTextSharp.text.Cell(new Paragraph(lst[0].WIDTH_HEAD, FontSong)); table.addCell(cell);

                //cell = new iTextSharp.text.Cell(new Paragraph("坯尾宽度", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                //cell = new iTextSharp.text.Cell(new Paragraph(lst[0].WIDTH_TAIL, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("坯厚", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Thickness, FontSong)); table.addCell(cell);              

                cell = new iTextSharp.text.Cell(new Paragraph("坯重", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Weight, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(" ", FontSong)); table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("开始时间", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Start_time, FontSong)); cell.Colspan = 2; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("结束时间", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Stop_time, FontSong)); cell.Colspan = 2; table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("浇铸时长", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                TimeSpan ts = Convert.ToDateTime(lst[0].Stop_time) - Convert.ToDateTime(lst[0].Start_time);
                cell = new iTextSharp.text.Cell(new Paragraph(ts.TotalMinutes.ToString("#0.0") + "分钟", FontSong)); cell.Colspan = 3; table.addCell(cell);

                //新行
                cell = new iTextSharp.text.Cell(new Paragraph("浇铸长度位置", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Start_cast_pos +"->"+ lst[0].Stop_cast_pos, FontSong)); cell.Colspan = 2; table.addCell(cell);
                 
                //cell = new iTextSharp.text.Cell(new Paragraph("SAMPLE_WGT", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                //cell = new iTextSharp.text.Cell(new Paragraph(lst[0].SAMPLE_WGT, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("DEFECT_LEVEL", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Defect_level, FontSong)); table.addCell(cell);
                
                cell = new iTextSharp.text.Cell(new Paragraph("手动切割", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Manual_cut_flg, FontSong)); table.addCell(cell);

                cell = new iTextSharp.text.Cell(new Paragraph("切割时间", FontKai)); cell.BackgroundColor = Color_DescCell; table.addCell(cell);
                cell = new iTextSharp.text.Cell(new Paragraph(lst[0].Cut_date, FontSong)); cell.Colspan = 2; table.addCell(cell);

            }

            //把表写入文档
            pdfDocument.Add(table);

            //历史数据
            string CCM=lst[0].CCM;
            string StrandNo = lst[0].Strand_no;
            DateTime StatrTime=Convert.ToDateTime(lst[0].Start_time);
            DateTime EndTime =Convert.ToDateTime(lst[0].Stop_time);
            GetReportPdf_Slab_HisDB_Casting(SlabID, CCM, StrandNo, StatrTime, EndTime, ref pdfDocument);
            GetReportPdf_Slab_HisDB_Weight(SlabID, CCM, StrandNo, StatrTime, EndTime, ref pdfDocument);
            //结晶器关键参数
            GetReportPdf_Slab_HisDB_MD(SlabID, CCM, StrandNo, StatrTime, EndTime, ref pdfDocument);
            //结晶器冷却历程
            GetReportPdf_Slab_HisDB_MD_Cooling_WaterFlux(SlabID, CCM, StrandNo, StatrTime, EndTime, ref pdfDocument);
            //进出水温差
            GetReportPdf_Slab_HisDB_MD_Cooling_DiffTemp(SlabID, CCM, StrandNo, StatrTime, EndTime, ref pdfDocument);
            //二冷水量
            GetReportPdf_Slab_HisDB_SCS(SlabID, ref pdfDocument);

            //热轧加热炉
            SingleQtTableLY2250 LYQHRM = new SingleQtTableLY2250();
            LYQHRM.GetReportPdf_HF(SlabID,ref  pdfDocument);
        }

        public void GetReportPdf_Slab_HisDB_Casting(string SlabID, string CCM, string StrandNo, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        { 
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "拉速";
            stuDrawPicInfo.TagUnit[TagNo] = "m/min";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 2;
            stuDrawPicInfo.TagFormat[TagNo] = "#0.00"; 
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1.5F;
            
            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "铸流长度";
            stuDrawPicInfo.TagUnit[TagNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = true;
            
            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "过热度";
            stuDrawPicInfo.TagUnit[TagNo] = "℃";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 50;
                        

            //绘图的数据
            string[] tags = new string[3];
            tags[0] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSpeed";
            tags[1] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingLength";            
            tags[2] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSupperHeatValue";                    
            
            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "铸坯 浇铸过程参数", stuDrawPicInfo, ref pdfDocument);

        }

        public void GetReportPdf_Slab_HisDB_Weight(string SlabID, string CCM, string StrandNo, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //***** 浇铸过程中的重量数据 ****** //
             

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "拉速";
            stuDrawPicInfo.TagUnit[TagNo] = "m/min";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 2;
            stuDrawPicInfo.TagFormat[TagNo] = "#0.00";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1.5F;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "大包重量";
            stuDrawPicInfo.TagUnit[TagNo] = "ton";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = true;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 250;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "中包重量";
            stuDrawPicInfo.TagUnit[TagNo] = "℃";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 80;           

            //绘图的数据
            string[] tags = new string[3];
            tags[0] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSpeed";
            tags[1] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".LD_WEIGHT";
            tags[2] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".TD_WEIGHT";         

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "铸坯 浇铸过程参数", stuDrawPicInfo, ref pdfDocument);

        }

        public void GetReportPdf_Slab_HisDB_MD(string SlabID, string CCM, string StrandNo, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        { 
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "拉速";
            stuDrawPicInfo.TagUnit[TagNo] = "m/min";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 2;
            stuDrawPicInfo.TagFormat[TagNo] = "#0.00";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 1.5F;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "结晶器液位";
            stuDrawPicInfo.TagUnit[TagNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = true;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 300;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "结晶器液面振幅";
            stuDrawPicInfo.TagUnit[TagNo] = "mm";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false ;
            stuDrawPicInfo.TagMin[TagNo] = -10;
            stuDrawPicInfo.TagMax[TagNo] = 10;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "MES电流";
            stuDrawPicInfo.TagUnit[TagNo] = "A";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 100;

            TagNo = TagNo + 1;
            stuDrawPicInfo.TagDescription[TagNo] = "MES频率";
            stuDrawPicInfo.TagUnit[TagNo] = "Hz";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 400;

            //绘图的数据
            string[] tags = new string[5];
            tags[0] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSpeed";
            tags[1] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_LEVAL";
            tags[2] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_LEVAL_DEV";            
            tags[3] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MEMS_Current";
            tags[4] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MEMS_Frequency";

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "铸坯 浇铸过程参数", stuDrawPicInfo, ref pdfDocument);
        }

        public void GetReportPdf_Slab_HisDB_MD_Cooling_WaterFlux(string SlabID, string CCM, string StrandNo, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //***** 历史His数据 ****** //
            //新页面
            pdfDocument.newPage();
            pdfDocument.setPageSize(PageSize.A4);

            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = true;
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "冷却水水温";
            stuDrawPicInfo.TagUnit[TagNo] = "℃";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 1;
            stuDrawPicInfo.TagFormat[TagNo] = "#0.0";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 50;

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "结晶器冷却水量(固定面)";
            stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 0;
            stuDrawPicInfo.TagFormat[TagNo] = "#0";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = true;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 25000;

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "松开面";
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 25000;
             

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "右侧";
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 25000;
            

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "左侧";
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 25000;             
                       

            //绘图的数据
            string[] tags = new string[5];
            tags[00] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MDCW_INLET_TEMP";
            tags[01] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Fix_face_water_flow";//  结晶器冷却水流量(固定面)
            tags[02] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Loose_face_water_flow";//	结晶器冷却水流量(松开面)
            tags[03] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Right_face_water_flow";//	 结晶器冷却水流量(右侧)
            tags[04] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Left_face_water_flow";//	结晶器冷却水流量(左侧)

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "铸坯 浇铸过程参数", stuDrawPicInfo, ref pdfDocument);

        }
        public void GetReportPdf_Slab_HisDB_MD_Cooling_DiffTemp(string SlabID, string CCM, string StrandNo, DateTime StatrTime, DateTime EndTime, ref  iTextSharp.text.Document pdfDocument)
        {
            //定义
            ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
            //使用系统的初始化函数
            clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

            //更改其中某些配置
            stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
            stuDrawPicInfo.IsDrawEveryYAxis = false;
            stuDrawPicInfo.YAxisDisplayWhenDrawOnly1YAxis = "进出水水温差,℃"; 
            stuDrawPicInfo.IsDisplayRelativeTime = true;

            int TagNo = 1;
            stuDrawPicInfo.TagDescription[TagNo] = "固定面";
            stuDrawPicInfo.TagUnit[TagNo] = "℃";
            stuDrawPicInfo.TagDecimalPlaces[TagNo] = 1;
            stuDrawPicInfo.TagFormat[TagNo] = "#0.0";
            stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 20;
             

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "松开面";
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 20;
             

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "右侧";
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 20;
            

            TagNo++;
            stuDrawPicInfo.TagDescription[TagNo] = "左侧";
            stuDrawPicInfo.AutoSetRang[TagNo] = false;
            stuDrawPicInfo.TagMin[TagNo] = 0;
            stuDrawPicInfo.TagMax[TagNo] = 20;
            


            //绘图的数据
            string[] tags = new string[4];              
            tags[00] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Fix_face_water_delta_T";//	结晶器冷却水温差(固定面)
            tags[01] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Loose_face_water_delta_T";//	结晶器冷却水温差(松散面)
            tags[02] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Right_face_water_delta_T";//	结晶器冷却水温差(右侧)
            tags[03] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Left_face_water_delta_T";//	结晶器冷却水温差(左侧)

            stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            //输出图像
            sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "铸坯 浇铸过程参数", stuDrawPicInfo, ref pdfDocument);
        }

        /// <summary>
        /// 获取二冷段的各个冷却过程
        /// </summary>        
        public void GetReportPdf_Slab_HisDB_SCS(string SlabID, ref  iTextSharp.text.Document pdfDocument)
        {
            

            //保存的各个冷却段到浇铸液面的距离
            List<double> DisSCS_LY210_CCM1 = new List<double>{1057,2384,5754,7997 ,9280 
                                            ,12115 ,13666 ,15216 ,18137 ,19506 
                                            ,22451 ,24026 ,25600 ,28751 ,30326 
                                            ,36401 ,34451 ,35426 ,36401 };
             List<string> HisNameSCS_LY210_CCM1=new List<string>{ "SCWF01","SCWF02","SCWF03","SCWF04","SCWF05"
                                                                ,"SCWF06.0","SCWF06.1","SCWF06.2","SCWF07.0","SCWF07.1"
                                                                ,"SCWF07.2","SCWF08.0","SCWF08.1","SCWF08.2","SCWF09.0"
                                                                ,"SCWF09.1","SCWF09.2","SCWF10.0","SCWF10.1"};

            
            //计算方法：非精确计算
            //首先 从 HisDB中读取其拉速值，再用该冷却段到结晶器中钢液液位的距离，得出经过该冷却段的时间差，
            // 加上开始浇铸时间StatrTime、结束时间EndTime，即可得到该冷却段的时间范围
            ////获取拉速
            //string[] tags = new string[1];
            //tags[0] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSpeed";//	
            //DataTable dt= sqt.ReadDatatable_HisDB(tags, StatrTime, EndTime);
            ////计算平均拉速
            //double CastingSpeed_Sum = 0, CastingSpeed_Avg = 0;            
            //for (int I = 0; I < dt.Rows.Count; I++) CastingSpeed_Sum +=Convert.ToDouble(dt.Rows[I][0]);
            //CastingSpeed_Avg = CastingSpeed_Sum / dt.Rows.Count;

            ////在这个平均拉速下，到达各个段的时间差
            //double[] disSCS = new double [DisSCS_LY210_CCM1.Count];          
            //DateTime[] SCSStatrTime=new DateTime [DisSCS_LY210_CCM1.Count];
            //DateTime[] SCSEndTime=new DateTime [DisSCS_LY210_CCM1.Count];
            //DateTime start=Convert.ToDateTime(StatrTime);
            //DateTime end=Convert.ToDateTime(EndTime);
            //for (int I = disSCS.GetLowerBound(0); I <= disSCS.GetUpperBound(0); I++)
            //{
            //    disSCS[I] = DisSCS_LY210_CCM1[I] / CastingSpeed_Avg;//单位为分
            //    SCSStatrTime[I] = start.AddMinutes(disSCS[I]);
            //    SCSEndTime[I] = end.AddMinutes(disSCS[I]);
            //}
               
           
            //调试数据表明，通过拉速计算时间并不可靠
            //以下方法改用铸流长度计算，很费时间

            //先查找起始的铸流长度
            string strSQL = "SELECT slab_no,ccm,strand_no,start_time,stop_time,start_cast_pos,stop_cast_pos,cut_date FROM SLAB_L2_REPORTS WHERE slab_no='" + SlabID + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            string CCM = dt.Rows[0]["ccm"].ToString();
            string strand_no = dt.Rows[0]["strand_no"].ToString();
            DateTime start_time =Convert.ToDateTime( dt.Rows[0]["start_time"]);
            DateTime stop_time = Convert.ToDateTime(dt.Rows[0]["stop_time"]);
            double start_cast_pos = Convert.ToDouble(dt.Rows[0]["start_cast_pos"]);
            double stop_cast_pos = Convert.ToDouble(dt.Rows[0]["stop_cast_pos"]);
            DateTime cut_date = Convert.ToDateTime(dt.Rows[0]["cut_date"]);
            //铸流结束时间

            //反查铸流长度对应的时间
            DateTime LastSegmentStartTime=DateTime.Now ;
            //查最后一段，其余的按照比例来计算
            dt = sqt.SearchTimeFromCastingLength_HisDB(CCM, strand_no, start_cast_pos, start_time.ToString(), cut_date.ToString());
            if (dt.Rows.Count > 0) LastSegmentStartTime =Convert.ToDateTime(dt.Rows[0][0]);

            //从结晶器到最后二冷一段的时间间隔
            TimeSpan ts = LastSegmentStartTime - start_time;
            double  Inteval = ts.TotalSeconds;
            //每mm的距离上消耗的时间
            double SecondPerMM = Inteval / DisSCS_LY210_CCM1[DisSCS_LY210_CCM1.Count-1];

            //各段开始时间,结束时间,然后绘图
            DateTime[] SegmentStartTime = new DateTime[DisSCS_LY210_CCM1.Count];
            DateTime[] SegmentEndTime = new DateTime[DisSCS_LY210_CCM1.Count];
            for (int I=0;I <DisSCS_LY210_CCM1.Count; I++)
            {
                SegmentStartTime[I] = start_time.AddSeconds(SecondPerMM * DisSCS_LY210_CCM1[I]);
                SegmentEndTime[I] = stop_time.AddSeconds(SecondPerMM * DisSCS_LY210_CCM1[I]);  
          
                //定义
                ClsDuHisPic.StuDrawPicInfo stuDrawPicInfo = new ClsDuHisPic.StuDrawPicInfo();
                //使用系统的初始化函数
                clsDuHisPic.Initialize_StuDrawPicInfo(ref stuDrawPicInfo);

                //更改其中某些配置
                stuDrawPicInfo.PicTextFont = new System.Drawing.Font("System", 12);
                stuDrawPicInfo.IsDrawEveryYAxis = true;
                stuDrawPicInfo.IsDisplayRelativeTime = true;

                int TagNo = 1;
                stuDrawPicInfo.TagDescription[TagNo] = "二冷"+I.ToString()+"段冷却水流量";
                stuDrawPicInfo.TagUnit[TagNo] = "m3/h";
                stuDrawPicInfo.TagDecimalPlaces[TagNo] = 1;
                stuDrawPicInfo.TagFormat[TagNo] = "#0.0";
                stuDrawPicInfo.YAxisIsLeft[TagNo] = false;
                stuDrawPicInfo.AutoSetRang[TagNo] = true;
                stuDrawPicInfo.TagMin[TagNo] = 0;
                stuDrawPicInfo.TagMax[TagNo] = 20;
 

                //绘图的数据
                string[] tags = new string[1];
                tags[00] = "LYQ210.CCM" + CCM + "S" + strand_no + "." + HisNameSCS_LY210_CCM1[I];                
                stuDrawPicInfo.dt = sqt.ReadDatatable_HisDB(tags, SegmentStartTime[I], SegmentEndTime[I]);
                //输出图像
                sqt.DrawstuDrawPicInfo2pdfDocument(SlabID + "铸坯 浇铸过程参数", stuDrawPicInfo, ref pdfDocument);

            }
        }

        public List<CC_MDCoolHisDB> GetCC_MDCoolHisDB( string CCM, string StrandNo, string StartDateTime, string EndDateTime)
        { 
            //获取结晶器的冷却历程数据
            List<CC_MDCoolHisDB> LST = new List<CC_MDCoolHisDB>();
            CC_MDCoolHisDB lst = new CC_MDCoolHisDB();
            

            object obj = new object();
            string[] tags = new string[13];

            tags[00] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MDCW_INLET_TEMP";

            tags[01] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Fix_face_water_flow";//  结晶器冷却水流量(固定面)
            tags[02] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Loose_face_water_flow";//	结晶器冷却水流量(松开面)
            tags[03] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Right_face_water_flow";//	 结晶器冷却水流量(右侧)
            tags[04] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Left_face_water_flow";//	结晶器冷却水流量(左侧)

            tags[05] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Fix_face_water_delta_T";//	结晶器冷却水温差(固定面)
            tags[06] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Loose_face_water_delta_T";//	结晶器冷却水温差(松散面)
            tags[07] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Right_face_water_delta_T";//	结晶器冷却水温差(右侧)
            tags[08] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_Left_face_water_delta_T";//	结晶器冷却水温差(左侧)

            tags[09] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_LEFT_FACE_EXTRACT";//	结晶器冷却：固定面热流密度
            tags[10] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_LOSE_FACE_EXTRACT";//	结晶器冷却：松散面热流密度
            tags[11] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_RIGHT_FACE_EXTRACT";//结晶器冷却：右侧面热流密度
            tags[12] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_FIX_FACE_EXTRACT";  //结晶器冷却：左侧面热流密度 
        
            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new CC_MDCoolHisDB();

                lst.DateAndTime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.DateAndTime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列

                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.CoolWaterInTempure = Convert.ToSingle(obj);

                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterFlux_Fix = Convert.ToSingle(obj);// MD_Fix_face_water_flow 结晶器冷却水流量(固定面)
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterFlux_Loose = Convert.ToSingle(obj);//MD_Loose_face_water_flow;//	结晶器冷却水流量(松开面)
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterFlux_Right = Convert.ToSingle(obj);//	MD_Right_face_water_flow 结晶器冷却水流量(右侧)
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterFlux_Left = Convert.ToSingle(obj);//	MD_Left_face_water_flow结晶器冷却水流量(左侧)

                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterDiffTemp_Fix = Convert.ToSingle(obj);//  MD_Fix_face_water_delta_T;//	结晶器冷却水温差(固定面)
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterDiffTemp_Loose = Convert.ToSingle(obj);//MD_Loose_face_water_delta_T;//	结晶器冷却水温差(松散面)
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterDiffTemp_Right = Convert.ToSingle(obj);// MD_Right_face_water_delta_T;//	结晶器冷却水温差(右侧)
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CoolWaterDiffTemp_Left = Convert.ToSingle(obj);// MD_Left_face_water_delta_T;//	结晶器冷却水温差(左侧)

                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.HeatFlux_Fix = Convert.ToSingle(obj);//MD_LEFT_FACE_EXTRACT;//	结晶器冷却：固定面热流密度
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.HeatFlux_Loose = Convert.ToSingle(obj);//MD_LOSE_FACE_EXTRACT;//	结晶器冷却：松散面热流密度
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.HeatFlux_Right = Convert.ToSingle(obj);//MD_RIGHT_FACE_EXTRACT	;//结晶器冷却：右侧面热流密度
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.HeatFlux_Left = Convert.ToSingle(obj);//MD_FIX_FACE_EXTRACT;//结晶器冷却：左侧面热流密度        
                
                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }
        public HEAT_TRACK GetHeatTrack(string HeatID)
        {//获取指定炉号列表信息 
            List<HEAT_TRACK> LST = new List<HEAT_TRACK>();
            HEAT_TRACK lst = new HEAT_TRACK();
            HEAT_TRACK_Ini(ref lst);

            string strSQL = "SELECT t.*,s.steel_grade FROM sm_mattrack_time t, SM_bof_heat s"
                        +" WHERE t.mat_no='" + HeatID + "'"
                        + " AND t.mat_no=s.heat_id"
                        + " AND rownum <=999";
             

            //获取炉号列表
            DataTable dt =sqt.ReadDatatable_OraDB(strSQL);
            
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                DataRow dr = dt.Rows[RowIndex];

                //调用子代码
                GetHEAT_TRACK_ByDataRow(dr, ref  lst, ref  LST);       

            }

            
            dt.Dispose();

            return lst;
        }

        private void HEAT_TRACK_Ini(ref HEAT_TRACK lst)
        {
            lst.HeatID = "-";
            lst.LF_Station = "-"; lst.LF_Arrive_Time = "-"; lst.MI_Leave_Time = "-"; lst.MI_Duration = "-";
            lst.KR_Station = "-"; lst.KR_Arrive_Time = "-"; lst.KR_Leave_Time = "-"; lst.KR_Duration = "-";
            lst.BOF_Station = "-"; lst.BOF_Arrive_Time = "-"; lst.BOF_Leave_Time = "-"; lst.BOF_Duration = "-";
            lst.LF_Station = "-"; lst.LF_Arrive_Time = "-"; lst.LF_Leave_Time = "-"; lst.LF_Duration = "-";
            lst.RH_Station = "-"; lst.RH_Arrive_Time = "-"; lst.RH_Leave_Time = "-"; lst.RH_Duration = "-";
            lst.CC_Station = "-"; lst.CC_Arrive_Time = "-"; lst.CC_Leave_Time = "-"; lst.CC_Duration = "-";
        }
        

        //获取铸坯的基本数据
        public List<CC_SlabInfo> GetCC_SlabInfo(string SlabNo)
        {
            List<CC_SlabInfo> LST = new List<CC_SlabInfo>();
            CC_SlabInfo lst = new CC_SlabInfo(); CC_SlabInfo_Ini(ref lst);
            Object Obj = new object();
            string str = "";

            string strSQL = "SELECT * FROM  sm_slab_info WHERE slab_no='" + SlabNo + "'";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new CC_SlabInfo(); CC_SlabInfo_Ini(ref lst);

                lst.Slab_no = dt.Rows[RowIndex]["slab_no"].ToString();
                lst.Heat_ID = dt.Rows[RowIndex]["HEAT_ID"].ToString();
                lst.Steel_grade = dt.Rows[RowIndex]["STEEL_GRADE"].ToString();

                lst.CCM = dt.Rows[RowIndex]["CCM"].ToString();
                lst.Strand_no = dt.Rows[RowIndex]["STRAND_NO"].ToString();
                lst.Prod_no = dt.Rows[RowIndex]["PROD_NO"].ToString();
                lst.Prod_counter = dt.Rows[RowIndex]["PROD_COUNTER"].ToString();
                lst.Taper_start = dt.Rows[RowIndex]["TAPER_START"].ToString();
                lst.Taper_end = dt.Rows[RowIndex]["TAPER_END"].ToString();

                str = dt.Rows[RowIndex]["WIDTH"].ToString(); lst.Width = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["WIDTH_HEAD"].ToString(); lst.Width_head = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["WIDTH_TAIL"].ToString(); lst.Width_tail = str.Split(new char[] { '.' })[0];

                str = dt.Rows[RowIndex]["THICKNESS"].ToString(); lst.Thickness = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["LENGTH"].ToString(); lst.Length = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["WEIGHT"].ToString(); lst.Weight = str.Split(new char[] { '.' })[0];

                //lst.Start_time = dt.Rows[RowIndex]["START_TIME"].ToString();
                //lst.Stop_time = dt.Rows[RowIndex]["STOP_TIME"].ToString();

                str = dt.Rows[RowIndex]["START_CAST_POS"].ToString(); lst.Start_cast_pos = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["STOP_CAST_POS"].ToString(); lst.Stop_cast_pos = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["SAMPLE_WGT"].ToString(); lst.Sample_wgt = str.Split(new char[] { '.' })[0];
                str = dt.Rows[RowIndex]["DEFECT_LEVEL"].ToString(); lst.Defect_level = str.Split(new char[] { '.' })[0];

                lst.Manual_report_flg = dt.Rows[RowIndex]["MANUAL_REPORT_FLG"].ToString();
                lst.Manual_cut_flg = dt.Rows[RowIndex]["MANUAL_CUT_FLG"].ToString();
                lst.Cut_date = dt.Rows[RowIndex]["CUT_DATE"].ToString();
                lst.Weight_real = dt.Rows[RowIndex]["WEIGHT_REAL"].ToString();

                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }

        public void CC_SlabInfo_Ini(ref CC_SlabInfo lst)
        {
            lst.Slab_no = "-"; lst.Heat_ID = "-"; lst.Steel_grade = "-"; lst.CCM = "-";
            lst.Strand_no = "-"; lst.Prod_counter = "-"; lst.Prod_no = "-"; lst.Width = "-";
            lst.Width_head = "-"; lst.Width_tail = "-"; lst.Thickness = "-"; lst.Taper_start = "-";
            lst.Taper_end = "-"; lst.Length = "-"; lst.Weight = "-"; lst.Start_time = "-";
            lst.Stop_time = "-"; lst.Start_cast_pos = "-"; lst.Stop_cast_pos = "-"; lst.Sample_wgt = "-";
            lst.Defect_level = "-"; lst.Manual_report_flg = "-"; lst.Manual_cut_flg = "-"; lst.Cut_date = "-";
            lst.Weight_real = "-";
        }

        public List<CC_HisData0> GetCC_HisData0_HisDB(string CCM, string StrandNo, string StartDateTime, string EndDateTime)
        {
            List<CC_HisData0> LST = new List<CC_HisData0>();

            object obj = new object();

            string[] tags = new string[12];

            tags[00] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingLength";
            tags[01] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSpeed";
            tags[02] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingSupperHeatValue";
            tags[03] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".CastingTempture";
            tags[04] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".LD_WEIGHT";
            tags[05] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_LEVAL";
            tags[06] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_LEVAL_DEV";
            tags[07] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MD_SEN_Immersion";
            tags[08] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MEMS_Current";
            tags[09] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".MEMS_Frequency";
            tags[10] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".NOZZLE_AR_FLUX";
            tags[11] = "LYQ210.CCM" + CCM + "S" + StrandNo + ".TD_WEIGHT";


            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                CC_HisData0 lst = new CC_HisData0();

                lst.datetime = Convert.ToDateTime(dt.Rows[I][00]);
                TimeSpan ts = Convert.ToDateTime(lst.datetime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                //dt.Rows[I][00]是时间列

                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.CastingLength = Convert.ToSingle(obj);
                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.CastingSpeed = Convert.ToSingle(obj);
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.CastingSupperHeatValue = Convert.ToSingle(obj);
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.CastingTempture = Convert.ToSingle(obj);
                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.LD_WEIGHT = Convert.ToSingle(obj);
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.MD_LEVAL = Convert.ToSingle(obj);
                obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.MD_LEVAL_DEV = Convert.ToSingle(obj);
                obj = dt.Rows[I][08]; if (obj.ToString().Length > 0) lst.MD_SEN_Immersion = Convert.ToSingle(obj);
                obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.MEMS_Current = Convert.ToSingle(obj);
                obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.MEMS_Frequency = Convert.ToSingle(obj);
                obj = dt.Rows[I][11]; if (obj.ToString().Length > 0) lst.NOZZLE_AR_FLUX = Convert.ToSingle(obj);
                obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.TD_WEIGHT = Convert.ToSingle(obj);

                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }
        public List<BOF_HisDB> GetBOF_WasteGas_HisDB(string bof_campaign, string StartDateTime, string EndDateTime)
        {
            List<BOF_HisDB> LST = new List<BOF_HisDB>();

            string[] tags = new string[9];
            tags[0] = "LYQ210.BOF" + bof_campaign + ".ContentO2";
            tags[1] = "LYQ210.BOF" + bof_campaign + ".ContentCO";
            tags[2] = "LYQ210.BOF" + bof_campaign + ".ContentCO2";

            tags[3] = "LYQ210.BOF" + bof_campaign + ".ACT_INCLINE_ANGLE";
            tags[4] = "LYQ210.BOF" + bof_campaign + ".ACT_LANCE_HEIGHT";
            tags[5] = "LYQ210.BOF" + bof_campaign + ".ACT_O2_FLUX";

            tags[6] = "LYQ210.BOF" + bof_campaign + ".ACT_N2_FLUX";
            tags[7] = "LYQ210.BOF" + bof_campaign + ".ACT_AR_FLUX";
            tags[8] = "LYQ210.BOF" + bof_campaign + ".ACT_BATH_LEVEL";

            //tags[] = "LYQ210.BOF" + bof_campaign + ".";

            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));

            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                BOF_HisDB lst = new BOF_HisDB();

                TimeSpan ts = Convert.ToDateTime(dt.Rows[I][0]) - TimeStart;
                lst.datetime = Convert.ToDateTime(dt.Rows[I][0]);
                lst.Duration = (float)ts.TotalSeconds / 60;
                lst.O2 = Convert.ToSingle(dt.Rows[I][1]);
                lst.CO = Convert.ToSingle(dt.Rows[I][2]);
                lst.CO2 = Convert.ToSingle(dt.Rows[I][3]);

                lst.ACT_INCLINE_ANGLE = Convert.ToSingle(dt.Rows[I][4]);
                lst.ACT_LANCE_HEIGHT = Convert.ToSingle(dt.Rows[I][5]);
                lst.ACT_O2_FLUX = Convert.ToSingle(dt.Rows[I][6]);

                lst.ACT_N2_FLUX = Convert.ToSingle(dt.Rows[I][7]);
                lst.ACT_AR_FLUX = Convert.ToSingle(dt.Rows[I][8]);
                lst.ACT_BATH_LEVEL = Convert.ToSingle(dt.Rows[I][9]);

                LST.Add(lst);
            }
            return LST;
        }


        public List<LF_HisDB> GetLF_HisDB(string LF_Station, string TrolleyNo, string StartDateTime, string EndDateTime)
        {
            List<LF_HisDB> LST = new List<LF_HisDB>();
            LF_HisDB lst = new LF_HisDB();
            object obj = new object();

            string[] tags = new string[13];

            tags[00] = "LYQ210.LF" + LF_Station + ".SetTapNo";

            tags[01] = "LYQ210.LF" + LF_Station + ".EleTran_PriSide_Voltage_A";
            tags[02] = "LYQ210.LF" + LF_Station + ".EleTran_PriSide_Curr_A";
            tags[03] = "LYQ210.LF" + LF_Station + ".EleTran_SecSide_Voltage_A";

            tags[04] = "LYQ210.LF" + LF_Station + ".EleTran_SecSide_Curr_A";
            tags[05] = "LYQ210.LF" + LF_Station + ".EleTran_SecSide_Curr_B";
            tags[06] = "LYQ210.LF" + LF_Station + ".EleTran_SecSide_Curr_C";

            tags[07] = "LYQ210.LF" + LF_Station + ".Trolley" + TrolleyNo + "_BotGasType";
            tags[08] = "LYQ210.LF" + LF_Station + ".Trolley" + TrolleyNo + "_BotGasPrss";
            tags[09] = "LYQ210.LF" + LF_Station + ".Trolley" + TrolleyNo + "_BotGasFlux";

            tags[10] = "LYQ210.LF" + LF_Station + ".Trolley" + TrolleyNo + "_TopGasType";
            tags[11] = "LYQ210.LF" + LF_Station + ".Trolley" + TrolleyNo + "_TopGasPrss";
            tags[12] = "LYQ210.LF" + LF_Station + ".Trolley" + TrolleyNo + "_TopGasFlux";


            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));

            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                lst = new LF_HisDB();

                TimeSpan ts = Convert.ToDateTime(dt.Rows[I][0]) - TimeStart;
                lst.datetime = Convert.ToDateTime(dt.Rows[I][0]);
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                obj = dt.Rows[I][01]; if (obj.ToString().Length > 0) lst.SetTapNo = Convert.ToInt32(obj);

                obj = dt.Rows[I][02]; if (obj.ToString().Length > 0) lst.PriSideVoltageA = Convert.ToInt32(obj);
                obj = dt.Rows[I][03]; if (obj.ToString().Length > 0) lst.PriSideCurrentB = Convert.ToInt32(obj);
                obj = dt.Rows[I][04]; if (obj.ToString().Length > 0) lst.SecSideVlotageA = Convert.ToInt32(obj);

                obj = dt.Rows[I][05]; if (obj.ToString().Length > 0) lst.SecSideCurrentA = Convert.ToInt32(obj);
                obj = dt.Rows[I][06]; if (obj.ToString().Length > 0) lst.SecSideCurrentB = Convert.ToInt32(obj);
                obj = dt.Rows[I][07]; if (obj.ToString().Length > 0) lst.SecSideCurrentC = Convert.ToInt32(obj);

                obj = dt.Rows[I][09]; if (obj.ToString().Length > 0) lst.BotGasPrss = Convert.ToSingle(obj);
                obj = dt.Rows[I][10]; if (obj.ToString().Length > 0) lst.BotGasFlux = Convert.ToSingle(obj);

                obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.TopGasPrss = Convert.ToSingle(obj);
                obj = dt.Rows[I][13]; if (obj.ToString().Length > 0) lst.TopGasFlux = Convert.ToSingle(obj);

                LST.Add(lst);
            }
            return LST;
        }

        public List<RH_HisDB> GetRH_HisDB(string RH_Station, string StartDateTime, string EndDateTime)
        {
            List<RH_HisDB> LST = new List<RH_HisDB>();

            object obj = new object();

            string[] tags = new string[13];

            tags[0] = "LYQ210.RH" + RH_Station + ".VacuumValue";
            tags[1] = "LYQ210.RH" + RH_Station + ".CycArFlowLift";
            tags[2] = "LYQ210.RH" + RH_Station + ".CycBlowingO2Flow";

            tags[3] = "LYQ210.RH" + RH_Station + ".FluxAr";
            tags[4] = "LYQ210.RH" + RH_Station + ".FluxN2";
            tags[5] = "LYQ210.RH" + RH_Station + ".FluxO2";

            tags[6] = "LYQ210.RH" + RH_Station + ".FlueGasAr";
            tags[7] = "LYQ210.RH" + RH_Station + ".FlueGasCO";
            tags[8] = "LYQ210.RH" + RH_Station + ".FlueGasCO2";

            tags[9] = "LYQ210.RH" + RH_Station + ".FlueGasFlux";
            tags[10] = "LYQ210.RH" + RH_Station + ".FlueGasH2";

            tags[11] = "LYQ210.RH" + RH_Station + ".ProcVolPlug1Stir";
            tags[12] = "LYQ210.RH" + RH_Station + ".ProcVolPlug2Stir";

            //获取历史数据
            System.Data.DataTable dt = sqt.ReadDatatable_HisDB(tags, Convert.ToDateTime(StartDateTime), Convert.ToDateTime(EndDateTime));
            //起始时间
            DateTime TimeStart = Convert.ToDateTime(StartDateTime);

            for (int I = 0; I < dt.Rows.Count; I++)
            {
                RH_HisDB lst = new RH_HisDB();

                lst.datetime = Convert.ToDateTime(dt.Rows[I][0]);
                TimeSpan ts = Convert.ToDateTime(lst.datetime) - TimeStart;
                lst.Duration = (float)ts.TotalMilliseconds / 60000;

                obj = dt.Rows[I][1]; if (null != obj) lst.VacuumValue = Convert.ToSingle(obj);
                obj = dt.Rows[I][2]; if (null != obj) lst.CycArFlowLift = Convert.ToSingle(obj);
                obj = dt.Rows[I][3]; if (obj.ToString().Length > 0) lst.CycBlowingO2Flow = Convert.ToSingle(obj);

                obj = dt.Rows[I][4]; if (null != obj) lst.FluxAr = Convert.ToSingle(obj);
                obj = dt.Rows[I][5]; if (null != obj) lst.FluxN2 = Convert.ToSingle(obj);
                obj = dt.Rows[I][6]; if (obj.ToString().Length > 0) lst.FluxO2 = Convert.ToSingle(obj);

                obj = dt.Rows[I][7]; if (null != obj) lst.FlueGasAr = Convert.ToSingle(obj);
                obj = dt.Rows[I][8]; if (null != obj) lst.FlueGasCO = Convert.ToSingle(obj);
                obj = dt.Rows[I][9]; if (null != obj) lst.FlueGasCO2 = Convert.ToSingle(obj);

                obj = dt.Rows[I][10]; if (null != obj) lst.FlueGasFlux = Convert.ToSingle(obj);
                obj = dt.Rows[I][11]; if (null != obj) lst.FlueGasH2 = Convert.ToSingle(obj);
                obj = dt.Rows[I][12]; if (obj.ToString().Length > 0) lst.ProcVolPlug1Stir = Convert.ToSingle(obj);

                obj = dt.Rows[I][13]; if (obj.ToString().Length > 0) lst.ProcVolPlug2Stir = Convert.ToSingle(obj);

                LST.Add(lst);
            }
            dt.Dispose();

            return LST;
        }

        public List<String> GetSteelGradeList()
        {//从bof_heat中获取钢种列表信息

            List<String> list = new List<string>();

            string strSQL = "SELECT DISTINCT steel_grade FROM SM_bof_heat WHERE steel_grade IS NOT NULL ORDER BY steel_grade";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                string str = dt.Rows[RowIndex][0].ToString().Trim();
                if (str.Length > 0) list.Add(str);
            }
            return list;
        }

        public List<HEAT_TRACK> GetHeatIDList(string schStartTime, string schEndTime, string SteelGrade, int WorkerTeam)
        {//从bof_heat表中获取炉号列表信息

            List<HEAT_TRACK> LST = new List<HEAT_TRACK>();


            //先从bof_heat中获取满足筛选条件的炉号
            string strSQL = "SELECT DISTINCT heat_id,steel_grade FROM SM_bof_heat WHERE heat_id IS NOT NULL";

            if (schStartTime.Length > 5) strSQL = strSQL + " AND ready_time >= to_date('" + schStartTime + " 00:00:01', 'yyyy-mm-dd hh24:mi:ss')"
                                                         + " AND ready_time <= to_date('" + schEndTime + " 23:59:59', 'yyyy-mm-dd hh24:mi:ss')";

            if (SteelGrade.Length > 2) strSQL = strSQL + " AND steel_grade='" + SteelGrade + "'";

            if (WorkerTeam > 0) strSQL = strSQL + " AND crew_id=" + WorkerTeam.ToString();

            strSQL = strSQL + "  ORDER BY heat_id";

            //然后联合查询,特别注意：ORDER BY t.mat_no必须有！
            strSQL = "SELECT t.*,s.steel_grade FROM SM_MATTRACK_TIME t,(" + strSQL + ") s "
                    + " WHERE t.mat_no=s.heat_id ORDER BY t.mat_no";

            //获取炉号列表
            DataTable dt =sqt.ReadDatatable_OraDB(strSQL);

            //生成新的
            HEAT_TRACK lst = new HEAT_TRACK(); HEAT_TRACK_Ini(ref lst);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                DataRow dr = dt.Rows[RowIndex];

                //调用子代码
                GetHEAT_TRACK_ByDataRow(dr, ref  lst, ref   LST);                
            }

            dt.Dispose();

            //按照进入转炉的时间排序
            LST.Sort(delegate(HEAT_TRACK a, HEAT_TRACK b) { return a.BOF_Arrive_Time.CompareTo(b.BOF_Arrive_Time); });

             return LST;
        }
        
        private void GetHEAT_TRACK_ByDataRow(DataRow dr, ref HEAT_TRACK lst, ref List<HEAT_TRACK> LST)
        {
            string cHeatID = "", DEVICE_NO = "";
            TimeSpan ts = new TimeSpan();

            DateTime Arrive_Time = DateTime.Now, Leave_Time = DateTime.Now;

            cHeatID = dr["mat_no"].ToString();

            if (lst.HeatID != cHeatID)
            {
                if ("-" != lst.HeatID)
                {
                    LST.Add(lst);
                    lst = new HEAT_TRACK(); HEAT_TRACK_Ini(ref lst);
                    HEAT_TRACK_Ini(ref lst);
                }
                lst.HeatID = cHeatID;
                lst.SteelGrade = dr["steel_grade"].ToString();
            }

             DEVICE_NO = dr["DEVICE_NO"].ToString();

            //判断工序,前5个为"LY210_"
            DEVICE_NO = DEVICE_NO.Replace("LY210_", "");
            switch (DEVICE_NO.Substring(0, 2))
            {
                case "MI":
                    lst.MI_Station = "1#";
                    lst.MI_Arrive_Time = dr["start_time"].ToString();
                    lst.MI_Leave_Time = dr["stop_time"].ToString();
                    if (DateTime.TryParse(dr["start_time"].ToString(), out Arrive_Time) && DateTime.TryParse(dr["stop_time"].ToString(), out Leave_Time))
                    {
                        ts = Leave_Time - Arrive_Time;
                        lst.MI_Duration = ts.TotalMinutes.ToString("#0.0");
                    }
                    break;

                case "KR":
                    lst.KR_Station = DEVICE_NO.Substring(DEVICE_NO.Length - 1) + "#";
                    lst.KR_Arrive_Time = dr["start_time"].ToString();
                    lst.KR_Leave_Time = dr["stop_time"].ToString();
                    if (DateTime.TryParse(dr["start_time"].ToString(), out Arrive_Time) && DateTime.TryParse(dr["stop_time"].ToString(), out Leave_Time))
                    {
                        ts = Leave_Time - Arrive_Time;
                        lst.KR_Duration = ts.TotalMinutes.ToString("#0.0");
                    }
                    break;

                case "BO":
                    lst.BOF_Station = DEVICE_NO.Substring(DEVICE_NO.Length - 1) + "#";
                    lst.BOF_Arrive_Time = dr["start_time"].ToString();
                    lst.BOF_Leave_Time = dr["stop_time"].ToString();
                    if (DateTime.TryParse(dr["start_time"].ToString(), out Arrive_Time) && DateTime.TryParse(dr["stop_time"].ToString(), out Leave_Time))
                    {
                        ts = Leave_Time - Arrive_Time;
                        lst.BOF_Duration = ts.TotalMinutes.ToString("#0.0");
                    }
                    break;

                case "LF":
                    lst.LF_Station = DEVICE_NO.Substring(DEVICE_NO.Length - 1) + "#";
                    lst.LF_Arrive_Time = dr["start_time"].ToString();
                    lst.LF_Leave_Time = dr["stop_time"].ToString();
                    if (DateTime.TryParse(dr["start_time"].ToString(), out Arrive_Time) && DateTime.TryParse(dr["stop_time"].ToString(), out Leave_Time))
                    {
                        ts = Leave_Time - Arrive_Time;
                        lst.LF_Duration = ts.TotalMinutes.ToString("#0.0");
                    }
                    break;

                case "RH":
                    lst.RH_Station = DEVICE_NO.Substring(DEVICE_NO.Length - 1) + "#";
                    lst.RH_Arrive_Time = dr["start_time"].ToString();
                    lst.RH_Leave_Time = dr["stop_time"].ToString();
                    if (DateTime.TryParse(dr["start_time"].ToString(), out Arrive_Time) && DateTime.TryParse(dr["stop_time"].ToString(), out Leave_Time))
                    {
                        ts = Leave_Time - Arrive_Time;
                        lst.RH_Duration = ts.TotalMinutes.ToString("#0.0");
                    }
                    break;

                case "CC":
                    lst.CC_Station = DEVICE_NO.Substring(DEVICE_NO.Length - 1) + "#";
                    lst.CC_Arrive_Time = dr["start_time"].ToString();
                    lst.CC_Leave_Time = dr["stop_time"].ToString();
                    if (DateTime.TryParse(dr["start_time"].ToString(), out Arrive_Time) && DateTime.TryParse(dr["stop_time"].ToString(), out Leave_Time))
                    {
                        ts = Leave_Time - Arrive_Time;
                        lst.CC_Duration = ts.TotalMinutes.ToString("#0.0");
                    }
                    break;
            }
        }
 
        public List<Addition> GetAddition(string HeatID)
        {
            List<Addition> LST = new List<Addition>();
            Addition lst=new Addition();
            string str = "";

            string strSQL = "SELECT * FROM sm_addition"
                            + " where HEAT_ID ='" + HeatID + "'"
                            + " Order by ADD_TIME";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new Addition();
                Additon_Ini(ref lst);

                str = dt.Rows[RowIndex]["DEVICE_NO"].ToString(); if (str.Length > 0) lst.DEVICE_NO = str;
                str = dt.Rows[RowIndex]["STATION"].ToString(); if (str.Length > 0) lst.STATION = str;
                str = dt.Rows[RowIndex]["HEAT_ID"].ToString(); if (str.Length > 0) lst.HEAT_ID = str;
                str = dt.Rows[RowIndex]["ADD_TIME"].ToString(); if (str.Length > 0) lst.ADD_TIME = str;
                str = dt.Rows[RowIndex]["ADD_BATCH"].ToString(); if (str.Length > 0) lst.ADD_BATCH = str;

                str = dt.Rows[RowIndex]["MAT_ID"].ToString(); if (str.Length > 0) lst.MAT_ID = str;
                str = dt.Rows[RowIndex]["MAT_NAME"].ToString(); if (str.Length > 0) lst.MAT_NAME = str;
                str = dt.Rows[RowIndex]["WEIGHT"].ToString(); if (str.Length > 0) lst.WEIGHT = str;
                str = dt.Rows[RowIndex]["HOPPER_ID"].ToString(); if (str.Length > 0) lst.HOPPER_ID = str;
                str = dt.Rows[RowIndex]["PLACE"].ToString(); if (str.Length > 0) lst.PLACE = str;

                LST.Add(lst);
            }

            return LST;

        }
        private void Additon_Ini(ref Addition lst)
        {
            lst.DEVICE_NO=" ";        lst.STATION=" ";        lst.HEAT_ID=" ";        lst.ADD_TIME=" ";
            lst.ADD_BATCH=" ";        lst.MAT_ID=" ";         lst.MAT_NAME=" ";       lst.WEIGHT=" ";
            lst.HOPPER_ID=" ";        lst.PLACE=" ";            
        }

        public List<TEMPTURE> GetTEMPTURE(string HeatID)
        {
             List<TEMPTURE> LST = new List<TEMPTURE>();
            TEMPTURE lst=new TEMPTURE();
            string str = "";

            string strSQL = "SELECT * FROM sm_TEMPTURE"
                            + " where HEAT_ID ='" + HeatID + "'"
                            + " Order by MEASURE_TIME";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new TEMPTURE();
                TEMPTURE_Ini(ref lst);

                str = dt.Rows[RowIndex]["DEVICE_NO"].ToString(); if (str.Length > 0) lst.DEVICE_NO = str;
                str = dt.Rows[RowIndex]["STATION"].ToString(); if (str.Length > 0) lst.STATION = str;
                str = dt.Rows[RowIndex]["HEAT_ID"].ToString(); if (str.Length > 0) lst.HEAT_ID = str;
                str = dt.Rows[RowIndex]["MEASURE_TYPE"].ToString(); if (str.Length > 0) lst.MEASURE_TYPE = str;
                str = dt.Rows[RowIndex]["MEASURE_TIME"].ToString(); if (str.Length > 0) lst.MEASURE_TIME = str;
                str = dt.Rows[RowIndex]["MEASURE_NUM"].ToString(); if (str.Length > 0) lst.MEASURE_NUM = str;
                str = dt.Rows[RowIndex]["TRMPTURE_VALUE"].ToString(); if (str.Length > 0) lst.TRMPTURE_VALUE = str;

                LST.Add(lst);
            }

            return LST;
        }
        private void TEMPTURE_Ini(ref TEMPTURE lst)
        { 
            lst.DEVICE_NO=" ";        lst.STATION=" ";        lst.HEAT_ID=" ";        lst.MEASURE_TYPE=" ";
            lst.MEASURE_TIME=" ";        lst.MEASURE_NUM=" ";        lst.TRMPTURE_VALUE=" ";        
        }

        public List<ELEM_ANA> GetELEM_ANA(string HeatID)
        {
            List<ELEM_ANA> LST = new List<ELEM_ANA>();
            ELEM_ANA lst = new ELEM_ANA();
            string str = "";

            string strSQL = "SELECT * FROM sm_ELEM_ANA"
                            + " where HEAT_ID ='" + HeatID + "'"
                            + " Order by SAMPLETIME";
            DataTable dt = sqt.ReadDatatable_OraDB(strSQL);
            for (int RowIndex = 0; RowIndex < dt.Rows.Count; RowIndex++)
            {
                lst = new ELEM_ANA();
                ELEM_ANA_Ini(ref lst);

                str = dt.Rows[RowIndex]["DEVICE_NO"].ToString(); if (str.Length > 0) lst.DEVICE_NO = str;
                str = dt.Rows[RowIndex]["STATION"].ToString(); if (str.Length > 0) lst.STATION = str;
                str = dt.Rows[RowIndex]["HEAT_ID"].ToString(); if (str.Length > 0) lst.HEAT_ID = str;
                str = dt.Rows[RowIndex]["IRON_ID"].ToString(); if (str.Length > 0) lst.IRON_ID = str;
                str = dt.Rows[RowIndex]["SAMPLETIME"].ToString(); if (str.Length > 0) lst.SAMPLETIME = str;
                str = dt.Rows[RowIndex]["SAMPLE_NUMBER"].ToString(); if (str.Length > 0) lst.SAMPLE_NUMBER = str;
                str = dt.Rows[RowIndex]["SAMPLE_ID"].ToString(); if (str.Length > 0) lst.SAMPLE_ID = str;

                str = dt.Rows[RowIndex]["ELE_ALS"].ToString(); if (str.Length > 0) lst.ELE_ALS = str;
                str = dt.Rows[RowIndex]["ELE_ALT"].ToString(); if (str.Length > 0) lst.ELE_ALT = str;
                str = dt.Rows[RowIndex]["ELE_AS"].ToString(); if (str.Length > 0) lst.ELE_AS = str;
                str = dt.Rows[RowIndex]["ELE_B"].ToString(); if (str.Length > 0) lst.ELE_B = str;
                str = dt.Rows[RowIndex]["ELE_BI"].ToString(); if (str.Length > 0) lst.ELE_BI = str;

                str = dt.Rows[RowIndex]["ELE_C"].ToString(); if (str.Length > 0) lst.ELE_C = str;
                str = dt.Rows[RowIndex]["ELE_CA"].ToString(); if (str.Length > 0) lst.ELE_CA = str;
                str = dt.Rows[RowIndex]["ELE_CE"].ToString(); if (str.Length > 0) lst.ELE_CE = str;
                str = dt.Rows[RowIndex]["ELE_CEQ"].ToString(); if (str.Length > 0) lst.ELE_CEQ = str;
                str = dt.Rows[RowIndex]["ELE_CO"].ToString(); if (str.Length > 0) lst.ELE_CO = str;
                str = dt.Rows[RowIndex]["ELE_CR"].ToString(); if (str.Length > 0) lst.ELE_CR = str;
                str = dt.Rows[RowIndex]["ELE_CU"].ToString(); if (str.Length > 0) lst.ELE_CU = str;

                str = dt.Rows[RowIndex]["ELE_H"].ToString(); if (str.Length > 0) lst.ELE_H = str;
                str = dt.Rows[RowIndex]["ELE_MG"].ToString(); if (str.Length > 0) lst.ELE_MG = str;
                str = dt.Rows[RowIndex]["ELE_MN"].ToString(); if (str.Length > 0) lst.ELE_MN = str;
                str = dt.Rows[RowIndex]["ELE_MO"].ToString(); if (str.Length > 0) lst.ELE_MO = str;
                str = dt.Rows[RowIndex]["ELE_N"].ToString(); if (str.Length > 0) lst.ELE_N = str;
                str = dt.Rows[RowIndex]["ELE_NB"].ToString(); if (str.Length > 0) lst.ELE_NB = str;
                str = dt.Rows[RowIndex]["ELE_NI"].ToString(); if (str.Length > 0) lst.ELE_NI = str;

                str = dt.Rows[RowIndex]["ELE_O"].ToString(); if (str.Length > 0) lst.ELE_O = str;
                str = dt.Rows[RowIndex]["ELE_P"].ToString(); if (str.Length > 0) lst.ELE_P = str;
                str = dt.Rows[RowIndex]["ELE_PB"].ToString(); if (str.Length > 0) lst.ELE_PB = str;
                str = dt.Rows[RowIndex]["ELE_RE"].ToString(); if (str.Length > 0) lst.ELE_RE = str;
                str = dt.Rows[RowIndex]["ELE_S"].ToString(); if (str.Length > 0) lst.ELE_S = str;
                str = dt.Rows[RowIndex]["ELE_SB"].ToString(); if (str.Length > 0) lst.ELE_SB = str;
                str = dt.Rows[RowIndex]["ELE_SE"].ToString(); if (str.Length > 0) lst.ELE_SE = str;
                str = dt.Rows[RowIndex]["ELE_SI"].ToString(); if (str.Length > 0) lst.ELE_SI = str;
                str = dt.Rows[RowIndex]["ELE_SN"].ToString(); if (str.Length > 0) lst.ELE_SN = str;

                str = dt.Rows[RowIndex]["ELE_TA"].ToString(); if (str.Length > 0) lst.ELE_TA = str;
                str = dt.Rows[RowIndex]["ELE_TE"].ToString(); if (str.Length > 0) lst.ELE_TE = str;
                str = dt.Rows[RowIndex]["ELE_TI"].ToString(); if (str.Length > 0) lst.ELE_TI = str;
                str = dt.Rows[RowIndex]["ELE_V"].ToString(); if (str.Length > 0) lst.ELE_V = str;
                str = dt.Rows[RowIndex]["ELE_W"].ToString(); if (str.Length > 0) lst.ELE_W = str;
                str = dt.Rows[RowIndex]["ELE_ZN"].ToString(); if (str.Length > 0) lst.ELE_ZN = str;
                str = dt.Rows[RowIndex]["ELE_ZR"].ToString(); if (str.Length > 0) lst.ELE_ZR = str;

                LST.Add(lst);
            }

            return LST;
        }

        private void ELEM_ANA_Ini(ref ELEM_ANA lst)
        {
           lst.DEVICE_NO=" ";  lst.STATION=" ";         lst.HEAT_ID=" ";             lst.IRON_ID=" ";     
        lst.SAMPLETIME=" ";    lst.SAMPLE_NUMBER=" ";        lst.SAMPLE_ID =" ";               lst.ELE_ALS=" ";
        lst.ELE_ALT=" ";       lst.ELE_AS=" ";        lst.ELE_B=" ";        lst.ELE_BI=" ";
        lst.ELE_C=" ";         lst.ELE_CA=" ";         lst.ELE_CE=" ";        lst.ELE_CEQ=" ";
        lst.ELE_CO=" ";        lst.ELE_CR=" ";        lst.ELE_CU=" ";        lst.ELE_H=" ";
        lst.ELE_MG=" ";        lst.ELE_MN=" ";        lst.ELE_MO=" ";        lst.ELE_N=" ";
        lst.ELE_NB=" ";        lst.ELE_NI=" ";        lst.ELE_O=" ";        lst.ELE_P=" ";
        lst.ELE_PB=" ";        lst.ELE_RE=" ";        lst.ELE_S=" ";        lst.ELE_SB=" ";
        lst.ELE_SE=" ";        lst.ELE_SI=" ";        lst.ELE_SN=" ";        lst.ELE_TA=" ";
        lst.ELE_TE=" ";        lst.ELE_TI=" ";        lst.ELE_V=" ";        lst.ELE_W=" ";
        lst.ELE_ZN=" ";        lst.ELE_ZR =" ";
        }

        private void DefinePdfFont()
        {
            //定义字体
            iTextSharp.text.pdf.BaseFont bfHei = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMHEI.TTF", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontHei = new iTextSharp.text.Font(bfHei, 32, 1);
            iTextSharp.text.pdf.BaseFont bfKai = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\simkai.ttf", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontKai = new iTextSharp.text.Font(bfKai, 32, 1);
            iTextSharp.text.pdf.BaseFont bfSun = iTextSharp.text.pdf.BaseFont.createFont(@"C:\Windows\Fonts\SIMSUN.TTC,1", iTextSharp.text.pdf.BaseFont.IDENTITY_H, iTextSharp.text.pdf.BaseFont.NOT_EMBEDDED);
            FontSong = new iTextSharp.text.Font(bfSun, 32, 1);
        }
    }
}
