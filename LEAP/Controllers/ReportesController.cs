using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using LEAP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LEAP.Controllers
{ 
    public class ReportesController : Controller
    {
        ConsumerModel _consumer = new ConsumerModel();
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get_Customer_Search(string term)
        {
            
            List<ConsumerModel> productos = _consumer.Get_Consumer_AllDataByName(term);

            return Json(productos, JsonRequestBehavior.AllowGet);
        }

        //public FileStreamResult Consumer_Report(string _uci)
        //{
        //    List<ConsumerModel> consumerList = new List<ConsumerModel>();
        //    consumerList = _consumer.Get_Consumer_ByID(_uci);
        //    MemoryStream workStream = new MemoryStream();
        //    try
        //    {
        //        foreach (var consumer_temps in consumerList)
        //        {


        //            string true_image = Server.MapPath("~/Content/Images/true.png");
        //            string salse_image = Server.MapPath("~/Content/Images/false.png");
        //            iTextSharp.text.Image img_true = iTextSharp.text.Image.GetInstance(true_image);
        //            iTextSharp.text.Image img_false = iTextSharp.text.Image.GetInstance(salse_image);
                    
        //            Document document = new Document(PageSize.A4, 50, 50, 10, 50);
        //            PdfWriter writer = PdfWriter.GetInstance(document, workStream);
        //            writer.CloseStream = false;
        //            document.Open();

        //            // Fuentes y estilos
        //            Font titulo_f = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30, Font.BOLD);
        //            Font subtitulo_f = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD);
        //            Font subtitulo2_f = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
        //            Font consumer_f = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
        //            Font base_f = FontFactory.GetFont(FontFactory.HELVETICA, 12);
        //            Font check_f = FontFactory.GetFont(FontFactory.HELVETICA, 9);

        //            // Añadir título
        //            Paragraph titulo_t = new Paragraph("L.E.A.P", titulo_f);
        //            titulo_t.Alignment = Element.ALIGN_CENTER;
        //            document.Add(titulo_t);
        //            Chunk espacio = new Chunk(new VerticalPositionMark());
        //            // Añadir subtítulo
        //            Paragraph _subtitulo_t1 = new Paragraph("Learning Experiences & Alternative Programs for infants", subtitulo_f);
        //            _subtitulo_t1.Alignment = Element.ALIGN_CENTER;
        //            _subtitulo_t1.SpacingAfter = 2;
        //            document.Add(_subtitulo_t1);

        //            Paragraph _subtitulo_t2 = new Paragraph(" In-Home Infant Development/ Parenting Education", subtitulo2_f);
        //            _subtitulo_t2.Alignment = Element.ALIGN_CENTER;
        //            _subtitulo_t2.SpacingAfter = 5;
        //            document.Add(_subtitulo_t2);

        //            Paragraph consumer_t = new Paragraph("CONSUMER INFORMATION", consumer_f);
        //            consumer_t.Alignment = Element.ALIGN_CENTER;
        //            consumer_t.SpacingAfter = 5;
        //            document.Add(consumer_t);

        //            Paragraph InfoGeneral = new Paragraph();
        //            InfoGeneral.TabSettings = new TabSettings(56f);
        //            Chunk chunk1 = new Chunk("Date:", FontFactory.GetFont(FontFactory.HELVETICA, 12));
        //            Chunk chunk2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.ClosingReport), FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD));
        //            //string formattedDate = string.Format("{0:MM/dd/yyyy}", consumer_temps.ClosingReport);
        //            Chunk chunk4 = new Chunk("Information taken by:", FontFactory.GetFont(FontFactory.HELVETICA, 12));
        //            Chunk chunk5 = new Chunk("Ulises Samayoa", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD));
        //            InfoGeneral.Add(chunk1);
        //            InfoGeneral.Add(chunk2);
        //            InfoGeneral.Add(espacio);
        //            InfoGeneral.Add(chunk4);
        //            InfoGeneral.Add(chunk5);
        //            InfoGeneral.SpacingAfter = 10;
        //            document.Add(InfoGeneral);

        //            PdfContentByte cb = writer.DirectContent;
        //            cb.MoveTo(document.LeftMargin, document.PageSize.Height - 145);
        //            cb.LineTo(document.PageSize.Width - document.RightMargin, document.PageSize.Height - 145);
        //            cb.Stroke();


        //            Paragraph InfoService = new Paragraph();
        //            InfoService.TabSettings = new TabSettings(56f);
        //            Chunk ch_s1 = new Chunk("Eval  ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk ch_s2 = new Chunk(img_false, 0, 0);
        //            Chunk ch_s3 = new Chunk("New Consumer Referral", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk ch_s4 = new Chunk(img_true, 0, 0);
        //            Chunk ch_s5 = new Chunk("Reauthorized Consumer", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk ch_s6 = new Chunk(img_false, 0, 0);
        //            Chunk ch_s7 = new Chunk("Consumer Terminated", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk ch_s8 = new Chunk(img_true, 0, 0);
        //            Chunk ch_s9 = new Chunk("Update", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk ch_s10 = new Chunk(img_false, 0, 0);
        //            Chunk infs_espace = new Chunk("   ", FontFactory.GetFont(FontFactory.HELVETICA, 9));

        //            InfoService.Add(ch_s1);
        //            InfoService.Add(ch_s2);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s3);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s4);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s5);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s6);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s7);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s8);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s9);
        //            InfoService.Add(infs_espace);
        //            InfoService.Add(ch_s10);
        //            InfoService.SpacingAfter = 8;
        //            document.Add(InfoService);

        //            //Paragraph InfoService = new Paragraph();
        //            //InfoService.TabSettings = new TabSettings(56f);
        //            //InfoService.SpacingAfter = 8;

        //            //// Agregar texto y checkboxes
        //            //AddCheckboxWithText(writer, document, "Eval        ", true, InfoService, check_f, 0,70,160);
        //            //AddCheckboxWithText(writer, document, "New Consumer Referral      ", true, InfoService, check_f, 1, 70, 160);
        //            //AddCheckboxWithText(writer, document, "Reauthorized Consumer        ", false, InfoService, check_f,2, 70, 160);
        //            //AddCheckboxWithText(writer, document, "Consumer Terminated                     ", false, InfoService, check_f, 3, 70, 160);
        //            //AddCheckboxWithText(writer, document, "Update          ", false, InfoService, check_f, 4, 70, 160);

        //            //document.Add(InfoService);


        //            PdfPTable Consumer_table = new PdfPTable(1);
        //            Consumer_table.WidthPercentage = 100;
        //            Consumer_table.DefaultCell.Border = Rectangle.BOX;

        //            PdfPTable innerTable = new PdfPTable(1);
        //            innerTable.DefaultCell.Border = Rectangle.BOX;
        //            PdfPCell innerCell = new PdfPCell(new Phrase("CONSUMER INFORMATION AND REASON FOR REFERRAL"));
        //            innerCell.HorizontalAlignment = Element.ALIGN_CENTER;
        //            innerTable.AddCell(innerCell);
        //            PdfPCell innerTableCell = new PdfPCell(innerTable);
        //            innerTableCell.Padding = 0;
        //            Consumer_table.AddCell(innerTableCell);
        //            document.Add(Consumer_table);


        //            Paragraph InfoConsumer_l1 = new Paragraph();
        //            InfoConsumer_l1.TabSettings = new TabSettings(56f);
        //            Chunk IC_L1_C1 = new Chunk("Consume's Name: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L1_C2 = new Chunk(consumer_temps.ConsumersName, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L1_C3 = new Chunk("UCI #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L1_C4 = new Chunk(consumer_temps.UCI, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l1.Add(IC_L1_C1);
        //            InfoConsumer_l1.Add(IC_L1_C2);
        //            InfoConsumer_l1.Add(espacio);
        //            InfoConsumer_l1.Add(IC_L1_C3);
        //            InfoConsumer_l1.Add(IC_L1_C4);
        //            InfoConsumer_l1.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l1);


        //            Paragraph InfoConsumer_l2 = new Paragraph();
        //            InfoConsumer_l2.TabSettings = new TabSettings(56f);
        //            Chunk IC_L2_C1 = new Chunk("DOB: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L2_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.DOB), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L2_C3 = new Chunk("Age: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L2_C4 = new Chunk(consumer_temps.Age.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L2_C5 = new Chunk("Adj Age: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L2_C6 = new Chunk(consumer_temps.adjage.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L2_C7 = new Chunk("Months  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L2_C8 = new Chunk("Sex: ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L2_C9 = new Chunk("Male: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L2_C11 = new Chunk("Female: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk infs12_espace = new Chunk("   ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l2.Add(IC_L2_C1);
                    
        //            InfoConsumer_l2.Add(IC_L2_C2);
        //            InfoConsumer_l2.Add(espacio);
        //            InfoConsumer_l2.Add(IC_L2_C3);
        //            InfoConsumer_l2.Add(IC_L2_C4);
        //            InfoConsumer_l2.Add(espacio);
        //            InfoConsumer_l2.Add(IC_L2_C5);
        //            InfoConsumer_l2.Add(IC_L2_C6);
        //            InfoConsumer_l2.Add(espacio);
        //            InfoConsumer_l2.Add(IC_L2_C7);
        //            InfoConsumer_l2.Add(IC_L2_C8);
        //            InfoConsumer_l2.Add(IC_L2_C9);
        //            if (consumer_temps.Male)
        //            {
        //                Chunk IC_L2_C10 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l2.Add(IC_L2_C10);
        //            }
        //            else
        //            {
        //                Chunk IC_L2_C10 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l2.Add(IC_L2_C10);
        //            }
        //            InfoConsumer_l2.Add(infs12_espace);
        //            InfoConsumer_l2.Add(IC_L2_C11);
                    
        //            if (consumer_temps.Female)
        //            {
        //                Chunk IC_L2_C12 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l2.Add(IC_L2_C12);
        //            }
        //            else
        //            {
        //                Chunk IC_L2_C12 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l2.Add(IC_L2_C12);
        //            }
        //            InfoConsumer_l2.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l2);

        //            Paragraph InfoConsumer_l3 = new Paragraph();
        //            InfoConsumer_l3.TabSettings = new TabSettings(56f);
        //            Chunk IC_L3_C1 = new Chunk("Address: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L3_C2 = new Chunk(consumer_temps.Customers_Address, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L3_C3 = new Chunk("City: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L3_C4 = new Chunk(consumer_temps.City, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L3_C5 = new Chunk("State: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L3_C6 = new Chunk(consumer_temps.State, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L3_C7 = new Chunk("ZipCode: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L3_C8 = new Chunk(consumer_temps.ZipCode, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l3.Add(IC_L3_C1);
        //            InfoConsumer_l3.Add(IC_L3_C2);
        //            InfoConsumer_l3.Add(espacio);
        //            InfoConsumer_l3.Add(IC_L3_C3);
        //            InfoConsumer_l3.Add(IC_L3_C4);
        //            InfoConsumer_l3.Add(espacio);
        //            InfoConsumer_l3.Add(IC_L3_C5);
        //            InfoConsumer_l3.Add(IC_L3_C6);
        //            InfoConsumer_l3.Add(espacio);
        //            InfoConsumer_l3.Add(IC_L3_C7);
        //            InfoConsumer_l3.Add(IC_L3_C8);
        //            InfoConsumer_l3.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l3);


        //            Paragraph InfoConsumer_l4 = new Paragraph();
        //            InfoConsumer_l4.TabSettings = new TabSettings(56f);
        //            Chunk IC_L4_C1 = new Chunk("Telephone #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L4_C2 = new Chunk(consumer_temps.Customers_Telephone, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L4_C3 = new Chunk("Emergency Telephone #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L4_C4 = new Chunk(consumer_temps.EmergencyTelephone, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l4.Add(IC_L4_C1);
        //            InfoConsumer_l4.Add(IC_L4_C2);
        //            InfoConsumer_l4.Add(espacio);
        //            InfoConsumer_l4.Add(IC_L4_C3);
        //            InfoConsumer_l4.Add(IC_L4_C4);
        //            InfoConsumer_l4.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l4);


        //            Paragraph InfoConsumer_l5 = new Paragraph();
        //            InfoConsumer_l5.TabSettings = new TabSettings(56f);
        //            Chunk IC_L5_C1 = new Chunk("Parent/Caregiver's Name #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L5_C2 = new Chunk(consumer_temps.Parent_CaregiversName, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l5.Add(IC_L5_C1);
        //            InfoConsumer_l5.Add(IC_L5_C2);
        //            InfoConsumer_l5.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l5);

        //            Paragraph InfoConsumer_l6 = new Paragraph();
        //            InfoConsumer_l6.TabSettings = new TabSettings(56f);
        //            Chunk IC_L6_C1 = new Chunk("Primary Language:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L6_C2 = new Chunk("  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L6_C3 = new Chunk("English:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L6_C5 = new Chunk("Spanish:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L6_C7 = new Chunk("Bilingual:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L6_C9 = new Chunk("Other:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L6_C10 = new Chunk(consumer_temps.Other, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk infs16_espace = new Chunk("        ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l6.Add(IC_L6_C1);
        //            InfoConsumer_l6.Add(IC_L6_C2);
        //            InfoConsumer_l6.Add(IC_L6_C3);
        //            InfoConsumer_l6.Add(infs16_espace);
        //            if (consumer_temps.English == true)
        //            {
        //                Chunk IC_L6_C4 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l6.Add(IC_L6_C4);
        //            }
        //            else
        //            {
        //                Chunk IC_L6_C4 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l6.Add(IC_L6_C4);
        //            }
        //            InfoConsumer_l6.Add(infs16_espace);
        //            InfoConsumer_l6.Add(IC_L6_C5);
        //            InfoConsumer_l6.Add(infs16_espace);
                    
        //            if (consumer_temps.Spanish == true)
        //            {
        //                Chunk IC_L6_C6 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l6.Add(IC_L6_C6);
        //            }
        //            else
        //            {
        //                Chunk IC_L6_C6 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l6.Add(IC_L6_C6);
        //            }
        //            InfoConsumer_l6.Add(infs16_espace);
        //            InfoConsumer_l6.Add(IC_L6_C7);
        //            InfoConsumer_l6.Add(infs16_espace);
        //            if (consumer_temps.Bilinigual == true)
        //            {
        //                Chunk IC_L6_C8 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l6.Add(IC_L6_C8);
        //            }
        //            else
        //            {
        //                Chunk IC_L6_C8 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l6.Add(IC_L6_C8);
        //            }
        //            InfoConsumer_l6.Add(infs16_espace);
        //            InfoConsumer_l6.Add(IC_L6_C9);
        //            InfoConsumer_l6.Add(IC_L6_C10);
        //            InfoConsumer_l6.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l6);

        //            Paragraph InfoService2 = new Paragraph();
        //            InfoService2.TabSettings = new TabSettings(56f);
        //            InfoService2.SpacingAfter = 8;




        //            Paragraph InfoConsumer_l7 = new Paragraph();
        //            InfoConsumer_l7.TabSettings = new TabSettings(56f);
        //            Chunk IC_L7_C1 = new Chunk("Reason for Referral (Diagnosis):\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L7_C2 = new Chunk(consumer_temps.ReasonfoReferral_Diagnosis, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l7.Add(IC_L7_C1);
        //            InfoConsumer_l7.Add(IC_L7_C2);
        //            InfoConsumer_l7.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l7);


        //            PdfPTable Authorization_table = new PdfPTable(1);
        //            Authorization_table.WidthPercentage = 100;
        //            Authorization_table.DefaultCell.Border = Rectangle.BOX;
        //            PdfPTable innerTable2 = new PdfPTable(1);
        //            innerTable2.DefaultCell.Border = Rectangle.BOX;
        //            PdfPCell innerCell2 = new PdfPCell(new Phrase("AUTHORIZATION AND HOURS PER WEEK"));
        //            innerCell2.HorizontalAlignment = Element.ALIGN_CENTER;
        //            innerTable2.AddCell(innerCell2);
        //            PdfPCell innerTableCell2 = new PdfPCell(innerTable2);
        //            innerTableCell2.Padding = 0;
        //            Authorization_table.AddCell(innerTableCell2);
        //            document.Add(Authorization_table);

        //            Paragraph InfoConsumer_l8 = new Paragraph();
        //            InfoConsumer_l8.TabSettings = new TabSettings(56f);
        //            Chunk IC_L8_C1 = new Chunk("Authorization From:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L8_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.AuhtorizationFrom), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L8_C4 = new Chunk("To:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L8_C5 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.To), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L8_C7 = new Chunk("Authorization: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L8_C8 = new Chunk(consumer_temps.Authorization.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l8.Add(IC_L8_C1);
        //            InfoConsumer_l8.Add(IC_L8_C2);
        //            InfoConsumer_l8.Add(espacio);
        //            InfoConsumer_l8.Add(IC_L8_C4);
        //            InfoConsumer_l8.Add(IC_L8_C5);
        //            InfoConsumer_l8.Add(espacio);
        //            InfoConsumer_l8.Add(IC_L8_C7);
        //            InfoConsumer_l8.Add(IC_L8_C8);
        //            InfoConsumer_l8.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l8);


        //            Paragraph InfoConsumer_l9 = new Paragraph();
        //            InfoConsumer_l9.TabSettings = new TabSettings(56f);
        //            Chunk IC_L9_C1 = new Chunk("Hours per Week:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L9_C2 = new Chunk(consumer_temps.HoursperWeek.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L9_C3 = new Chunk("Maximun hours per month:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L9_C4 = new Chunk(consumer_temps.Maximunhourspermonth.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l9.Add(IC_L9_C1);
        //            InfoConsumer_l9.Add(IC_L9_C2);
        //            InfoConsumer_l9.Add(espacio);
        //            InfoConsumer_l9.Add(IC_L9_C3);
        //            InfoConsumer_l9.Add(IC_L9_C4);
        //            InfoConsumer_l9.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l9);

        //            Paragraph InfoConsumer_l10 = new Paragraph();
        //            InfoConsumer_l10.TabSettings = new TabSettings(56f);
        //            Chunk IC_L10_C1 = new Chunk("Termination date effective:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L10_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.Terminationdateeffective), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L10_C3 = new Chunk("Additional Evel:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L10_C4 = new Chunk("NO SE QUE CAMPO ES EN BASE DE DATOS", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l10.Add(IC_L10_C1);
        //            InfoConsumer_l10.Add(IC_L10_C2);
        //            InfoConsumer_l10.Add(espacio);
        //            InfoConsumer_l10.Add(IC_L10_C3);
        //            InfoConsumer_l10.Add(IC_L10_C4);
        //            InfoConsumer_l10.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l10);

        //            PdfPTable services_table = new PdfPTable(1);
        //            services_table.WidthPercentage = 100;
        //            services_table.DefaultCell.Border = Rectangle.BOX;
        //            PdfPTable innerTable3 = new PdfPTable(1);
        //            innerTable3.DefaultCell.Border = Rectangle.BOX;
        //            PdfPCell innerCell3 = new PdfPCell(new Phrase("TYPE OF SERVICES AND SPECIALIST ASSIGNED"));
        //            innerCell3.HorizontalAlignment = Element.ALIGN_CENTER;
        //            innerTable3.AddCell(innerCell3);
        //            PdfPCell innerTableCell3 = new PdfPCell(innerTable3);
        //            innerTableCell3.Padding = 0;
        //            services_table.AddCell(innerTableCell3);
        //            services_table.SpacingAfter = 5;
        //            document.Add(services_table);

        //            Paragraph InfoConsumer_l11 = new Paragraph();
        //            InfoConsumer_l11.TabSettings = new TabSettings(56f);
        //            Chunk IC_L11_C1 = new Chunk("InHome E.I:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L11_C3 = new Chunk("E.I. with OT/PT/SLP Consultation:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L11_C5 = new Chunk("OT/PT/SLP:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk infs11_espace = new Chunk("   ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l11.Add(IC_L11_C1);
        //            InfoConsumer_l11.Add(infs11_espace);
        //            if (consumer_temps.In_homeEI == true)
        //            {
        //                Chunk IC_L11_C2 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l6.Add(IC_L11_C2);
        //            }
        //            else
        //            {
        //                Chunk IC_L11_C2 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l6.Add(IC_L11_C2);
        //            }
        //            InfoConsumer_l11.Add(espacio);
        //            InfoConsumer_l11.Add(IC_L11_C3);
        //            InfoConsumer_l11.Add(infs11_espace);
        //            if (consumer_temps.EIwithOT_PT_SLPConsultation == true)
        //            {
        //                Chunk IC_L11_C4 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l6.Add(IC_L11_C4);
        //            }
        //            else
        //            {
        //                Chunk IC_L11_C4 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l6.Add(IC_L11_C4);
        //            }
        //            InfoConsumer_l11.Add(espacio);
        //            InfoConsumer_l11.Add(IC_L11_C5);
        //            InfoConsumer_l11.Add(infs11_espace);
        //            if (consumer_temps.OT_PT_SLP == true)
        //            {
        //                Chunk IC_L11_C6 = new Chunk(img_true, 0, 0);
        //                InfoConsumer_l6.Add(IC_L11_C6);
        //            }
        //            else
        //            {
        //                Chunk IC_L11_C6 = new Chunk(img_false, 0, 0);
        //                InfoConsumer_l6.Add(IC_L11_C6);
        //            }
        //            InfoConsumer_l11.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l11);

        //            Paragraph InfoConsumer_l12 = new Paragraph();
        //            InfoConsumer_l12.TabSettings = new TabSettings(56f);
        //            Chunk IC_L12_C1 = new Chunk("CDS:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L12_C2 = new Chunk("NO SE QUE CAMPOS ES", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L12_C3 = new Chunk("Speciality (3 campos)", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L12_C4 = new Chunk(consumer_temps.Specialis1, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L12_C5 = new Chunk("Therapist: (no campo)", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L12_C6 = new Chunk(consumer_temps.Specialisr2, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l12.Add(IC_L12_C1);
        //            InfoConsumer_l12.Add(IC_L12_C2);
        //            InfoConsumer_l12.Add(espacio);
        //            InfoConsumer_l12.Add(IC_L12_C3);
        //            InfoConsumer_l12.Add(IC_L12_C4);
        //            InfoConsumer_l12.Add(espacio);
        //            InfoConsumer_l12.Add(IC_L12_C5);
        //            InfoConsumer_l12.Add(IC_L12_C6);
        //            InfoConsumer_l12.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l12);

        //            Paragraph InfoConsumer_l13 = new Paragraph();
        //            InfoConsumer_l13.TabSettings = new TabSettings(56f);
        //            Chunk IC_L13_C1 = new Chunk("Program Presenter:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L13_C2 = new Chunk(consumer_temps.presenter, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l13.Add(IC_L13_C1);
        //            InfoConsumer_l13.Add(IC_L13_C2);
        //            InfoConsumer_l13.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l13);


        //            Paragraph InfoConsumer_l14 = new Paragraph();
        //            InfoConsumer_l14.TabSettings = new TabSettings(56f);
        //            Chunk IC_L14_C1 = new Chunk("Notes:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L14_C2 = new Chunk("________________________________", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l14.Add(IC_L14_C1);
        //            InfoConsumer_l14.Add(IC_L14_C2);
        //            InfoConsumer_l14.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l14);

        //            PdfPTable regiona_table = new PdfPTable(1);
        //            regiona_table.WidthPercentage = 100;
        //            regiona_table.DefaultCell.Border = Rectangle.BOX;
        //            PdfPTable innerTable4 = new PdfPTable(1);
        //            innerTable4.DefaultCell.Border = Rectangle.BOX;
        //            PdfPCell innerCell4 = new PdfPCell(new Phrase("REGIONAL CENTER INFORMATION"));
        //            innerCell4.HorizontalAlignment = Element.ALIGN_CENTER;
        //            innerTable4.AddCell(innerCell4);
        //            PdfPCell innerTableCell4 = new PdfPCell(innerTable4);
        //            innerTableCell4.Padding = 0;
        //            regiona_table.AddCell(innerTableCell4);
        //            document.Add(regiona_table);


        //            Paragraph InfoConsumer_l15 = new Paragraph();
        //            InfoConsumer_l15.TabSettings = new TabSettings(56f);
        //            Chunk IC_L15_C1 = new Chunk("Regional Center:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L15_C2 = new Chunk(consumer_temps.RegionalCenter, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L15_C3 = new Chunk("Telephone #:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L15_C4 = new Chunk("NO SE QUE CAMPO ES", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l15.Add(IC_L15_C1);
        //            InfoConsumer_l15.Add(IC_L15_C2);
        //            InfoConsumer_l15.Add(espacio);
        //            InfoConsumer_l15.Add(IC_L15_C3);
        //            InfoConsumer_l15.Add(IC_L15_C4);
        //            InfoConsumer_l15.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l15);

        //            Paragraph InfoConsumer_l16 = new Paragraph();
        //            InfoConsumer_l16.TabSettings = new TabSettings(56f);
        //            Chunk IC_L16_C1 = new Chunk("Address:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L16_C2 = new Chunk("NO SE QUE CAMPO ES", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l16.Add(IC_L16_C1);
        //            InfoConsumer_l16.Add(IC_L16_C2);
        //            InfoConsumer_l16.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l16);

        //            Paragraph InfoConsumer_l17 = new Paragraph();
        //            InfoConsumer_l17.TabSettings = new TabSettings(56f);
        //            Chunk IC_L17_C1 = new Chunk("Service Coordinato's name:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L17_C2 = new Chunk("NO SE QUE CAMPO ES", FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L17_C3 = new Chunk("Extension #:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L17_C4 = new Chunk(consumer_temps.extension, FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l17.Add(IC_L17_C1);
        //            InfoConsumer_l17.Add(IC_L17_C2);
        //            InfoConsumer_l17.Add(espacio);
        //            InfoConsumer_l17.Add(IC_L17_C3);
        //            InfoConsumer_l17.Add(IC_L17_C4);
        //            InfoConsumer_l17.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l17);

        //            PdfPTable report_table = new PdfPTable(1);
        //            report_table.WidthPercentage = 100;
        //            report_table.DefaultCell.Border = Rectangle.BOX;
        //            PdfPTable innerTable5 = new PdfPTable(1);
        //            innerTable5.DefaultCell.Border = Rectangle.BOX;
        //            PdfPCell innerCell5 = new PdfPCell(new Phrase("REPORT DUE DATES"));
        //            innerCell5.HorizontalAlignment = Element.ALIGN_CENTER;
        //            innerTable5.AddCell(innerCell5);
        //            PdfPCell innerTableCell5 = new PdfPCell(innerTable5);
        //            innerTableCell5.Padding = 0;
        //            report_table.AddCell(innerTableCell5);
        //            document.Add(report_table);

        //            Paragraph InfoConsumer_l18 = new Paragraph();
        //            InfoConsumer_l18.TabSettings = new TabSettings(56f);
        //            Chunk IC_L18_C1 = new Chunk("Initial Evaluation:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L18_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.InitialEvaluation), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L18_C3 = new Chunk("Initial Evaluation due by:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L18_C4 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.InitialEvaluationdueby), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l18.Add(IC_L18_C1);
        //            InfoConsumer_l18.Add(IC_L18_C2);
        //            InfoConsumer_l18.Add(espacio);
        //            InfoConsumer_l18.Add(IC_L18_C3);
        //            InfoConsumer_l18.Add(IC_L18_C4);
        //            InfoConsumer_l18.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l18);

        //            Paragraph InfoConsumer_l19 = new Paragraph();
        //            InfoConsumer_l19.TabSettings = new TabSettings(56f);
        //            Chunk IC_L19_C1 = new Chunk("1st Progress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L19_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.FirstProgressReport), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L19_C3 = new Chunk("4thProgress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L19_C4 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.fourthProgressReport), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l19.Add(IC_L19_C1);
        //            InfoConsumer_l19.Add(IC_L19_C2);
        //            InfoConsumer_l19.Add(espacio);
        //            InfoConsumer_l19.Add(IC_L19_C3);
        //            InfoConsumer_l19.Add(IC_L19_C4);
        //            InfoConsumer_l19.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l19);

        //            Paragraph InfoConsumer_l20 = new Paragraph();
        //            InfoConsumer_l20.TabSettings = new TabSettings(56f);
        //            Chunk IC_L20_C1 = new Chunk("2nd Progress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L20_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.SecondProgressReport), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l20.Add(IC_L20_C1);
        //            InfoConsumer_l20.Add(IC_L20_C2);
        //            InfoConsumer_l20.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l20);

        //            Paragraph InfoConsumer_l21 = new Paragraph();
        //            InfoConsumer_l21.TabSettings = new TabSettings(56f);
        //            Chunk IC_L21_C1 = new Chunk("3rd Progress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L21_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.ThirdProgressReport), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            Chunk IC_L21_C3 = new Chunk("Closing Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
        //            Chunk IC_L21_C4 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.ClosingReport), FontFactory.GetFont(FontFactory.HELVETICA, 9));
        //            InfoConsumer_l21.Add(IC_L21_C1);
        //            InfoConsumer_l21.Add(IC_L21_C2);
        //            InfoConsumer_l21.Add(espacio);
        //            InfoConsumer_l21.Add(IC_L21_C3);
        //            InfoConsumer_l21.Add(IC_L21_C4);
        //            InfoConsumer_l21.SpacingAfter = 5;
        //            document.Add(InfoConsumer_l21);
        //            document.Close();
        //        }
        //        workStream.Position = 0;
        //        return new FileStreamResult(workStream, "application/pdf");
        //    }
        //    catch (Exception _error)
        //    {
        //        workStream.Position = 0;
        //        return new FileStreamResult(workStream,"application/pdf");
        //    }
            
            
        //}

    }
}