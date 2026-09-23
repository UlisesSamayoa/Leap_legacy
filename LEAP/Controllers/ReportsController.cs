    using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System.IO;
using iTextSharp.text;
using SelectPdf;
using ClosedXML.Excel;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Bibliography;
using System.Globalization;

namespace LEAP.Controllers
{
    public class ReportsController : Controller
    {
        ConsumerModel _ConsumerModel = new ConsumerModel();
        ReportsModel _ReportsModel = new ReportsModel();
        NotesxConsumerModel _NotesConsumerModel = new NotesxConsumerModel();
        CitiesModel _CitiesModel = new CitiesModel();

        private List<SelectListItem> _NotesList;
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get_Customer_Search(string term)
        {

            List<ConsumerModel> productos = _ConsumerModel.Get_Consumer_AllDataByName(term);

            return Json(productos, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Rpt_consumer()
        {
            List<ConsumerModel> _notes = _ConsumerModel.Get_Consumer().ToList();
            _NotesList = new List<SelectListItem>();
            foreach (var items in _notes)
            {
                _NotesList.Add(new SelectListItem
                {
                    Text = items.Name,
                    Value = items.UCI
                });
            }
            ViewBag.VB_Consumer = _NotesList;
            return View();
        }

        public ActionResult Rpt_CreateConsumer(string UCI)
        {
            var consumer = _ReportsModel.Get_Reports_RPTConsumer(UCI);
            return PartialView("_RptConsumerPartial", consumer);
            //return PartialView("_RptConsumerPreviewPartial", consumer);
        }

        public ActionResult Rpt_Notesconsumer()
        {
            return View();
        }
        public ActionResult Rpt_CreateNotesConsumer(string UCI)
        {
            List<ReportsModel> consumer = _ReportsModel.Get_NotesXConsumer(UCI);
            return PartialView("_RptNotesConsumerPartial", consumer);
        }




        [HttpPost]
        public FileStreamResult Consumer_Report(string UCI)
        {
            List<ReportsModel> consumerList = new List<ReportsModel>();
            consumerList = _ReportsModel.Get_Reports_RPTConsumerList(UCI);
            //var _consumerList = _ReportsModel.Get_Reports_RPTConsumer(UCI);
            MemoryStream workStream = new MemoryStream();
            try
            {
                foreach (var consumer_temps in consumerList)
                {


                    string true_image = Server.MapPath("~/Content/Images/true.png");
                    string salse_image = Server.MapPath("~/Content/Images/false.png");
                    iTextSharp.text.Image img_true = iTextSharp.text.Image.GetInstance(true_image);
                    iTextSharp.text.Image img_false = iTextSharp.text.Image.GetInstance(salse_image);

                    Document document = new Document(PageSize.A4, 50, 50, 10, 50);
                    PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                    writer.CloseStream = false;
                    document.Open();

                    // Fuentes y estilos
                    Font titulo_f = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30, Font.BOLD);
                    Font subtitulo_f = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD);
                    Font subtitulo2_f = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
                    Font consumer_f = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                    Font base_f = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                    Font check_f = FontFactory.GetFont(FontFactory.HELVETICA, 9);

                    // Añadir título
                    Paragraph titulo_t = new Paragraph("L.E.A.P", titulo_f);
                    titulo_t.Alignment = Element.ALIGN_CENTER;
                    document.Add(titulo_t);
                    Chunk espacio = new Chunk(new VerticalPositionMark());
                    // Añadir subtítulo
                    Paragraph _subtitulo_t1 = new Paragraph("Learning Experiences & Alternative Programs for infants", subtitulo_f);
                    _subtitulo_t1.Alignment = Element.ALIGN_CENTER;
                    _subtitulo_t1.SpacingAfter = 2;
                    document.Add(_subtitulo_t1);

                    Paragraph _subtitulo_t2 = new Paragraph(" In-Home Infant Development/ Parenting Education", subtitulo2_f);
                    _subtitulo_t2.Alignment = Element.ALIGN_CENTER;
                    _subtitulo_t2.SpacingAfter = 5;
                    document.Add(_subtitulo_t2);

                    Paragraph consumer_t = new Paragraph("CONSUMER INFORMATION", consumer_f);
                    consumer_t.Alignment = Element.ALIGN_CENTER;
                    consumer_t.SpacingAfter = 5;
                    document.Add(consumer_t);

                    Paragraph InfoGeneral = new Paragraph();
                    InfoGeneral.TabSettings = new TabSettings(56f);
                    Chunk chunk1 = new Chunk("Date:", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                    Chunk chunk2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.ReportClose), FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD));
                    //string formattedDate = string.Format("{0:MM/dd/yyyy}", consumer_temps.ClosingReport);
                    Chunk chunk4 = new Chunk("Information taken by:", FontFactory.GetFont(FontFactory.HELVETICA, 12));
                    Chunk chunk5 = new Chunk("José Gudiel", FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD));
                    InfoGeneral.Add(chunk1);
                    InfoGeneral.Add(chunk2);
                    InfoGeneral.Add(espacio);
                    InfoGeneral.Add(chunk4);
                    InfoGeneral.Add(chunk5);
                    InfoGeneral.SpacingAfter = 10;
                    document.Add(InfoGeneral);

                    PdfContentByte cb = writer.DirectContent;
                    cb.MoveTo(document.LeftMargin, document.PageSize.Height - 145);
                    cb.LineTo(document.PageSize.Width - document.RightMargin, document.PageSize.Height - 145);
                    cb.Stroke();


                    Paragraph InfoService = new Paragraph();
                    InfoService.TabSettings = new TabSettings(56f);
                    Chunk ch_s1 = new Chunk("Eval  ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk ch_s2 = new Chunk(img_false, 0, 0);
                    Chunk ch_s3 = new Chunk("New Consumer Referral", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk ch_s4 = new Chunk(img_true, 0, 0);
                    Chunk ch_s5 = new Chunk("Reauthorized Consumer", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk ch_s6 = new Chunk(img_false, 0, 0);
                    Chunk ch_s7 = new Chunk("Consumer Terminated", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk ch_s8 = new Chunk(img_true, 0, 0);
                    Chunk ch_s9 = new Chunk("Update", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk ch_s10 = new Chunk(img_false, 0, 0);
                    Chunk infs_espace = new Chunk("   ", FontFactory.GetFont(FontFactory.HELVETICA, 9));

                    InfoService.Add(ch_s1);
                    InfoService.Add(ch_s2);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s3);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s4);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s5);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s6);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s7);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s8);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s9);
                    InfoService.Add(infs_espace);
                    InfoService.Add(ch_s10);
                    InfoService.SpacingAfter = 8;
                    document.Add(InfoService);

                    //Paragraph InfoService = new Paragraph();
                    //InfoService.TabSettings = new TabSettings(56f);
                    //InfoService.SpacingAfter = 8;

                    //// Agregar texto y checkboxes
                    //AddCheckboxWithText(writer, document, "Eval        ", true, InfoService, check_f, 0,70,160);
                    //AddCheckboxWithText(writer, document, "New Consumer Referral      ", true, InfoService, check_f, 1, 70, 160);
                    //AddCheckboxWithText(writer, document, "Reauthorized Consumer        ", false, InfoService, check_f,2, 70, 160);
                    //AddCheckboxWithText(writer, document, "Consumer Terminated                     ", false, InfoService, check_f, 3, 70, 160);
                    //AddCheckboxWithText(writer, document, "Update          ", false, InfoService, check_f, 4, 70, 160);

                    //document.Add(InfoService);


                    PdfPTable Consumer_table = new PdfPTable(1);
                    Consumer_table.WidthPercentage = 100;
                    Consumer_table.DefaultCell.Border = Rectangle.BOX;

                    PdfPTable innerTable = new PdfPTable(1);
                    innerTable.DefaultCell.Border = Rectangle.BOX;
                    PdfPCell innerCell = new PdfPCell(new Phrase("CONSUMER INFORMATION AND REASON FOR REFERRAL"));
                    innerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    innerTable.AddCell(innerCell);
                    PdfPCell innerTableCell = new PdfPCell(innerTable);
                    innerTableCell.Padding = 0;
                    Consumer_table.AddCell(innerTableCell);
                    document.Add(Consumer_table);


                    Paragraph InfoConsumer_l1 = new Paragraph();
                    InfoConsumer_l1.TabSettings = new TabSettings(56f);
                    Chunk IC_L1_C1 = new Chunk("Consume's Name: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L1_C2 = new Chunk(consumer_temps.ConsumerName, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L1_C3 = new Chunk("UCI #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L1_C4 = new Chunk(consumer_temps.UCI, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l1.Add(IC_L1_C1);
                    InfoConsumer_l1.Add(IC_L1_C2);
                    InfoConsumer_l1.Add(espacio);
                    InfoConsumer_l1.Add(IC_L1_C3);
                    InfoConsumer_l1.Add(IC_L1_C4);
                    InfoConsumer_l1.SpacingAfter = 5;
                    document.Add(InfoConsumer_l1);


                    Paragraph InfoConsumer_l2 = new Paragraph();
                    InfoConsumer_l2.TabSettings = new TabSettings(56f);
                    Chunk IC_L2_C1 = new Chunk("DOB: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L2_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.DateOfBirth), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L2_C3 = new Chunk("Age: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L2_C4 = new Chunk(consumer_temps.Age.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L2_C5 = new Chunk("Adj Age: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L2_C6 = new Chunk(consumer_temps.adjage.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L2_C7 = new Chunk("Months  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L2_C8 = new Chunk("Sex: ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L2_C9 = new Chunk("Male: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L2_C11 = new Chunk("Female: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk infs12_espace = new Chunk("   ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l2.Add(IC_L2_C1);

                    InfoConsumer_l2.Add(IC_L2_C2);
                    InfoConsumer_l2.Add(espacio);
                    InfoConsumer_l2.Add(IC_L2_C3);
                    InfoConsumer_l2.Add(IC_L2_C4);
                    InfoConsumer_l2.Add(espacio);
                    InfoConsumer_l2.Add(IC_L2_C5);
                    InfoConsumer_l2.Add(IC_L2_C6);
                    InfoConsumer_l2.Add(espacio);
                    InfoConsumer_l2.Add(IC_L2_C7);
                    InfoConsumer_l2.Add(IC_L2_C8);
                    InfoConsumer_l2.Add(IC_L2_C9);
                    //if (consumer_temps.Gender)
                    //{
                    //    Chunk IC_L2_C10 = new Chunk(img_true, 0, 0);
                    //    InfoConsumer_l2.Add(IC_L2_C10);
                    //}
                    //else
                    //{
                    //    Chunk IC_L2_C10 = new Chunk(img_false, 0, 0);
                    //    InfoConsumer_l2.Add(IC_L2_C10);
                    //}
                    Chunk IC_L2_C10 = new Chunk(img_false, 0, 0);
                    InfoConsumer_l2.Add(IC_L2_C10);
                    InfoConsumer_l2.Add(infs12_espace);
                    InfoConsumer_l2.Add(IC_L2_C11);

                    //if (consumer_temps.Female)
                    //{
                    //    Chunk IC_L2_C12 = new Chunk(img_true, 0, 0);
                    //    InfoConsumer_l2.Add(IC_L2_C12);
                    //}
                    //else
                    //{
                    //    Chunk IC_L2_C12 = new Chunk(img_false, 0, 0);
                    //    InfoConsumer_l2.Add(IC_L2_C12);
                    //}
                    //InfoConsumer_l2.SpacingAfter = 5;
                    //document.Add(InfoConsumer_l2);

                    Paragraph InfoConsumer_l3 = new Paragraph();
                    InfoConsumer_l3.TabSettings = new TabSettings(56f);
                    Chunk IC_L3_C1 = new Chunk("Address: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L3_C2 = new Chunk(consumer_temps.Address, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L3_C3 = new Chunk("City: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L3_C4 = new Chunk(consumer_temps.CityID.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L3_C5 = new Chunk("State: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L3_C6 = new Chunk(consumer_temps.State, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L3_C7 = new Chunk("ZipCode: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L3_C8 = new Chunk(consumer_temps.ZipCode, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l3.Add(IC_L3_C1);
                    InfoConsumer_l3.Add(IC_L3_C2);
                    InfoConsumer_l3.Add(espacio);
                    InfoConsumer_l3.Add(IC_L3_C3);
                    InfoConsumer_l3.Add(IC_L3_C4);
                    InfoConsumer_l3.Add(espacio);
                    InfoConsumer_l3.Add(IC_L3_C5);
                    InfoConsumer_l3.Add(IC_L3_C6);
                    InfoConsumer_l3.Add(espacio);
                    InfoConsumer_l3.Add(IC_L3_C7);
                    InfoConsumer_l3.Add(IC_L3_C8);
                    InfoConsumer_l3.SpacingAfter = 5;
                    document.Add(InfoConsumer_l3);


                    Paragraph InfoConsumer_l4 = new Paragraph();
                    InfoConsumer_l4.TabSettings = new TabSettings(56f);
                    Chunk IC_L4_C1 = new Chunk("Telephone #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L4_C2 = new Chunk(consumer_temps.Phone, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L4_C3 = new Chunk("Emergency Telephone #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L4_C4 = new Chunk(consumer_temps.EmergencyPhone, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l4.Add(IC_L4_C1);
                    InfoConsumer_l4.Add(IC_L4_C2);
                    InfoConsumer_l4.Add(espacio);
                    InfoConsumer_l4.Add(IC_L4_C3);
                    InfoConsumer_l4.Add(IC_L4_C4);
                    InfoConsumer_l4.SpacingAfter = 5;
                    document.Add(InfoConsumer_l4);


                    Paragraph InfoConsumer_l5 = new Paragraph();
                    InfoConsumer_l5.TabSettings = new TabSettings(56f);
                    Chunk IC_L5_C1 = new Chunk("Parent/Caregiver's Name #: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L5_C2 = new Chunk(consumer_temps.ParentFullName, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l5.Add(IC_L5_C1);
                    InfoConsumer_l5.Add(IC_L5_C2);
                    InfoConsumer_l5.SpacingAfter = 5;
                    document.Add(InfoConsumer_l5);

                    Paragraph InfoConsumer_l6 = new Paragraph();
                    InfoConsumer_l6.TabSettings = new TabSettings(56f);
                    Chunk IC_L6_C1 = new Chunk("Primary Language:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L6_C2 = new Chunk("  ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L6_C3 = new Chunk("English:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L6_C5 = new Chunk("Spanish:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L6_C7 = new Chunk("Bilingual:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L6_C9 = new Chunk("Other:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    //Chunk IC_L6_C10 = new Chunk(consumer_temps.Other, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk infs16_espace = new Chunk("        ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l6.Add(IC_L6_C1);
                    InfoConsumer_l6.Add(IC_L6_C2);
                    InfoConsumer_l6.Add(IC_L6_C3);
                    InfoConsumer_l6.Add(infs16_espace);
                    if (consumer_temps.LanguajeID == 1)
                    {
                        Chunk IC_L6_C4 = new Chunk(img_true, 0, 0);
                        InfoConsumer_l6.Add(IC_L6_C4);
                    }
                    else
                    {
                        Chunk IC_L6_C4 = new Chunk(img_false, 0, 0);
                        InfoConsumer_l6.Add(IC_L6_C4);
                    }


                    InfoConsumer_l6.Add(infs16_espace);
                    InfoConsumer_l6.Add(IC_L6_C5);
                    InfoConsumer_l6.Add(infs16_espace);

                    if (consumer_temps.LanguajeID == 2)
                    {
                        Chunk IC_L6_C6 = new Chunk(img_true, 0, 0);
                        InfoConsumer_l6.Add(IC_L6_C6);
                    }
                    else
                    {
                        Chunk IC_L6_C6 = new Chunk(img_false, 0, 0);
                        InfoConsumer_l6.Add(IC_L6_C6);
                    }


                    InfoConsumer_l6.Add(infs16_espace);
                    InfoConsumer_l6.Add(IC_L6_C7);
                    InfoConsumer_l6.Add(infs16_espace);
                    if (consumer_temps.LanguajeID == 6)
                    {
                        Chunk IC_L6_C8 = new Chunk(img_true, 0, 0);
                        InfoConsumer_l6.Add(IC_L6_C8);
                    }
                    else
                    {
                        Chunk IC_L6_C8 = new Chunk(img_false, 0, 0);
                        InfoConsumer_l6.Add(IC_L6_C8);
                    }

                    InfoConsumer_l6.Add(infs16_espace);
                    InfoConsumer_l6.Add(IC_L6_C9);
                    //InfoConsumer_l6.Add(IC_L6_C10);
                    InfoConsumer_l6.SpacingAfter = 5;
                    document.Add(InfoConsumer_l6);

                    Paragraph InfoService2 = new Paragraph();
                    InfoService2.TabSettings = new TabSettings(56f);
                    InfoService2.SpacingAfter = 8;




                    Paragraph InfoConsumer_l7 = new Paragraph();
                    InfoConsumer_l7.TabSettings = new TabSettings(56f);
                    Chunk IC_L7_C1 = new Chunk("Reason for Referral (Diagnosis):\n", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L7_C2 = new Chunk(consumer_temps.Reasonforreferral, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l7.Add(IC_L7_C1);
                    InfoConsumer_l7.Add(IC_L7_C2);
                    InfoConsumer_l7.SpacingAfter = 5;
                    document.Add(InfoConsumer_l7);


                    PdfPTable Authorization_table = new PdfPTable(1);
                    Authorization_table.WidthPercentage = 100;
                    Authorization_table.DefaultCell.Border = Rectangle.BOX;
                    PdfPTable innerTable2 = new PdfPTable(1);
                    innerTable2.DefaultCell.Border = Rectangle.BOX;
                    PdfPCell innerCell2 = new PdfPCell(new Phrase("AUTHORIZATION AND HOURS PER WEEK"));
                    innerCell2.HorizontalAlignment = Element.ALIGN_CENTER;
                    innerTable2.AddCell(innerCell2);
                    PdfPCell innerTableCell2 = new PdfPCell(innerTable2);
                    innerTableCell2.Padding = 0;
                    Authorization_table.AddCell(innerTableCell2);
                    document.Add(Authorization_table);

                    Paragraph InfoConsumer_l8 = new Paragraph();
                    InfoConsumer_l8.TabSettings = new TabSettings(56f);
                    Chunk IC_L8_C1 = new Chunk("Authorization From:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L8_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps._From), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L8_C4 = new Chunk("To:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L8_C5 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps._To), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L8_C7 = new Chunk("Authorization: ", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L8_C8 = new Chunk(consumer_temps.auth.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l8.Add(IC_L8_C1);
                    InfoConsumer_l8.Add(IC_L8_C2);
                    InfoConsumer_l8.Add(espacio);
                    InfoConsumer_l8.Add(IC_L8_C4);
                    InfoConsumer_l8.Add(IC_L8_C5);
                    InfoConsumer_l8.Add(espacio);
                    InfoConsumer_l8.Add(IC_L8_C7);
                    InfoConsumer_l8.Add(IC_L8_C8);
                    InfoConsumer_l8.SpacingAfter = 5;
                    document.Add(InfoConsumer_l8);


                    Paragraph InfoConsumer_l9 = new Paragraph();
                    InfoConsumer_l9.TabSettings = new TabSettings(56f);
                    Chunk IC_L9_C1 = new Chunk("Hours per Week:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L9_C2 = new Chunk(consumer_temps.HoursxWeek.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L9_C3 = new Chunk("Maximun hours per month:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L9_C4 = new Chunk(consumer_temps.MaxHours.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l9.Add(IC_L9_C1);
                    InfoConsumer_l9.Add(IC_L9_C2);
                    InfoConsumer_l9.Add(espacio);
                    InfoConsumer_l9.Add(IC_L9_C3);
                    InfoConsumer_l9.Add(IC_L9_C4);
                    InfoConsumer_l9.SpacingAfter = 5;
                    document.Add(InfoConsumer_l9);

                    Paragraph InfoConsumer_l10 = new Paragraph();
                    InfoConsumer_l10.TabSettings = new TabSettings(56f);
                    Chunk IC_L10_C1 = new Chunk("Termination date effective:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    //Chunk IC_L10_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.Terminationdateeffective), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L10_C3 = new Chunk("Additional Evel:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L10_C4 = new Chunk(consumer_temps.AdditionalEval, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l10.Add(IC_L10_C1);
                    //InfoConsumer_l10.Add(IC_L10_C2);
                    InfoConsumer_l10.Add(espacio);
                    InfoConsumer_l10.Add(IC_L10_C3);
                    InfoConsumer_l10.Add(IC_L10_C4);
                    InfoConsumer_l10.SpacingAfter = 5;
                    document.Add(InfoConsumer_l10);

                    PdfPTable services_table = new PdfPTable(1);
                    services_table.WidthPercentage = 100;
                    services_table.DefaultCell.Border = Rectangle.BOX;
                    PdfPTable innerTable3 = new PdfPTable(1);
                    innerTable3.DefaultCell.Border = Rectangle.BOX;
                    PdfPCell innerCell3 = new PdfPCell(new Phrase("TYPE OF SERVICES AND SPECIALIST ASSIGNED"));
                    innerCell3.HorizontalAlignment = Element.ALIGN_CENTER;
                    innerTable3.AddCell(innerCell3);
                    PdfPCell innerTableCell3 = new PdfPCell(innerTable3);
                    innerTableCell3.Padding = 0;
                    services_table.AddCell(innerTableCell3);
                    services_table.SpacingAfter = 5;
                    document.Add(services_table);

                    Paragraph InfoConsumer_l11 = new Paragraph();
                    InfoConsumer_l11.TabSettings = new TabSettings(56f);
                    Chunk IC_L11_C1 = new Chunk("InHome E.I:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L11_C3 = new Chunk("E.I. with OT/PT/SLP Consultation:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L11_C5 = new Chunk("OT/PT/SLP:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk infs11_espace = new Chunk("   ", FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l11.Add(IC_L11_C1);
                    InfoConsumer_l11.Add(infs11_espace);
                    if (consumer_temps.InHome == true)
                    {
                        Chunk IC_L11_C2 = new Chunk(img_true, 0, 0);
                        InfoConsumer_l6.Add(IC_L11_C2);
                    }
                    else
                    {
                        Chunk IC_L11_C2 = new Chunk(img_false, 0, 0);
                        InfoConsumer_l6.Add(IC_L11_C2);
                    }
                    InfoConsumer_l11.Add(espacio);
                    InfoConsumer_l11.Add(IC_L11_C3);
                    InfoConsumer_l11.Add(infs11_espace);
                    if (consumer_temps.EIWITH == true)
                    {
                        Chunk IC_L11_C4 = new Chunk(img_true, 0, 0);
                        InfoConsumer_l6.Add(IC_L11_C4);
                    }
                    else
                    {
                        Chunk IC_L11_C4 = new Chunk(img_false, 0, 0);
                        InfoConsumer_l6.Add(IC_L11_C4);
                    }
                    InfoConsumer_l11.Add(espacio);
                    InfoConsumer_l11.Add(IC_L11_C5);
                    InfoConsumer_l11.Add(infs11_espace);
                    if (consumer_temps.OTPT == true)
                    {
                        Chunk IC_L11_C6 = new Chunk(img_true, 0, 0);
                        InfoConsumer_l6.Add(IC_L11_C6);
                    }
                    else
                    {
                        Chunk IC_L11_C6 = new Chunk(img_false, 0, 0);
                        InfoConsumer_l6.Add(IC_L11_C6);
                    }
                    InfoConsumer_l11.SpacingAfter = 5;
                    document.Add(InfoConsumer_l11);

                    Paragraph InfoConsumer_l12 = new Paragraph();
                    InfoConsumer_l12.TabSettings = new TabSettings(56f);
                    Chunk IC_L12_C1 = new Chunk("CDS:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L12_C2 = new Chunk(consumer_temps.spe_FullName, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L12_C3 = new Chunk("Speciality:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L12_C4 = new Chunk(consumer_temps.specialist1.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L12_C5 = new Chunk("Therapist:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L12_C6 = new Chunk(consumer_temps.specialist2.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l12.Add(IC_L12_C1);
                    InfoConsumer_l12.Add(IC_L12_C2);
                    InfoConsumer_l12.Add(espacio);
                    InfoConsumer_l12.Add(IC_L12_C3);
                    InfoConsumer_l12.Add(IC_L12_C4);
                    InfoConsumer_l12.Add(espacio);
                    InfoConsumer_l12.Add(IC_L12_C5);
                    InfoConsumer_l12.Add(IC_L12_C6);
                    InfoConsumer_l12.SpacingAfter = 5;
                    document.Add(InfoConsumer_l12);

                    Paragraph InfoConsumer_l13 = new Paragraph();
                    InfoConsumer_l13.TabSettings = new TabSettings(56f);
                    Chunk IC_L13_C1 = new Chunk("Program Presenter:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L13_C2 = new Chunk(consumer_temps.PresenterName, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l13.Add(IC_L13_C1);
                    InfoConsumer_l13.Add(IC_L13_C2);
                    InfoConsumer_l13.SpacingAfter = 5;
                    document.Add(InfoConsumer_l13);


                    Paragraph InfoConsumer_l14 = new Paragraph();
                    InfoConsumer_l14.TabSettings = new TabSettings(56f);
                    Chunk IC_L14_C1 = new Chunk("Notes:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L14_C2 = new Chunk(consumer_temps.NotesConsumer, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l14.Add(IC_L14_C1);
                    InfoConsumer_l14.Add(IC_L14_C2);
                    InfoConsumer_l14.SpacingAfter = 5;
                    document.Add(InfoConsumer_l14);

                    PdfPTable regiona_table = new PdfPTable(1);
                    regiona_table.WidthPercentage = 100;
                    regiona_table.DefaultCell.Border = Rectangle.BOX;
                    PdfPTable innerTable4 = new PdfPTable(1);
                    innerTable4.DefaultCell.Border = Rectangle.BOX;
                    PdfPCell innerCell4 = new PdfPCell(new Phrase("REGIONAL CENTER INFORMATION"));
                    innerCell4.HorizontalAlignment = Element.ALIGN_CENTER;
                    innerTable4.AddCell(innerCell4);
                    PdfPCell innerTableCell4 = new PdfPCell(innerTable4);
                    innerTableCell4.Padding = 0;
                    regiona_table.AddCell(innerTableCell4);
                    document.Add(regiona_table);


                    Paragraph InfoConsumer_l15 = new Paragraph();
                    InfoConsumer_l15.TabSettings = new TabSettings(56f);
                    Chunk IC_L15_C1 = new Chunk("Regional Center:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L15_C2 = new Chunk(consumer_temps.RegionalCenter, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L15_C3 = new Chunk("Telephone #:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L15_C4 = new Chunk(consumer_temps.rc_Phone, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l15.Add(IC_L15_C1);
                    InfoConsumer_l15.Add(IC_L15_C2);
                    InfoConsumer_l15.Add(espacio);
                    InfoConsumer_l15.Add(IC_L15_C3);
                    InfoConsumer_l15.Add(IC_L15_C4);
                    InfoConsumer_l15.SpacingAfter = 5;
                    document.Add(InfoConsumer_l15);

                    Paragraph InfoConsumer_l16 = new Paragraph();
                    InfoConsumer_l16.TabSettings = new TabSettings(56f);
                    Chunk IC_L16_C1 = new Chunk("Address:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L16_C2 = new Chunk(consumer_temps.rc_Address, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l16.Add(IC_L16_C1);
                    InfoConsumer_l16.Add(IC_L16_C2);
                    InfoConsumer_l16.SpacingAfter = 5;
                    document.Add(InfoConsumer_l16);

                    Paragraph InfoConsumer_l17 = new Paragraph();
                    InfoConsumer_l17.TabSettings = new TabSettings(56f);
                    Chunk IC_L17_C1 = new Chunk("Service Coordinato's name:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L17_C2 = new Chunk(consumer_temps.ServiceCoordinator, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L17_C3 = new Chunk("Extension #:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L17_C4 = new Chunk(consumer_temps.Ext, FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l17.Add(IC_L17_C1);
                    InfoConsumer_l17.Add(IC_L17_C2);
                    InfoConsumer_l17.Add(espacio);
                    InfoConsumer_l17.Add(IC_L17_C3);
                    InfoConsumer_l17.Add(IC_L17_C4);
                    InfoConsumer_l17.SpacingAfter = 5;
                    document.Add(InfoConsumer_l17);

                    PdfPTable report_table = new PdfPTable(1);
                    report_table.WidthPercentage = 100;
                    report_table.DefaultCell.Border = Rectangle.BOX;
                    PdfPTable innerTable5 = new PdfPTable(1);
                    innerTable5.DefaultCell.Border = Rectangle.BOX;
                    PdfPCell innerCell5 = new PdfPCell(new Phrase("REPORT DUE DATES"));
                    innerCell5.HorizontalAlignment = Element.ALIGN_CENTER;
                    innerTable5.AddCell(innerCell5);
                    PdfPCell innerTableCell5 = new PdfPCell(innerTable5);
                    innerTableCell5.Padding = 0;
                    report_table.AddCell(innerTableCell5);
                    document.Add(report_table);

                    Paragraph InfoConsumer_l18 = new Paragraph();
                    InfoConsumer_l18.TabSettings = new TabSettings(56f);
                    Chunk IC_L18_C1 = new Chunk("Initial Evaluation:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L18_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.initEval), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L18_C3 = new Chunk("Initial Evaluation due by:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L18_C4 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.evaldueby), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l18.Add(IC_L18_C1);
                    InfoConsumer_l18.Add(IC_L18_C2);
                    InfoConsumer_l18.Add(espacio);
                    InfoConsumer_l18.Add(IC_L18_C3);
                    InfoConsumer_l18.Add(IC_L18_C4);
                    InfoConsumer_l18.SpacingAfter = 5;
                    document.Add(InfoConsumer_l18);

                    Paragraph InfoConsumer_l19 = new Paragraph();
                    InfoConsumer_l19.TabSettings = new TabSettings(56f);
                    Chunk IC_L19_C1 = new Chunk("1st Progress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L19_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.Report1), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L19_C3 = new Chunk("4thProgress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L19_C4 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.Report4), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l19.Add(IC_L19_C1);
                    InfoConsumer_l19.Add(IC_L19_C2);
                    InfoConsumer_l19.Add(espacio);
                    InfoConsumer_l19.Add(IC_L19_C3);
                    InfoConsumer_l19.Add(IC_L19_C4);
                    InfoConsumer_l19.SpacingAfter = 5;
                    document.Add(InfoConsumer_l19);

                    Paragraph InfoConsumer_l20 = new Paragraph();
                    InfoConsumer_l20.TabSettings = new TabSettings(56f);
                    Chunk IC_L20_C1 = new Chunk("2nd Progress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L20_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.Report2), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l20.Add(IC_L20_C1);
                    InfoConsumer_l20.Add(IC_L20_C2);
                    InfoConsumer_l20.SpacingAfter = 5;
                    document.Add(InfoConsumer_l20);

                    Paragraph InfoConsumer_l21 = new Paragraph();
                    InfoConsumer_l21.TabSettings = new TabSettings(56f);
                    Chunk IC_L21_C1 = new Chunk("3rd Progress Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L21_C2 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.Report3), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    Chunk IC_L21_C3 = new Chunk("Closing Report:", FontFactory.GetFont(FontFactory.HELVETICA, 9, Font.BOLD));
                    Chunk IC_L21_C4 = new Chunk(string.Format("{0:MM/dd/yyyy}", consumer_temps.ReportClose), FontFactory.GetFont(FontFactory.HELVETICA, 9));
                    InfoConsumer_l21.Add(IC_L21_C1);
                    InfoConsumer_l21.Add(IC_L21_C2);
                    InfoConsumer_l21.Add(espacio);
                    InfoConsumer_l21.Add(IC_L21_C3);
                    InfoConsumer_l21.Add(IC_L21_C4);
                    InfoConsumer_l21.SpacingAfter = 5;
                    document.Add(InfoConsumer_l21);
                    document.Close();
                }
                workStream.Position = 0;
                return new FileStreamResult(workStream, "application/pdf");
            }
            catch (Exception _error)
            {
                workStream.Position = 0;
                return new FileStreamResult(workStream, "application/pdf");
            }


        }



        public FileStreamResult NotesConsumer_Report(string _uci)
        {
            List<ReportsModel> consumerList = new List<ReportsModel>();
            List<ReportsModel> NotesconsumerList = new List<ReportsModel>();
            consumerList = _ReportsModel.GetList_Reports_RPTNotesConsumer(_uci);
            NotesconsumerList = _ReportsModel.Get_NotesXConsumer(_uci);
            //var _consumerList = _ReportsModel.Get_Reports_RPTConsumer(UCI);
            MemoryStream workStream = new MemoryStream();
            try
            {
                foreach (var consumer_temps in consumerList)
                {
                    // TimesxWeek/PresentInSession/TotalHours vienen de las visitas
                    // reales (Get_NotesXConsumer -> leap_api ReportController::notesByConsumer),
                    // no de consumerList (Get_Reports_RPTConsumer, que no trae esos
                    // datos) - antes TIMES/WK y PARENT/GUARDIAN salian siempre en
                    // blanco y TOTAL OF HOURS estaba hardcodeado a "Total Hours".
                    // LastOrDefault, no FirstOrDefault: Get_NotesXConsumer viene
                    // ordenado por session_started_at ascendente, y para
                    // TimesxWeek/PresentInSession queremos el dato mas reciente
                    // que anoto el CDS, no el de la primera visita historica.
                    var latestNote = NotesconsumerList.LastOrDefault();

                    string logo_image = Server.MapPath("~/Content/Images/logo.png");
                    string true_image = Server.MapPath("~/Content/Images/true.png");
                    string salse_image = Server.MapPath("~/Content/Images/false.png");
                    iTextSharp.text.Image img_logo = iTextSharp.text.Image.GetInstance(logo_image);
                    iTextSharp.text.Image img_true = iTextSharp.text.Image.GetInstance(true_image);
                    iTextSharp.text.Image img_false = iTextSharp.text.Image.GetInstance(salse_image);

                    Document document = new Document(PageSize.A4, 50, 50, 10, 50);
                    PdfWriter writer = PdfWriter.GetInstance(document, workStream);
                    writer.CloseStream = false;
                    document.Open();

                    // Fuentes y estilos
                    Font titulo_f = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 30, Font.BOLD);
                    Font subtitulo_f = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.BOLD);
                    Font subtitulo2_f = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD);
                    Font consumer_f = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14);
                    Font base_f = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                    Font check_f = FontFactory.GetFont(FontFactory.HELVETICA, 9);

                    // Agregar el logo
                    Image logo = Image.GetInstance(img_logo);
                    logo.ScaleAbsolute(90f, 45f);
                    logo.SetAbsolutePosition(30, (document.PageSize.Height - logo.ScaledHeight) - 30);
                    document.Add(logo);

                    PdfContentByte cb1 = writer.DirectContent;
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    cb1.SetFontAndSize(bf, 12);

                    // Posiciones base
                    float textPositionX = document.PageSize.Width / 2 - 100;
                    float textPositionY = document.PageSize.Height - 80;

                    // Escribir "Consumer Name: " y subrayar el nombre
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "CONSUMER NAME: ", textPositionX - 10, textPositionY, 0);
                    cb1.EndText();
                    float consumerNamePositionX = textPositionX + bf.GetWidthPoint("CONSUMER NAME: ", 12);
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, consumer_temps.ConsumerName, consumerNamePositionX - 10, textPositionY, 0); // Cambia "John Doe" por el valor dinámico
                    cb1.EndText();
                    float underlineWidthConsumer = bf.GetWidthPoint(consumer_temps.ConsumerName, 12);
                    cb1.MoveTo(consumerNamePositionX - 10, textPositionY - 2);
                    cb1.LineTo((consumerNamePositionX - 10) + underlineWidthConsumer, textPositionY - 2);
                    cb1.Stroke();

                    // Mover la posición Y para el siguiente texto
                    textPositionY -= 20;

                    // Escribir "Regional Center: " y subrayar el nombre del centro regional
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "REGIONAL CENTER:", textPositionX - 70, textPositionY, 0);
                    cb1.EndText();
                    float regionalCenterPositionX = textPositionX + bf.GetWidthPoint("REGIONAL CENTER:", 12);
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_CENTER, consumer_temps.RegionalCenter, regionalCenterPositionX - 20, textPositionY, 0); // Cambia "Regional Center Name" por el valor dinámico
                    cb1.EndText();
                    float underlineWidthRegional = bf.GetWidthPoint(consumer_temps.RegionalCenter, 12);
                    cb1.MoveTo(regionalCenterPositionX - 60, textPositionY - 2);
                    cb1.LineTo((regionalCenterPositionX - 60) + underlineWidthRegional, textPositionY - 2);
                    cb1.Stroke();

                    // Escribir "Times/WK: " y subrayar el número de veces por semana
                    float timesWKPositionX = regionalCenterPositionX + underlineWidthRegional + 20;
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "TIMES/WK: ", timesWKPositionX, textPositionY, 0);
                    cb1.EndText();
                    float timesPositionX = timesWKPositionX + bf.GetWidthPoint("TIMES/WK: ", 12);
                    string timesxWeek = latestNote?.TimesxWeek ?? "";
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, timesxWeek, timesPositionX, textPositionY, 0); // Cambia "3" por el valor dinámico
                    cb1.EndText();
                    float underlineWidthTimes = bf.GetWidthPoint(timesxWeek, 12);
                    cb1.MoveTo(timesPositionX, textPositionY - 2);
                    cb1.LineTo(timesPositionX + underlineWidthTimes, textPositionY - 2);
                    cb1.Stroke();

                    // Mover la posición Y para el siguiente texto
                    textPositionY -= 20;

                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "SESSION LOG ", textPositionX + 50, textPositionY, 0);
                    cb1.EndText();
                    cb1.Stroke();


                    // Crear la tabla
                    PdfPTable table = new PdfPTable(4);

                    // Establecer anchos de columna (opcional)
                    float[] columnWidths = new float[] { 2f, 2f, 4f, 2f };
                    table.SetWidths(columnWidths);

                    // Agregar los títulos de la primera fila
                    PdfPCell cell1 = new PdfPCell(new Phrase("DATE OF SERVICE"));
                    cell1.BackgroundColor = new BaseColor(192, 192, 192); // Color gris claro

                    PdfPCell cell2 = new PdfPCell(new Phrase("TIME OF SESSION"));
                    cell2.BackgroundColor = new BaseColor(192, 192, 192);

                    PdfPCell cell3 = new PdfPCell(new Phrase("PRESENT DURING SESSION"));
                    cell3.BackgroundColor = new BaseColor(192, 192, 192);

                    PdfPCell cell4 = new PdfPCell(new Phrase("LENGTH OF SESSION"));
                    cell4.BackgroundColor = new BaseColor(192, 192, 192);

                    // Agregar las celdas a la tabla
                    table.AddCell(cell1);
                    table.AddCell(cell2);
                    table.AddCell(cell3);
                    table.AddCell(cell4);
                    // Llenar las demás filas dinámicamente
                    int _cantidadNotes = NotesconsumerList.Count;
                    int _cantidadFor = 0;
                    foreach (var _NotesC in NotesconsumerList)
                    {
                        if (_NotesC.spe_FullName != "")
                        {
                            table.AddCell(_NotesC.Date.ToString("MM-dd-yyyy"));
                            table.AddCell(_NotesC.Date.ToString("HH:mm"));
                            table.AddCell(_NotesC.PresentInSession);
                            table.AddCell(_NotesC.Duration);
                            _cantidadFor++;
                        }

                    }

                    Paragraph transparentParagraph = new Paragraph(" ");
                    transparentParagraph.SpacingBefore = 120f;
                    transparentParagraph.Font.Color = BaseColor.WHITE; // Esto lo hace invisible

                    // Agregar el bloque de texto al documento
                    document.Add(transparentParagraph);
                    table.WidthPercentage = 95;
                    // Agregar la tabla al documento
                    document.Add(table);

                    Paragraph titulo_t = new Paragraph("      SESSION NOTES", base_f);
                    titulo_t.Alignment = Element.ALIGN_LEFT;
                    document.Add(titulo_t);


                    // Crear la tabla
                    PdfPTable table2 = new PdfPTable(1);
                    PdfPCell cell11 = new PdfPCell(new Phrase(""));
                    // Establecer anchos de columna (opcional)
                    float[] columnWidths2 = new float[] { 2f };
                    table2.SetWidths(columnWidths2);
                    // Agregar las celdas a la tabla
                    table2.AddCell(cell11);


                    foreach (var _NotesC in NotesconsumerList)
                    {
                        if (_NotesC.spe_FullName != "")
                        {
                            table2.AddCell(_NotesC.Date.ToString() + " " + _NotesC.NotesConsumer);
                        }

                    }

                    Paragraph transparentParagraph2 = new Paragraph(" ");
                    transparentParagraph2.SpacingBefore = 5f;
                    transparentParagraph2.Font.Color = BaseColor.WHITE; // Esto lo hace invisible

                    // Agregar el bloque de texto al documento
                    document.Add(transparentParagraph2);
                    table2.WidthPercentage = 95;
                    // Agregar la tabla al documento
                    document.Add(table2);


                    //Paragraph titulo_t = new Paragraph("CHILD DEVELOPMENT SPECIALIST NAME:", base_f);
                    //titulo_t.Alignment = Element.ALIGN_LEFT;
                    //document.Add(titulo_t);

                    // Mover la posición Y para el siguiente bloque de texto debajo de la última tabla
                    float textPositionY_2 = 100; // Ajusta este valor según la posición deseada

                    // Primera línea: "TITLE 1: " y subrayar el valor
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "CHILD DEVELOPMENT SPECIALIST NAME:", 50, textPositionY_2, 0);
                    cb1.EndText();
                    float title1PositionX = 95 + bf.GetWidthPoint("CHILD DEVELOPMENT SPECIALIST NAME:", 10);
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, consumer_temps.spe_FullName, title1PositionX, textPositionY_2, 0); // Cambia "Valor 1" por el valor dinámico
                    cb1.EndText();
                    float underlineWidth1 = bf.GetWidthPoint(consumer_temps.spe_FullName, 10);
                    cb1.MoveTo(title1PositionX, textPositionY_2 - 2);
                    cb1.LineTo(title1PositionX + underlineWidth1, textPositionY_2 - 2);
                    cb1.Stroke();

                    // Mover la posición Y para la siguiente línea
                    textPositionY_2 -= 20;

                    // Segunda línea: "TITLE 2: " y subrayar el valor
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "CHILD DEVELOPMENT SPECIALIST SIGNATURE: ", 50, textPositionY_2, 0);
                    cb1.EndText();
                    float title2PositionX = 95 + bf.GetWidthPoint("CHILD DEVELOPMENT SPECIALIST SIGNATURE: ", 10);
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, consumer_temps.spe_FullName, title2PositionX, textPositionY_2, 0); // Cambia "Valor 2" por el valor dinámico
                    cb1.EndText();
                    float underlineWidth2 = bf.GetWidthPoint(consumer_temps.spe_FullName, 10);
                    cb1.MoveTo(title2PositionX, textPositionY_2 - 2);
                    cb1.LineTo(title2PositionX + underlineWidth2, textPositionY_2 - 2);
                    cb1.Stroke();

                    textPositionY_2 -= 20;

                    string guardianName = latestNote?.PresentInSession ?? "";
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "PARENT/GUARDIAN NAME: ", 50, textPositionY_2, 0);
                    cb1.EndText();
                    float title3PositionX = 75 + bf.GetWidthPoint("PARENT/GUARDIAN NAME: ", 10);
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, guardianName, title3PositionX, textPositionY_2, 0);
                    cb1.EndText();
                    float underlineWidth3 = bf.GetWidthPoint(guardianName, 10);
                    cb1.MoveTo(title3PositionX, textPositionY_2 - 2);
                    cb1.LineTo(title3PositionX + underlineWidth3, textPositionY_2 - 2);
                    cb1.Stroke();

                    // Agregar espacio para el cuarto título y valor (TITLE 4)
                    float title4PositionX = title3PositionX + underlineWidth3 + 50; // Ajusta este valor según el espacio deseado

                    string totalHours = latestNote?.TotalHours ?? "";
                    // Cuarta línea: "TITLE 4: " y subrayar el valor
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, "TOTAL OF HOURS: ", title4PositionX, textPositionY_2, 0);
                    cb1.EndText();
                    float title4ValuePositionX = title4PositionX + 35 + bf.GetWidthPoint("TOTAL OF HOURS: ", 8);
                    cb1.BeginText();
                    cb1.ShowTextAligned(Element.ALIGN_LEFT, totalHours, title4ValuePositionX, textPositionY_2, 0);
                    cb1.EndText();
                    float underlineWidth4 = bf.GetWidthPoint(totalHours, 12);
                    cb1.MoveTo(title4ValuePositionX, textPositionY_2 - 2);
                    cb1.LineTo(title4ValuePositionX + underlineWidth4, textPositionY_2 - 2);
                    cb1.Stroke();





                    document.Close();
                }
                workStream.Position = 0;
                return new FileStreamResult(workStream, "application/pdf");
            }
            catch (Exception _error)
            {
                workStream.Position = 0;
                return new FileStreamResult(workStream, "application/pdf");
            }


        }
        public FileResult ConsumerReport(string UCI)
        {
            FileResult result = null;
            try
            {
                string _dActual = DateTime.Now.ToLongDateString();
                var filePath2 = Server.MapPath("~/Content/Template/rpt_consumer.html");
                //string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _pathUserBase = Server.MapPath("~/Content/Template/user_base.png");
                string _pathLogo = Server.MapPath("~/Content/Template/azul.png");
                string _pathLogoAzul = Server.MapPath("~/Content/Template/azul.png");
                string _pathLogoVerde = Server.MapPath("~/Content/Template/verde.png");
                string true_image = Server.MapPath("~/Content/Images/true.png");
                string false_image = Server.MapPath("~/Content/Images/false.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);
                List<ReportsModel> _ReportConsumer = _ReportsModel.Get_Reports_RPTConsumerList(UCI);
               
                //_htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));
                string NameConsumer = "";
                string _Programa = "";
                foreach (var _Consumer in _ReportConsumer)
                {
                    
                    if (_Consumer.Date.ToString("MM-dd-yyyy") == "01-01-1900")
                    {
                        _htmlString = _htmlString.Replace("[[Date]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[Date]]", _Consumer.Date.ToString("MM-dd-yyyy"));
                    }
                    _htmlString = _htmlString.Replace("[[TakenBy]]",_Consumer.UserC.ToString());


                    _htmlString = _htmlString.Replace("[[ConsumerName]]", _Consumer.ConsumerName);
                    _htmlString = _htmlString.Replace("[[UCIN]]", _Consumer.UCI);
                    _htmlString = _htmlString.Replace("[[DOB]]", _Consumer.DateOfBirth.ToString("MM-dd-yyyy"));
                    _htmlString = _htmlString.Replace("[[Age]]", CalcularEdad(_Consumer.DateOfBirth, DateTime.Now));

                    if (string.IsNullOrEmpty(_Consumer.email))
                    {
                        _htmlString = _htmlString.Replace("[[Email]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[Email]]", _Consumer.email);
                    }
                 
                    if (_Consumer.adjage.ToString("MM-dd-yyyy") == "01-01-1900")
                    {
                        _htmlString = _htmlString.Replace("[[AdjAge]]", "");
                    }
                    else
                    {
                       // _htmlString = _htmlString.Replace("[[4thProgress]]", _Consumer.adjage.ToString("MM-dd-yyyy"));
                    }
                    //1
                    if (_Consumer.Action.ToString() == "1")
                    {
                        _htmlString = _htmlString.Replace("[[loguito]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[loguito]]", false_image);
                    }

                    //2
                    if (_Consumer.Action.ToString() == "2")
                    {
                        _htmlString = _htmlString.Replace("[[loguito2]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[loguito2]]", false_image);
                    }

                    //3
                    if (_Consumer.Action.ToString() == "3")
                    {
                        _htmlString = _htmlString.Replace("[[loguito3]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[loguito3]]", false_image);
                    }

                    //4
                    if (_Consumer.Action.ToString() == "4")
                    {
                        _htmlString = _htmlString.Replace("[[loguito4]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[loguito4]]", false_image);
                    }

                    //5
                    if (_Consumer.Action.ToString() == "5")
                    {
                        _htmlString = _htmlString.Replace("[[loguito5]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[loguito5]]", false_image);
                    }



                    if (_Consumer.Gender == "m")
                    {
                        _htmlString = _htmlString.Replace("[[Male]]", true_image);
                        _htmlString = _htmlString.Replace("[[Female]]", false_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[Male]]", false_image);
                        _htmlString = _htmlString.Replace("[[Female]]", true_image);
                    }

                    var ciudad = _CitiesModel.Get_Cities_ByID(Convert.ToInt32(_Consumer.CityID));
                    _htmlString = _htmlString.Replace("[[Teleserv]]", _Consumer.tel.ToString());
                    _htmlString = _htmlString.Replace("[[CAddres]]", _Consumer.Address.ToString());
                    var getCity = ciudad._ErrorCode ? "" : ciudad.City;

                    _htmlString = _htmlString.Replace("[[CCity]]", getCity.ToString());
                    _htmlString = _htmlString.Replace("[[CSt]]", _Consumer.State.ToString());
                    _htmlString = _htmlString.Replace("[[CZipCode]]", _Consumer.ZipCode.ToString());
                    string NEW_PHONE = FormatPhoneNumber(_Consumer.Phone.ToString());
                    _htmlString = _htmlString.Replace("[[CTelephone]]", NEW_PHONE);
                    string NEW_PHONE2 = FormatPhoneNumber(_Consumer.EmergencyPhone.ToString());
                    _htmlString = _htmlString.Replace("[[CETelephono]]", _Consumer.EmergencyPhone.ToString());
                    _htmlString = _htmlString.Replace("[[ParentName]]", _Consumer.ParentFullName.ToString());
                    if (_Consumer.LanguajeName == "English")
                    {
                        _htmlString = _htmlString.Replace("[[English]]", true_image);
                        _htmlString = _htmlString.Replace("[[Spanish]]", false_image);
                        _htmlString = _htmlString.Replace("[[Bilinigual]]", false_image);
                        _htmlString = _htmlString.Replace("[[Other]]", "");
                    }
                    else if (_Consumer.LanguajeName == "Spanish")
                    {
                        _htmlString = _htmlString.Replace("[[English]]", false_image);
                        _htmlString = _htmlString.Replace("[[Spanish]]", true_image);
                        _htmlString = _htmlString.Replace("[[Bilingual]]", false_image);
                        _htmlString = _htmlString.Replace("[[Other]]", "");
                    }
                    else if (_Consumer.LanguajeName == "Bilingual")
                    {
                        _htmlString = _htmlString.Replace("[[English]]", false_image);
                        _htmlString = _htmlString.Replace("[[Spanish]]", false_image);
                        _htmlString = _htmlString.Replace("[[Bilingual]]", true_image);
                        _htmlString = _htmlString.Replace("[[Other]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[English]]", false_image);
                        _htmlString = _htmlString.Replace("[[Spanish]]", false_image);
                        _htmlString = _htmlString.Replace("[[Bilingual]]", false_image);
                        _htmlString = _htmlString.Replace("[[Other]]", _Consumer.LanguajeName);
                    }
                    _htmlString = _htmlString.Replace("[[Diagnosis]]", _Consumer.Reasonforreferral.ToString());
                 
                    //from
                    if (_Consumer._From.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer._From.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[AuthorizationF]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[AuthorizationF]]", _Consumer._From.ToString("MM-dd-yyyy"));
                    }

                    //to
                    if (_Consumer._To.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer._To.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[AutTo]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[AutTo]]", _Consumer._To.ToString("MM-dd-yyyy"));
                    }
                    //terminationDate
                    if (_Consumer.TerminationDateEffective.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.TerminationDateEffective.ToString("MM-dd-yyyy") == "01-01-1901" || _Consumer.TerminationDateEffective.ToString("MM-dd-yyyy") == "01-01-0001")
                    {
                        _htmlString = _htmlString.Replace("[[TermEfect]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[TermEfect]]", _Consumer.TerminationDateEffective.ToString("MM-dd-yyyy"));
                    }
                    _htmlString = _htmlString.Replace("[[AuthorizationN]]", _Consumer.auth.ToString());
                   
                    if (_Consumer.MaxHours.ToString() == "0")
                    {
                        _htmlString = _htmlString.Replace("[[MaxMonth]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[MaxMonth]]", _Consumer.MaxHours.ToString());
                    }
                    if (_Consumer.HoursxWeek.ToString() == "0")
                    {
                        _htmlString = _htmlString.Replace("[[HoursWk]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[HoursWk]]", _Consumer.HoursxWeek.ToString());
                    }
                   
                    
                    _htmlString = _htmlString.Replace("[[AddiEval]]", _Consumer.AdditionalEval.ToString());
                    //InHome
                    if (_Consumer.InHome.ToString()=="True")
                    {
                        _htmlString = _htmlString.Replace("[[InHome]]", true_image);
                    }
                    else
                    {

                        _htmlString = _htmlString.Replace("[[InHome]]", false_image);

                    }

                    if (_Consumer.EIWITH.ToString() == "True")
                    {
                        _htmlString = _htmlString.Replace("[[WOT]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[WOT]]", false_image);
                    }
                    if (_Consumer.PEP.ToString() == "True")
                    {
                        _htmlString = _htmlString.Replace("[[PEP]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[PEP]]", false_image);
                    }

                    if (_Consumer.CB.ToString() == "True")
                    {
                        _htmlString = _htmlString.Replace("[[CB]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[CB]]", false_image);
                    }

                    if (_Consumer.OTPT.ToString() == "True")
                    {
                        _htmlString = _htmlString.Replace("[[OT]]", true_image);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[OT]]", false_image);
                    }
                 
                    _htmlString = _htmlString.Replace("[[CDS]]", _Consumer.CDS_name.ToString());
                    //if (_Consumer.specialist2.ToString()=="0")
                    //{
                    //    _htmlString = _htmlString.Replace("[[Specialty]]", "");
                    //}
                    //else
                    //{
                    //    _htmlString = _htmlString.Replace("[[Specialty]]", _Consumer.Specialty_name.ToString());
                    //}
                    _htmlString = _htmlString.Replace("[[Specialty]]", _Consumer.Specialty_name.ToString());

                    //if (_Consumer.specialist3.ToString() == "0")
                    //{
                    //    _htmlString = _htmlString.Replace("[[Therapist]]", "");
                    //}
                    //else
                    //{
                    //    _htmlString = _htmlString.Replace("[[Therapist]]", _Consumer.Therapist_name.ToString());
                    //}
                    _htmlString = _htmlString.Replace("[[Therapist]]", _Consumer.Therapist_name.ToString());

                    //_htmlString = _htmlString.Replace("[[CPresenter]]", _Consumer.PresenterName.ToString());
                    _htmlString = _htmlString.Replace("[[CPresenter]]", _Consumer.PresenterName);
                    _htmlString = _htmlString.Replace("[[Notes]]", _Consumer.NotesConsumer.ToString());
                    _htmlString = _htmlString.Replace("[[RegionalCenter]]", _Consumer.RegionalCenter.ToString());
                    _htmlString = _htmlString.Replace("[[RCTelephone]]", _Consumer.rc_Phone.ToString());
                    _htmlString = _htmlString.Replace("[[RCAddress]]", _Consumer.rc_Address.ToString());
                    //_htmlString = _htmlString.Replace("[[ServiceCoordinator]]", _Consumer.ServiceCoordinator.ToString());
                    _htmlString = _htmlString.Replace("[[ServiceCoordinator]]", _Consumer.ServiceCoordinator.ToString());
                  
                    _htmlString = _htmlString.Replace("[[Extension]]", _Consumer.Ext.ToString());
                    if (_Consumer.initEval.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.initEval.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[InitialEvaluation]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[InitialEvaluation]]", _Consumer.initEval.ToString("MM-dd-yyyy"));
                    }

                    if (_Consumer.evaldueby.ToString("MM-dd-yyyy")== "01-01-1900" || _Consumer.evaldueby.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[InitialEvaluationDB]]","" );
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[InitialEvaluationDB]]", _Consumer.evaldueby.ToString("MM-dd-yyyy"));
                    }

                    if (_Consumer.Report1.ToString("MM-dd-yyyy")== "01-01-1900" || _Consumer.Report1.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[1stProgress]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[1stProgress]]", _Consumer.Report1.ToString("MM-dd-yyyy"));
                    }
                    //progreso 2
                    if (_Consumer.Report2.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.Report2.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[2ndProgress]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[2ndProgress]]", _Consumer.Report2.ToString("MM-dd-yyyy"));
                    }

                    //progreso 3

                    if (_Consumer.Report3.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.Report3.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[3rdProgress]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[3rdProgress]]", _Consumer.Report3.ToString("MM-dd-yyyy"));
                    }

                    // 4st report
                    if (_Consumer.Report4.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.Report4.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[4thProgress]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[4thProgress]]", _Consumer.Report4.ToString("MM-dd-yyyy"));
                    }
                    // 5th report
                    if (_Consumer.Report5.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.Report5.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[5thProgress]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[5thProgress]]", _Consumer.Report5.ToString("MM-dd-yyyy"));
                    }


                    if (_Consumer.ReportClose.ToString("MM-dd-yyyy") == "01-01-1900" || _Consumer.ReportClose.ToString("MM-dd-yyyy") == "01-01-1901")
                    {
                        _htmlString = _htmlString.Replace("[[ClosingReport]]", "");
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[ClosingReport]]", _Consumer.ReportClose.ToString("MM-dd-yyyy"));
                    }

                    NameConsumer = _Consumer.ConsumerName;
                    if (_Consumer.c_image != "")
                    {
                        byte[] imageBytes = Convert.FromBase64String(_Consumer.c_image);
                        string base64ImageUri = "data:image/png;base64," + _Consumer.c_image;
                        _htmlString = _htmlString.Replace("[[ConsumerImage]]", base64ImageUri);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[ConsumerImage]]", _pathUserBase);
                    }

                    if (_Consumer.Type.ToLower().Equals("leap"))
                    {
                        /* if (_Consumer.CB == true)
                         {
                             _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoAzul);
                             _htmlString = _htmlString.Replace("[[Logo2]]", _pathLogoVerde);
                         }
                         else
                         {
                             _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoAzul);
                             _htmlString = _htmlString.Replace("[[Logo2]]", "");
                         }*/
                        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoAzul);

                    }
                    else if (_Consumer.Type.ToLower().Equals("cent"))
                    {
                        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoVerde);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                    }
                    
                }
                NameConsumer.Replace(" ", "_");


                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Portrait;
                int webPageWidth = 1024;
                int webPageHeight = 0;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(_htmlString, "");
                foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                {
                    doc.AddPage(page);
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_Consumer_" + NameConsumer + ".pdf";
                    //result.FileDownloadName = "Rpt_NotesConsumer_" + NameConsumer + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }
        public string FormatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length != 10)
            {
                return phoneNumber;
            }
            else
            {
                return $"({phoneNumber.Substring(0, 3)}) {phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 4)}";
            }

        }
        //public string edad(DateTime dateOfBirth)
        //{
        //    DateTime birth = dateOfBirth;
        //    DateTime today = DateTime.Now;
        //    TimeSpan span = today - birth;
        //    DateTime age = DateTime.MinValue + span;

        //    // Make adjustment due to MinValue equalling 1/1/1int years = age.Year - 1;
        //    int months = age.Month - 1;
        //    var meses = (age.Year - 1) * 12;
        //    var final = Convert.ToInt32(months) + Convert.ToInt32(meses);
        //    int days = age.Day - 2;

        //    // Print out not only how many years old they are but give months and days as well
        //    var ageInYMD = final + " months";
        //    var ageInYM = string.Format("{0} years, {1} months", age.Year, months);
        //    var ageInY = string.Format("{0} years", age.Year);

        //    return ageInYMD;
        //}
        //public string CalcularEdad(DateTime fechaNacimiento, DateTime fechaActual)
        //{
        //    int años = fechaActual.Year - fechaNacimiento.Year;
        //    int meses = (fechaActual.Month - fechaNacimiento.Month + 12) % 12;
        //    int dias = fechaActual.Day - fechaNacimiento.Day;

        //    if (dias < 0)
        //    {
        //        meses--;
        //        dias += DateTime.DaysInMonth(fechaActual.Year, fechaActual.Month - 1);
        //    }

        //    if (meses < 0)
        //    {
        //        años--;
        //        meses += 12;
        //    }
        //    if (años <= 0)
        //    {
        //        años = 0;
        //    }
        //    var FechaFinal = string.Format("{0} years, {1} months,  {2} days", años, meses, dias);
        //    return FechaFinal;
        //}

        public string CalcularEdad(DateTime fechaNacimiento, DateTime fechaActual)
        {
            int años = fechaActual.Year - fechaNacimiento.Year;
            int meses = fechaActual.Month - fechaNacimiento.Month;
            int dias = fechaActual.Day - fechaNacimiento.Day;

            if (dias < 0)
            {
                meses--;

                int mesAnterior = fechaActual.Month - 1;
                int añoMesAnterior = fechaActual.Year;

                if (mesAnterior == 0)
                {
                    mesAnterior = 12;
                    añoMesAnterior--;
                }

                dias += DateTime.DaysInMonth(añoMesAnterior, mesAnterior);
            }

            if (meses < 0)
            {
                años--;
                meses += 12;
            }

            if (años < 0)
                años = 0;

            return $"{años} years, {meses} months, {dias} days";
        }

        //public string edad(DateTime dateOfBirth)
        //{
        //    DateTime birth = dateOfBirth;
        //    DateTime today = DateTime.Now;
        //    TimeSpan span = today - birth;
        //    DateTime age = DateTime.MinValue + span;

        //    // Make adjustment due to MinValue equalling 1/1/1int years = age.Year - 1;
        //    int months = age.Month - 1;
        //    var meses = (age.Year - 1) * 12;
        //    var final = Convert.ToInt32(months) + Convert.ToInt32(meses);
        //    int days = age.Day - 2;

        //    // Print out not only how many years old they are but give months and days as well
        //    var ageInYMD = final + " months";
        //    var ageInYM = string.Format("{0} years, {1} months", age.Year, months);
        //    var ageInY = string.Format("{0} years", age.Year);

        //    return ;
        //}
        public FileResult NotesConsumerReport(string _uci, string _date = null)
        {
            FileResult result = null;
            try
            {
                string _dActual = DateTime.Now.ToLongDateString();
                var filePath2 = Server.MapPath("~/Content/Template/rpt_notesconsumer.html");
                //string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _pathUserBase = Server.MapPath("~/Content/Template/user_base.png");
                string _pathLogo = Server.MapPath("~/Content/Template/azul.png");
                string _pathLogoAzul = Server.MapPath("~/Content/Template/azul.png");
                string _pathLogoVerde = Server.MapPath("~/Content/Template/verde.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);


                List<ReportsModel> consumerList = new List<ReportsModel>();
                List<ReportsModel> NotesconsumerList = new List<ReportsModel>();
                consumerList = _ReportsModel.GetList_Reports_RPTNotesConsumer(_uci);
                NotesconsumerList = _ReportsModel.Get_NotesXConsumer(_uci);
                // Reporte por visita: el consumer puede tener varias visitas y cada una debe
                // imprimirse por separado, asi que si viene _date se filtra a esa unica visita
                // en vez de imprimir todas juntas en el mismo PDF.
                DateTime visitDate;
                if (!string.IsNullOrEmpty(_date) && DateTime.TryParse(_date, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out visitDate))
                {
                    NotesconsumerList = NotesconsumerList.Where(n => n.Date == visitDate).ToList();
                }
                string TableNotes = "";
                string TableService = "";
                int _ContarService = 1;
                foreach (var _Notes in NotesconsumerList)
                {

                    // Columna "Parent/Caregiver Signature": la firma capturada en
                    // leap_client (data URI completa) reemplaza al texto libre de
                    // PresentInSession en el rediseno del PDF (decision confirmada
                    // con el usuario).
                    string _signatureCell = !string.IsNullOrEmpty(_Notes.Signature)
                        ? "<img src='" + _Notes.Signature + "' alt='Signature' style='max-height:35px;max-width:150px;' />"
                        : "";
                    TableService += "<tr style='border: solid 1px black;'>";
                    TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black; height:40px;' class='text-center'>" + _Notes.Date.ToString("MM-dd-yyyy") + "</td>";
                    TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _Notes.Date + "</td>";
                    TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _signatureCell + "</td>";
                    TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _Notes.Duration + "</td>";
                    TableService += "</tr>";
                    TableNotes += "<tr>";
                    TableNotes += "<td style='padding: 5px;'>" + _Notes.Date.ToString("MM/dd/yyyyy") + " - " + _Notes.NotesConsumer + "</td>";
                    TableNotes += "</tr>";
                    _ContarService++;
                }
                if (_ContarService < 10)
                {
                    int CountReal = 10 - _ContarService;
                    for (int i = 0; i < CountReal; i++)
                    {
                        TableService += "<tr style='border: solid 1px black;'>";
                        TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black; height:40px' class='text-center'></td>";
                        TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'></td>";
                        TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'></td>";
                        TableService += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'></td>";
                        TableService += "</tr>";
                    }
                }
                _htmlString = _htmlString.Replace("[[TableService]]", TableService);
                _htmlString = _htmlString.Replace("[[TableNotes]]", TableNotes);
                //_htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));
                string NameConsumer = "";
                int contador_imagen = 0;
                // TimesWk/ParentName/TotalHours del formulario impreso salen de las
                // visitas reales (NotesconsumerList -> leap_api ReportController::notesByConsumer),
                // no de consumerList (Get_Reports_RPTConsumer, que no trae esos
                // datos) - antes salian siempre en blanco o con el dato fijo del
                // Consumer en vez del ingresado en la visita.
                var latestNote = NotesconsumerList.LastOrDefault();
                // [[TotalHours]] no debe repetir el total historico de TODAS las visitas del
                // consumer que trae Get_NotesXConsumer (asi lo calcula la API sobre la lista
                // completa) - una vez filtrado a la(s) visita(s) de este reporte, se recalcula
                // sumando solo lo que quedo en NotesconsumerList. Se deja como suma (no un
                // valor fijo de una sola fila) por si en el futuro un reporte vuelve a incluir
                // mas de una visita, igual que hacia antes.
                int _totalHoursSum = 0;
                foreach (var _n in NotesconsumerList)
                {
                    int _h;
                    var _firstToken = (_n.Duration ?? "").Split(' ').FirstOrDefault();
                    if (int.TryParse(_firstToken, out _h))
                    {
                        _totalHoursSum += _h;
                    }
                }
                string _totalHoursText = NotesconsumerList.Count > 0
                    ? _totalHoursSum + " " + (_totalHoursSum == 1 ? "hour" : "hours")
                    : "";
                foreach (var _Consumer in consumerList)
                {
                    _htmlString = _htmlString.Replace("[[ConsumerName]]", _Consumer.ConsumerName);
                    _htmlString = _htmlString.Replace("[[UCI]]", _Consumer.UCI);
                    _htmlString = _htmlString.Replace("[[RCenter]]", _Consumer.RegionalCenter);
                    _htmlString = _htmlString.Replace("[[TimesWk]]", latestNote?.TimesxWeek ?? "");
                    _htmlString = _htmlString.Replace("[[SpecialistName]]", _Consumer.spe_FullName);
                    _htmlString = _htmlString.Replace("[[SpecialistSignature]]", _Consumer.spe_FullName);
                    _htmlString = _htmlString.Replace("[[ParentName]]", latestNote?.PresentInSession ?? "");
                    _htmlString = _htmlString.Replace("[[TotalHours]]", _totalHoursText);
                    NameConsumer = _Consumer.spe_FullName;
                    
                    if (_Consumer.c_image != "")
                    {
                        byte[] imageBytes = Convert.FromBase64String(_Consumer.c_image);
                        string base64ImageUri = "data:image/png;base64," + _Consumer.c_image;
                        _htmlString = _htmlString.Replace("[[ConsumerImage]]", base64ImageUri);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[ConsumerImage]]", _pathUserBase);
                    }



                    if (_Consumer.Type.ToLower().Equals("leap"))
                    {
                        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoAzul);
                    }
                    else if (_Consumer.Type.ToLower().Equals("center"))
                    {
                        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoVerde);
                    }
                    else
                    {
                        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                    }
                    contador_imagen++;                }
                NameConsumer.Replace(" ", "_");

                



                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Portrait;
                int webPageWidth = 1024;
                int webPageHeight = 0;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(_htmlString, "");
                foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                {
                    doc.AddPage(page);
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_NotesConsumer_" + NameConsumer + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }
        //=====================================================================//
        //============== NUEVO METODO DE REPORTE DE CUMPLEAÑEROS DEL MES ======//
        //=====================================================================//
     
        //public FileResult BirthDayReport_XMes(string regional, int month, int year)
        public FileResult BirthDayReport_XMes(int month, int year)
        {
            FileResult result = null;
            try
            {
                string _dActual = DateTime.Now.ToLongDateString();
                var filePath2 = Server.MapPath("~/Content/Template/rpt_bday.html");
                string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _pathLogoVerde = Server.MapPath("~/Content/Template/verde.png");
                //string _pathLogoAzul = Server.MapPath("~/Content/Template/azul.png");
                string _pathLogoAzul = Server.MapPath("~/Content/Template/logo.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);
                List<ReportsModel> _List = _ReportsModel._RptBirthday_XMes(month,year);
                int MaxRowXPage = 20;
                int CurrentRow = 0;
                int CountRow = 1;
                //string _Programa = "";
                List<string> sections = new List<string>();
                string CurrentSection = "";
                foreach (var _birthday in _List)
                {
                    if (CurrentRow >= MaxRowXPage)
                    {
                        sections.Add(CurrentSection);
                        CurrentSection = "";
                        CurrentRow = 0;
                    }
                    CurrentSection += "<tr style='border: solid 1px black;'>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.DateOfBirth.ToString("MM-dd-yyyy") + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.UCI + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.ConsumerName + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.RegionalCenter + "</td>";
                    //CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px;' class='text-center'>" + _birthday.spe_FullName + "</td>";
                    CurrentSection += "</tr>";
                    CountRow++;
                    CurrentRow++;
                    //_Programa = _birthday.Type;
                }

                if (!string.IsNullOrEmpty(CurrentSection))
                {
                    sections.Add(CurrentSection);
                }

                _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoAzul);
                _htmlString = _htmlString.Replace("[[Date]]", month + "/" + year);
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));

                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Portrait;
                int webPageWidth = 1024;
                int webPageHeight = 0;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;

                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                foreach (var section in sections)
                {
                    string sectionHtml = _htmlString.Replace("[[Table]]", section);
                    SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(sectionHtml, "");
                    foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                    {
                        doc.AddPage(page);
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_Birthdays_" + DateTime.Now.ToShortDateString() + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }
        
        //ESTE REPORTE DE DESACTIVARA Y PARA EVITAR CONFUCIONES NO SE ELIMINARA EL METODO
        public FileResult BirthDayReport(DateTime _From, DateTime _To)
        {
            FileResult result = null;
            try
            {
                string _dActual = DateTime.Now.ToLongDateString();
                var filePath2 = Server.MapPath("~/Content/Template/rpt_bday.html");
                //string _pathLogo = Server.MapPath("~/Content/Template/azul.png");
                string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _pathLogoVerde = Server.MapPath("~/Content/Template/verde.png");
                string _pathLogoAzul = Server.MapPath("~/Content/Template/azul.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);
                List<ReportsModel> _List = _ReportsModel._RptBirthday(_From, _To);
                int MaxRowXPage = 23;
                int CurrentRow = 0;
                int CountRow = 1;
                //string _Programa = "";
                List<string> sections = new List<string>();
                string CurrentSection = "";
                foreach (var _birthday in _List)
                {
                    if (CurrentRow >= MaxRowXPage)
                    {
                        sections.Add(CurrentSection);
                        CurrentSection = "";
                        CurrentRow = 0;
                    }
                    CurrentSection += "<tr style='border: solid 1px black;'>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.DateOfBirth.ToString("MM-dd-yyyy") + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.UCI + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.ConsumerName + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>" + _birthday.Age + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px;' class='text-center'>" + _birthday.spe_FullName + "</td>";
                    CurrentSection += "</tr>";
                    CountRow++;
                    CurrentRow++;
                    //_Programa = _birthday.Type;
                }
                //for (int i = 0; i < 40; i++)
                //{
                //    if (CurrentRow >= MaxRowXPage)
                //    {
                //        sections.Add(CurrentSection);
                //        CurrentSection = "";
                //        CurrentRow = 0;
                //    }
                //    CurrentSection += "<tr style='border: solid 1px black;'>";
                //    //CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'>  " + i + "  </td>";
                //    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'> " + i + "/10/2024</td>";
                //    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'> " + i + "1212" + i + " </td>";
                //    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'> villanueva hernandez brokenrope Santamaria monterroza </td>";
                //    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black;' class='text-center'> 20 </td>";
                //    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px;' class='text-center'>  Katherinne Jeanmilette patricia monterroza </td>";
                //    CurrentSection += "</tr>";
                //    CurrentRow++;
                //}


                if (!string.IsNullOrEmpty(CurrentSection))
                {
                    sections.Add(CurrentSection);
                }
                //if (_Programa.Equals("Leap"))
                //{
                //    _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoAzul);
                //}
                //else if(_Programa.Equals("Cent"))
                //{
                //    _htmlString = _htmlString.Replace("[[Logo]]", _pathLogoVerde);
                //}
                //else
                //{
                //    _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                //}
                _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                _htmlString = _htmlString.Replace("[[Date]]", _From.ToString("MM-dd-yyyy") + " - " + _To.ToString("MM-dd-yyyy"));
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));

                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Portrait;
                int webPageWidth = 1024;
                int webPageHeight = 0;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;

                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                foreach (var section in sections)
                {
                    string sectionHtml = _htmlString.Replace("[[Table]]", section);
                    SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(sectionHtml, "");
                    foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                    {
                        doc.AddPage(page);
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_Birthdays_" + DateTime.Now.ToShortDateString() + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }

        //public FileResult CenterReport(string regional, int month, int year)
        //{
        //    FileResult result = null;
        //    try
        //    {
        //        int daysInMonth = DateTime.DaysInMonth(year, month);
        //        List<DateTime> sundays = new List<DateTime>();
        //        for (int day = 1; day <= daysInMonth; day++)
        //        {
        //            DateTime currentDate = new DateTime(year, month, day);
        //            if (currentDate.DayOfWeek == DayOfWeek.Sunday)
        //            {
        //                sundays.Add(currentDate);
        //            }
        //        }
        //        var filePath2 = Server.MapPath("~/Content/Template/rpt_center.html");
        //        string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
        //        string _htmlString = System.IO.File.ReadAllText(filePath2);
        //        List<string> sections = new List<string>();
        //        string CurrentSection = "";
        //        CurrentSection += "<tr class='text-center' style='border: solid 1px black;color: #fff; '>";
        //        CurrentSection += "<th style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black; background:#01550f; '>Consumer name</th>";
        //        CurrentSection += "<th style='border-right:solid 1px black; background:#01550f;'>A</th>";
        //        CurrentSection += "<th style='border-right:solid 1px black; background:#01550f;'>B</th>";
        //        CurrentSection += "<th style='border-right:solid 1px black; background:#01550f;'>C</th>";
        //        for (int i = 1; i <= daysInMonth; i++)
        //        {
        //            DateTime currentDate = new DateTime(year, month, i);
        //            string backgroundColor = (currentDate.DayOfWeek == DayOfWeek.Sunday) ? "#03227a" : "#01550f";
        //            CurrentSection += $"<th style='border-right:solid 1px black; background:{backgroundColor};'>{i}</th>";
        //        }
        //        CurrentSection += "<th style='border-right:solid 1px black; background:#01550f;'>Total</th>";
        //        CurrentSection += "</tr>";

        //        int CountConsumer = 10;
        //        for (int i = 1; i <= CountConsumer; i++)
        //        {
        //            int TotalCountByConsumer = 0;
        //            CurrentSection += "<tr class='text-center' style='border: solid 1px black;color: #000;  '>";
        //            CurrentSection += "<th style='padding-top: 10px; padding-bottom: 10px; border-right:solid 1px black; background:#c7ccdb;'>Ulises Samayoa</th>";
        //            CurrentSection += "<th style='border-right:solid 1px black; background:#c7ccdb;'>" + i + "</th>";
        //            CurrentSection += "<th style='border-right:solid 1px black; background:#c7ccdb;'>N/A</th>";
        //            CurrentSection += "<th style='border-right:solid 1px black; background:#c7ccdb;'>N/A</th>";
        //            string DayValue = "";
        //            for (int j = 1; j <= daysInMonth; j++)
        //            {
        //                DateTime currentDate = new DateTime(year, month, j);
        //                DayValue = (currentDate.DayOfWeek == DayOfWeek.Sunday) ? "" : j.ToString();
        //                string backgroundColor = (currentDate.DayOfWeek == DayOfWeek.Sunday) ? "#043cdc" : "#c7ccdb";
        //                CurrentSection += $"<th style='border-right:solid 1px black; background:{backgroundColor};'>{DayValue}</th>";
        //                TotalCountByConsumer = TotalCountByConsumer + j;
        //            }
        //            CurrentSection += "<th style='border-right:solid 1px black; background:#c7ccdb;'>" + TotalCountByConsumer + "</th>";
        //            CurrentSection += "</tr>";
        //            TotalCountByConsumer = 0;
        //        }
        //        if (!string.IsNullOrEmpty(CurrentSection))
        //        {
        //            sections.Add(CurrentSection);
        //        }
        //        _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
        //        string[] Basemonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        //        string CurrentMonth = "";
        //        CurrentMonth = Basemonths[month - 1];
        //        _htmlString = _htmlString.Replace("[[MonthReport]]", CurrentMonth + "/" + year);
        //        _htmlString = _htmlString.Replace("[[RegCenter]]", regional);
        //        _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));
        //        PdfPageSize pageSize = PdfPageSize.Letter;
        //        PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Landscape;
        //        int webPageWidth = 1024;
        //        int webPageHeight = 0;
        //        HtmlToPdf converter = new HtmlToPdf();
        //        converter.Options.PdfPageSize = pageSize;
        //        converter.Options.PdfPageOrientation = pdfPageOrientation;
        //        converter.Options.WebPageWidth = webPageWidth;
        //        converter.Options.WebPageHeight = webPageHeight;
        //        SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
        //        foreach (var section in sections)
        //        {
        //            string sectionHtml = _htmlString.Replace("[[Table]]", section);
        //            SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(sectionHtml, "");
        //            foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
        //            {
        //                doc.AddPage(page);
        //            }
        //        }
        //        using (var stream = new System.IO.MemoryStream())
        //        {
        //            doc.Save(stream);
        //            stream.Seek(0, System.IO.SeekOrigin.Begin);
        //            result = new FileContentResult(stream.ToArray(), "application/pdf");
        //            result.FileDownloadName = "Rpt_Center_" + DateTime.Now.ToShortDateString() + ".pdf";
        //        }
        //        doc.Close();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        return result;
        //        throw;
        //    }
        //}

        public async Task<FileStreamResult> CenterReport(string regional, int month, int year)
        {
            var data = _ReportsModel._Rpt_Data_CentersByMonth(month, year, regional);
            string numeroMes = month.ToString();

            string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(int.Parse(numeroMes));
            var reporte = Crear_RptExcel(data, nombreMes, year.ToString(),regional);

            var fileStreamResult = new FileStreamResult(reporte, "application/vnd.ms-excel")
            {
                FileDownloadName = "RPT_Center.xls"
            };
            return fileStreamResult;
        }
        public MemoryStream Crear_RptExcel(List<ReportsModel> _data,string month,string year,string regional)
        {
            MemoryStream workStream = new MemoryStream();
            try
            {
                Int32 TotalCeldasGlobal = 0;
                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Reporte");
                var imagePath = Server.MapPath("~/Content/Images/logoN.png");
                var picture = worksheet.AddPicture(imagePath)
                    .MoveTo(worksheet.Cell(1, 1))
                    .Scale(0.5);

                for (int j = 0; j < 34; j++)
                {
                    var encabezados2 = worksheet.Cell(1, j + 1);
                    encabezados2.Value = "";
                    encabezados2.Style.Font.Bold = true;
                    encabezados2.Style.Font.FontColor = XLColor.Black;
                    encabezados2.Style.Fill.BackgroundColor = XLColor.Black;
                    encabezados2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    encabezados2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    encabezados2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    encabezados2.Style.Border.OutsideBorderColor = XLColor.Black;
                }
                worksheet.Row(1).Height = 50;
                worksheet.Row(2).Height = 50;
                worksheet.Row(3).Height = 35;
                

                var Titulo2Reporte = worksheet.Range(1, 36, 2, 36).Merge().Cell(1, 1);
                Titulo2Reporte.Value = "";
                Titulo2Reporte.Style.Font.Bold = true;
                Titulo2Reporte.Style.Font.FontColor = XLColor.White;
                Titulo2Reporte.Style.Fill.BackgroundColor = XLColor.Black;
                Titulo2Reporte.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                Titulo2Reporte.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                var Titulo3Reporte = worksheet.Range(1, 37, 2, 37).Merge().Cell(1, 1);
                Titulo3Reporte.Value = "";
                Titulo3Reporte.Style.Font.Bold = true;
                Titulo3Reporte.Style.Font.FontColor = XLColor.White;
                Titulo3Reporte.Style.Fill.BackgroundColor = XLColor.Black;
                Titulo3Reporte.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                Titulo3Reporte.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                for (int h = 0; h < 24; h++)
                {
                    var encabezados2 = worksheet.Cell(2, (h+10) + 1);
                    encabezados2.Value = "";
                    encabezados2.Style.Font.Bold = true;
                    encabezados2.Style.Font.FontColor = XLColor.Black;
                    encabezados2.Style.Fill.BackgroundColor = XLColor.Black;
                    encabezados2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    encabezados2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    encabezados2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    encabezados2.Style.Border.OutsideBorderColor = XLColor.Black;
                }

                var MesActual = worksheet.Range(2, 25, 2, 33).Merge().Cell(1, 1);
                
                MesActual.Value = month+" ,"+year ;
                MesActual.Style.Font.Bold = true;
                MesActual.Style.Font.FontColor = XLColor.White;
                MesActual.Style.Fill.BackgroundColor = XLColor.Black;
                MesActual.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                MesActual.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                MesActual.Style.Font.FontSize = 26;
                //var PeriodoRangoReporte = worksheet.Range(1, 4, 2, 6).Merge();
                //var PeriodoReporte = PeriodoRangoReporte.FirstCell();
                //PeriodoReporte.Value = "PERIODO:";
                //PeriodoReporte.Style.Font.Bold = true;
                //PeriodoReporte.Style.Font.FontColor = XLColor.White;
                //TituloReporte.Style.Fill.BackgroundColor = XLColor.Redwood;
                //TituloReporte.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //TituloReporte.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                //var Cantidad_TotalReporte = worksheet.Range(1, 8, 2, 9).Merge().Cell(1,1);
                //Cantidad_TotalReporte.Value = "TOTAL:";
                //Cantidad_TotalReporte.Style.Font.Bold = true;
                //Cantidad_TotalReporte.Style.Font.FontColor = XLColor.White;
                //Cantidad_TotalReporte.Style.Fill.BackgroundColor = XLColor.DarkBlue;
                //Cantidad_TotalReporte.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //Cantidad_TotalReporte.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                //var Cantidad_Reporte = worksheet.Range(2, 8, 3, 9).Merge().Cell(1, 1);
                //Cantidad_Reporte.Value = "Count";
                //Cantidad_Reporte.Style.Font.Bold = true;
                //Cantidad_Reporte.Style.Font.FontColor = XLColor.White;
                //Cantidad_Reporte.Style.Fill.BackgroundColor = XLColor.DarkBlue;
                //Cantidad_Reporte.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                //Cantidad_Reporte.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                string[] Titulos_Tabla = { "Consumer name", "H", };
                Int32 _Cantididad_Titulos = Titulos_Tabla.Length;
                for (int i = 0; i < _Cantididad_Titulos; i++)
                {
                    var encabezados = worksheet.Cell(3, i + 1);
                    encabezados.Value = Titulos_Tabla[i];
                    encabezados.Style.Font.Bold = true;
                    encabezados.Style.Font.FontColor = XLColor.Black;
                    encabezados.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    encabezados.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    encabezados.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    encabezados.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    encabezados.Style.Border.OutsideBorderColor = XLColor.Black;
                    encabezados.Style.Font.FontSize = 12;
                    if (i>=1)
                    {
                        for (int j = 0; j < 31; j++)
                        {
                            var encabezados2 = worksheet.Cell(3, (j+2) + 1);
                            encabezados2.Value = j + 1;
                            encabezados2.Style.Font.Bold = true;
                            encabezados2.Style.Font.FontColor = XLColor.Black;
                            encabezados2.Style.Fill.BackgroundColor = XLColor.LightGreen;
                            encabezados2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            encabezados2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            encabezados2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            encabezados2.Style.Border.OutsideBorderColor = XLColor.Black;
                            encabezados2.Style.Font.FontSize = 12;
                        }
                    }
                    var encabezados3 = worksheet.Cell(3, 34);
                    encabezados3.Value = "TOTAL";
                    encabezados3.Style.Font.Bold = true;
                    encabezados3.Style.Font.FontColor = XLColor.Black;
                    encabezados3.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    encabezados3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    encabezados3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    encabezados3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    encabezados3.Style.Border.OutsideBorderColor = XLColor.Black;
                    encabezados3.Style.Font.FontSize = 12;

                }

                string[] Titulos2_Tabla = { "Consumer name", "H",};
                Int32 _Cantididad2_Titulos = Titulos2_Tabla.Length;
                for (int i = 35; i < (35+_Cantididad2_Titulos); i++)
                {
                    var encabezadosE = worksheet.Cell(3, i + 1);
                    encabezadosE.Value = Titulos2_Tabla[i-35];
                    encabezadosE.Style.Font.Bold = true;
                    encabezadosE.Style.Font.FontColor = XLColor.Black;
                    encabezadosE.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    encabezadosE.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    encabezadosE.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    encabezadosE.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    encabezadosE.Style.Border.OutsideBorderColor = XLColor.Black;
                    encabezadosE.Style.Font.FontSize = 12;


                }

                string[] TitulosEspe_Tabla = { "DE LA TORRE, MAR", "MULLIGAN, JENNIFER", "RICH GUILLOT, NICOLE", "WATERFIELD, JENAE", "TOTAL" };
                Int32 _CantididadEspe_Titulos = TitulosEspe_Tabla.Length;
                for (int i = 38; i < (38+_CantididadEspe_Titulos); i++)
                {
                    var range = worksheet.Range(1, i, 3, i);

                    // Aplicar bordes a todas las celdas dentro del rango
                    range.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    range.Style.Border.OutsideBorderColor = XLColor.Black;

                    // Combinar las celdas
                    range.Merge();
                    var encabezadosE2 = range.Cell(1, 1);
                    //encabezadosE2 = worksheet.Cell(3, i + 1);
                    encabezadosE2.Value = TitulosEspe_Tabla[i-38];
                    encabezadosE2.Style.Font.Bold = false;
                    encabezadosE2.Style.Font.FontColor = XLColor.Black;
                    encabezadosE2.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    encabezadosE2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    encabezadosE2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    encabezadosE2.Style.Alignment.TextRotation = 90;
                    encabezadosE2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    encabezadosE2.Style.Border.OutsideBorderColor = XLColor.Black;
                    encabezadosE2.Style.Font.FontSize = 12;
                    worksheet.Column(i).Width = 10;
                }
                worksheet.Column(1).Width = 50;
                var datoR = "";
                    Int32 _count = 4;
                Int32 _count_t = 1;
                foreach (var item in _data)
                {
                    string[] Valor_Tabla = { item.ConsumerName, item.MaxHours.ToString() };
                    datoR = item.RegionalCenter;
                    for (int i = 0; i < Valor_Tabla.Length; i++)
                    {
                        //var valores = worksheet.Cell(_count,i+1).Value = Valor_Tabla[i];
                        var valores = worksheet.Cell(_count,i+1);
                        valores.Value = Valor_Tabla[i];
                        valores.Style.Font.Bold = true;
                        valores.Style.Font.FontColor = XLColor.Black;
                        valores.Style.Fill.BackgroundColor = XLColor.LightGray;
                        valores.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        valores.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                        valores.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        valores.Style.Border.OutsideBorderColor = XLColor.Black;
                        valores.Style.Font.FontSize = 12;


                        if (i >= 1)
                        {
                            for (int j = 0; j < 32; j++)
                            {

                                var valores2 = worksheet.Cell(_count, (j+2) + 1);
                                // Horas brindadas ese dia del mes (columna = dia j+1). Antes esta
                                // columna quedaba siempre en blanco; ahora se llena con lo que
                                // trae leap_api (centersByMonth) a partir de las visitas reales,
                                // para que el SUMATORIA de la fila de mas abajo sume algo.
                                int _dayHours;
                                if (item.DayHours != null && item.DayHours.TryGetValue(j + 1, out _dayHours) && _dayHours > 0)
                                {
                                    valores2.Value = _dayHours;
                                }
                                else
                                {
                                    valores2.Value = "";
                                }
                                valores2.Style.Font.Bold = true;
                                valores2.Style.Font.FontColor = XLColor.Black;
                                valores2.Style.Fill.BackgroundColor = XLColor.LightGray;
                                valores2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                valores2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                valores2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                valores2.Style.Border.OutsideBorderColor = XLColor.Black;
                                worksheet.Column(j+2).Width = 5;
                                valores2.Style.Font.FontSize = 12;
                            }
                            for (int j = 35; j < 42; j++)
                            {

                                var valores2 = worksheet.Cell(_count, j + 1);
                                valores2.Value = "";
                                valores2.Style.Font.Bold = true;
                                valores2.Style.Font.FontColor = XLColor.Black;
                                valores2.Style.Fill.BackgroundColor = XLColor.LightGray;
                                valores2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                valores2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                valores2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                                valores2.Style.Border.OutsideBorderColor = XLColor.Black;
                                valores2.Style.Font.FontSize = 12;
                                //worksheet.Column(j).Width = 5;
                            }
                        }
                        worksheet.Cell("AJ" + _count).FormulaA1 = "=+A" + _count;
                        worksheet.Cell("AK" + _count).FormulaA1 = "=+B" + _count;

                        worksheet.Row(_count).Height = 22;
                    }
                    string SUMATORIA = "=SUM(C" + _count + ":AG" + _count + ")";
                    worksheet.Cell("AH" + _count).FormulaA1 = SUMATORIA;
                    _count++;
                    _count_t++;
                }
                TotalCeldasGlobal = _count;

                string[] ContraTitulos_Tabla = { "TOTAL", "" };
                Int32 _Cantididad_ContraTitulos = ContraTitulos_Tabla.Length;
                for (int i = 0; i < _Cantididad_ContraTitulos; i++)
                {
                    var footer = worksheet.Cell(TotalCeldasGlobal, i + 1);
                    footer.Value = ContraTitulos_Tabla[i];
                    footer.Style.Font.Bold = true;
                    footer.Style.Font.FontColor = XLColor.Black;
                    footer.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    footer.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    footer.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    footer.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    footer.Style.Border.OutsideBorderColor = XLColor.Black;

                    var footerE2 = worksheet.Cell(TotalCeldasGlobal, 36);
                    footerE2.Value = ContraTitulos_Tabla[0];
                    footerE2.Style.Font.Bold = true;
                    footerE2.Style.Font.FontColor = XLColor.Black;
                    footerE2.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    footerE2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    footerE2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    footerE2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    footerE2.Style.Border.OutsideBorderColor = XLColor.Black;
                    if (i >= 1) 
                    {
                        for (int j = 0; j < 31; j++)
                        {
                            var footer2 = worksheet.Cell(TotalCeldasGlobal, (j + 2) + 1);
                            footer2.Value = "";
                            footer2.Style.Font.Bold = true;
                            footer2.Style.Font.FontColor = XLColor.Black;
                            footer2.Style.Fill.BackgroundColor = XLColor.LightGreen;
                            footer2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            footer2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            footer2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            footer2.Style.Border.OutsideBorderColor = XLColor.Black;
                        }
                        for (int j = 36; j < 42; j++)
                        {
                            var footer2 = worksheet.Cell(TotalCeldasGlobal, j + 1);
                            footer2.Value = "";
                            footer2.Style.Font.Bold = true;
                            footer2.Style.Font.FontColor = XLColor.Black;
                            footer2.Style.Fill.BackgroundColor = XLColor.LightGreen;
                            footer2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            footer2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            footer2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            footer2.Style.Border.OutsideBorderColor = XLColor.Black;
                        }
                    }
                    var footer3 = worksheet.Cell(TotalCeldasGlobal, 34);
                    // Fila TOTAL (justo debajo del ultimo consumer): suma los subtotales por
                    // consumer de la columna AH (filas 4 a TotalCeldasGlobal-1, ya que los
                    // consumers arrancan en la fila 4).
                    if (TotalCeldasGlobal > 4)
                    {
                        footer3.FormulaA1 = "=SUM(AH4:AH" + (TotalCeldasGlobal - 1) + ")";
                    }
                    else
                    {
                        footer3.Value = "";
                    }
                    footer3.Style.Font.Bold = true;
                    footer3.Style.Font.FontColor = XLColor.Black;
                    footer3.Style.Fill.BackgroundColor = XLColor.LightGreen;
                    footer3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    footer3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    footer3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    footer3.Style.Border.OutsideBorderColor = XLColor.Black;
                }
                //lo pondre aca

                var TituloReporte = worksheet.Range(2, 1, 2, 10).Merge().Cell(1, 1);
                TituloReporte.Value = datoR ;
                TituloReporte.Style.Font.Bold = true;
                TituloReporte.Style.Font.FontColor = XLColor.White;
                TituloReporte.Style.Fill.BackgroundColor = XLColor.Black;
                TituloReporte.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                TituloReporte.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                TituloReporte.Style.Font.FontSize = 26;

                string[] ContraTitulos2_Tabla = { "GRAND TOTAL", "" };
                Int32 _Cantididad_Contra2Titulos = ContraTitulos2_Tabla.Length;
                for (int i = 0; i < _Cantididad_Contra2Titulos; i++)
                {
                    var footer_2 = worksheet.Cell(TotalCeldasGlobal+1, i + 1);
                    footer_2.Value = ContraTitulos2_Tabla[i];
                    footer_2.Style.Font.Bold = true;
                    footer_2.Style.Font.FontColor = XLColor.Black;
                    footer_2.Style.Fill.BackgroundColor = XLColor.Yellow;
                    footer_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    footer_2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    footer_2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    footer_2.Style.Border.OutsideBorderColor = XLColor.Black;

                    var footerE_2 = worksheet.Cell(TotalCeldasGlobal + 1, 36);
                    footerE_2.Value = ContraTitulos2_Tabla[0];
                    footerE_2.Style.Font.Bold = true;
                    footerE_2.Style.Font.FontColor = XLColor.Black;
                    footerE_2.Style.Fill.BackgroundColor = XLColor.Yellow;
                    footerE_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    footerE_2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    footerE_2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    footerE_2.Style.Border.OutsideBorderColor = XLColor.Black;

                    if (i >= 1)
                    {
                        for (int j = 0; j < 31; j++)
                        {
                            var footer_2_2 = worksheet.Cell(TotalCeldasGlobal+1, (j + 2) + 1);
                            footer_2_2.Value = "";
                            footer_2_2.Style.Font.Bold = true;
                            footer_2_2.Style.Font.FontColor = XLColor.Black;
                            footer_2_2.Style.Fill.BackgroundColor = XLColor.Yellow;
                            footer_2_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            footer_2_2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            footer_2_2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            footer_2_2.Style.Border.OutsideBorderColor = XLColor.Black;
                        }
                        for (int j = 36; j < 42; j++)
                        {
                            var footerE_2_2 = worksheet.Cell(TotalCeldasGlobal + 1, j  + 1);
                            footerE_2_2.Value = "";
                            footerE_2_2.Style.Font.Bold = true;
                            footerE_2_2.Style.Font.FontColor = XLColor.Black;
                            footerE_2_2.Style.Fill.BackgroundColor = XLColor.Yellow;
                            footerE_2_2.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            footerE_2_2.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            footerE_2_2.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                            footerE_2_2.Style.Border.OutsideBorderColor = XLColor.Black;
                        }
                    }
                    var footer_2_3 = worksheet.Cell(TotalCeldasGlobal+1, 34);
                    footer_2_3.Value = "";
                    footer_2_3.Style.Font.Bold = true;
                    footer_2_3.Style.Font.FontColor = XLColor.Black;
                    footer_2_3.Style.Fill.BackgroundColor = XLColor.Yellow;
                    footer_2_3.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    footer_2_3.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    footer_2_3.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    footer_2_3.Style.Border.OutsideBorderColor = XLColor.Black;
                }

                worksheet.Column(36).Width = 50;
                //worksheet.Cell("AQ4").FormulaA1 = "=+B4";
                //worksheet.Cell("AP4").FormulaA1 = "=+A4";
                
                workbook.SaveAs(workStream);
                workStream.Position = 0;
                return workStream;
            }
            catch (Exception ex)
            {
                return workStream;
            }
        }

        // Reporte de visitas por CDS: mismo enfoque que CenterReport (grilla de
        // dias del mes x consumer, con las horas brindadas cada dia), pero
        // agrupado por CDS (specialist1) en vez de por Regional Center, y sin las
        // columnas extra de ese reporte (MaxHours, bloque de especialistas, etc).
        public FileStreamResult CDSVisitsReport(string cds, int month, int year)
        {
            var data = _ReportsModel._Rpt_Data_CDSVisitsByMonth(month, year, cds);
            string nombreMes = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
            var reporte = Crear_RptCDSVisitsExcel(data, nombreMes, year.ToString());

            return new FileStreamResult(reporte, "application/vnd.ms-excel")
            {
                FileDownloadName = "RPT_CDS_Visits.xls"
            };
        }

        public MemoryStream Crear_RptCDSVisitsExcel(List<ReportsModel> _data, string month, string year)
        {
            MemoryStream workStream = new MemoryStream();
            try
            {
                // Columnas: A = Customer, B..AF = dias 1-31, AG = total.
                const int firstDayColumn = 2;
                const int lastDayColumn = 32;
                const int totalColumn = 33;
                const int firstDataRow = 4;

                var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("Reporte");

                var imagePath = Server.MapPath("~/Content/Images/logoN.png");
                worksheet.AddPicture(imagePath).MoveTo(worksheet.Cell(1, 1)).Scale(0.5);
                worksheet.Row(1).Height = 50;
                worksheet.Row(2).Height = 35;

                var titulo = worksheet.Range(1, firstDayColumn, 1, totalColumn).Merge().Cell(1, 1);
                titulo.Value = "CDS and Customer Visits by month: " + month;
                StyleTitle(titulo, XLColor.Black, 14);

                string cdsName = _data.Select(d => d.CDS_name).FirstOrDefault(n => !string.IsNullOrEmpty(n)) ?? "";
                var cdsTitulo = worksheet.Range(2, firstDayColumn, 2, totalColumn).Merge().Cell(1, 1);
                cdsTitulo.Value = cdsName;
                StyleTitle(cdsTitulo, XLColor.Black, 14);

                var customerHeader = worksheet.Cell(3, 1);
                customerHeader.Value = "Customer";
                StyleHeader(customerHeader);
                worksheet.Column(1).Width = 30;
                for (int day = 1; day <= 31; day++)
                {
                    var dayHeader = worksheet.Cell(3, firstDayColumn + day - 1);
                    dayHeader.Value = day;
                    StyleHeader(dayHeader);
                    worksheet.Column(firstDayColumn + day - 1).Width = 5;
                }
                var totalHeader = worksheet.Cell(3, totalColumn);
                totalHeader.Value = "total";
                StyleHeader(totalHeader);
                worksheet.Column(totalColumn).Width = 10;

                int row = firstDataRow;
                foreach (var item in _data)
                {
                    var nameCell = worksheet.Cell(row, 1);
                    nameCell.Value = item.ConsumerName;
                    StyleData(nameCell);

                    for (int day = 1; day <= 31; day++)
                    {
                        var cell = worksheet.Cell(row, firstDayColumn + day - 1);
                        int hours;
                        if (item.DayHours != null && item.DayHours.TryGetValue(day, out hours) && hours > 0)
                        {
                            cell.Value = hours;
                        }
                        else
                        {
                            cell.Value = "";
                        }
                        StyleData(cell);
                    }

                    var rowTotal = worksheet.Cell(row, totalColumn);
                    rowTotal.FormulaA1 = "=SUM(B" + row + ":AF" + row + ")";
                    StyleData(rowTotal);
                    row++;
                }

                int totalRow = row;
                var totalLabel = worksheet.Range(totalRow, 1, totalRow, lastDayColumn).Merge().Cell(1, 1);
                totalLabel.Value = "TOTAL";
                StyleHeader(totalLabel);

                var grandTotal = worksheet.Cell(totalRow, totalColumn);
                if (totalRow > firstDataRow)
                {
                    grandTotal.FormulaA1 = "=SUM(AG" + firstDataRow + ":AG" + (totalRow - 1) + ")";
                }
                else
                {
                    grandTotal.Value = 0;
                }
                StyleHeader(grandTotal);

                workbook.SaveAs(workStream);
                workStream.Position = 0;
            }
            catch (Exception)
            {
                return workStream;
            }
            return workStream;
        }

        private static void StyleTitle(IXLCell cell, XLColor color, int fontSize)
        {
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = color;
            cell.Style.Font.FontSize = fontSize;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
        }

        private static void StyleHeader(IXLCell cell)
        {
            cell.Style.Font.Bold = true;
            cell.Style.Font.FontColor = XLColor.Black;
            cell.Style.Fill.BackgroundColor = XLColor.LightGreen;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.OutsideBorderColor = XLColor.Black;
        }

        private static void StyleData(IXLCell cell)
        {
            cell.Style.Font.FontColor = XLColor.Black;
            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            cell.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            cell.Style.Border.OutsideBorderColor = XLColor.Black;
        }

        public FileResult CdsReport(string cds, int month, int year)
        {
            FileResult result = null;
            try
            {
                List<ReportsModel> _List = _ReportsModel._RptByCDS(cds, month, year);

                var filePath2 = Server.MapPath("~/Content/Template/rpt_cds.html");
                string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);
                string _cdsName = "";
                List<string> sections = new List<string>();
                int MaxRowXPage = 10 ;
                int CurrentRow = 0;
                string CurrentSection = "";
                foreach (var _ReportList in _List)
                {
                    if (CurrentRow >= MaxRowXPage)
                    {
                        sections.Add(CurrentSection);
                        CurrentSection = "";
                        CurrentRow = 0;
                    }
                    string Report1_date = _ReportList.Report1.ToString("dd/MM/yyyy");
                    string Report2_date = _ReportList.Report2.ToString("dd/MM/yyyy");
                    string Report3_date = _ReportList.Report3.ToString("dd/MM/yyyy");
                    string Report4_date = _ReportList.Report4.ToString("dd/MM/yyyy");
                    string Report5_date = _ReportList.Report5.ToString("dd/MM/yyyy");
                    string ReportClose_date = _ReportList.ReportClose.ToString("dd/MM/yyyy");
                    CurrentSection += "<tr class='text-center' style='border:1px solid black;'>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ConsumerName + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.DateOfBirth.ToString("MM-dd-yyyy") + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.RegionalCenter + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ServiceCoordinator + "</td>";
                    if (Report1_date == "01/01/1900" || Report1_date == "01/01/1901" || Report1_date == "01/01/0001")
                    //if (Report1_date == DateTime.MinValue.ToString("dd/MM/yyyy") || Report1_date == "01/01/1901")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report1.ToString("MM-dd-yyyy") + "</td>";
                    }
                    if (Report2_date == "01/01/1900" || Report2_date == "01/01/1901" || Report2_date == "01/01/0001")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report2.ToString("MM-dd-yyyy") + "</td>";
                    }
                    if (Report3_date == "01/01/1900" || Report3_date == "01/01/1901" || Report3_date == "01/01/0001")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report3.ToString("MM-dd-yyyy") + "</td>";
                    }
                    if (Report4_date == "01/01/1900" || Report4_date == "01/01/1901" || Report4_date == "01/01/0001")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report4.ToString("MM-dd-yyyy") + "</td>";
                    }
                    if (Report5_date == "01/01/1900" || Report5_date == "01/01/1901" || Report5_date == "01/01/0001")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black; min-width:100px;  height:40px !important;'>" + _ReportList.Report5.ToString("MM-dd-yyyy") + "</td>";
                    }
                    if (ReportClose_date == "01/01/1900" || ReportClose_date == "01/01/1901" || ReportClose_date == "01/01/0001")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black; min-width:100px; height:40px !important;' >" + _ReportList.ReportClose.ToString("MM-dd-yyyy") + "</td>";
                    }
                    CurrentSection += "</tr>";
                    CurrentRow++;
                    _cdsName = _ReportList.spe_FullName;
                }


                //int CountConsumer = 9;
                //for (int i = 1; i <= CountConsumer; i++)
                //{
                //    int TotalCountByConsumer = 0;
                //    CurrentSection += "<tr class='text-center' style='border:1px solid black;'>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>Jorge Perez</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>01/"+(i+1)+"/2024</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>NLACRC</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>Ulises Samayoa</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>01/" + (i + 2) + "/2024</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>10/" + (i + 3) + "/2024</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>12/" + (i + 4) + "/2024</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>05/" + (i + 5) + "/2024</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>12/" + (i + 6) + "/2024</td>";
                //    CurrentSection += "</tr>";
                //    TotalCountByConsumer = 0;
                //}
                if (!string.IsNullOrEmpty(CurrentSection))
                {
                    sections.Add(CurrentSection);
                }
                _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                string[] Basemonths = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                string CurrentMonth = "";
                CurrentMonth = Basemonths[month - 1];
                _htmlString = _htmlString.Replace("[[MonthReport]]", CurrentMonth + "/" + year);
                _htmlString = _htmlString.Replace("[[CdsReport]]", _cdsName);
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));
                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Landscape;
                int webPageWidth = 1024;
                int webPageHeight = 0;
                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                foreach (var section in sections)
                {
                    string sectionHtml = _htmlString.Replace("[[Table]]", section);
                    SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(sectionHtml, "");
                    foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                    {
                        doc.AddPage(page);
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_ByCDS_" + DateTime.Now.ToShortDateString() + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }


        public FileResult CdsConsumerReport(string cdsconsumer)
        {
            FileResult result = null;
            try
            {
                List<ReportsModel> _List = _ReportsModel._RptByCDSConsumer(cdsconsumer);

                var filePath2 = Server.MapPath("~/Content/Template/rpt_cdsconsumer.html");
                string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);

                string _cdsName = "";
                List<string> sections = new List<string>();
                int MaxRowXPage = 15;
                int CurrentRow = 0;
                string CurrentSection = "";
                foreach (var _ReportList in _List)
                {
                    if (CurrentRow >= MaxRowXPage)
                    {
                        sections.Add(CurrentSection);
                        CurrentSection = "";
                        CurrentRow = 0;
                    }
                    //CurrentSection += "<tr class='text-center' style='border:1px solid black;'>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ConsumerName + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.DateOfBirth.ToString("MM-dd-yyyy") + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.RegionalCenter + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ServiceCoordinator + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report1.ToString("MM-dd-yyyy") + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report2.ToString("MM-dd-yyyy") + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report3.ToString("MM-dd-yyyy") + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Report4.ToString("MM-dd-yyyy") + "</td>";
                    //CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ReportClose.ToString("MM-dd-yyyy") + "</td>";
                    //CurrentSection += "</tr>";

                    CurrentSection += "<tr class='text-center' style='border:1px solid black;'>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ConsumerName + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.DateOfBirth.ToString("MM-dd-yyyy") + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Address + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.CityID + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.HoursxWeek + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.State + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ZipCode + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.Phone + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ServiceCoordinator + "</td>";
                    CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.ParentFullName + "</td>";
                    if (_ReportList.LanguajeName == "English")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'>x</td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                    }
                    else if (_ReportList.LanguajeName == "Spanish")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'>x</td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                    }
                    else if (_ReportList.LanguajeName == "Bilingual")
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'>x</td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                        CurrentSection += "<td style='border-right:solid 1px black;'>" + _ReportList.LanguajeName + "</td>";
                    }
                    CurrentSection += "</tr>";
                    CurrentRow++;
                    if (cdsconsumer == "999")
                    {
                        _cdsName = "General";
                    }
                    else {
                        _cdsName = _ReportList.spe_FullName;
                    }
                    
                }
                //string[] Nombres = { "Jose", "Juan", "Andres", "Katherinne", "Patricia", "Mauricio", "Jefrey", "Jeff", "Arnold", "Duncan", "Sheldon", "Franco","Joaquin","Matias","Marcos" };
                //int CountConsumer = 9;
                //for (int i = 1; i <= CountConsumer; i++)
                //{
                //    int TotalCountByConsumer = 0;
                //    CurrentSection += "<tr class='text-center' style='border:1px solid black;'>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>"+Nombres[i] +" " + Nombres[i+3]+"</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>01/" + (i + 1) + "/2024</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>Address Address Address Address Address Address</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>City</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>" + i + "</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>CA</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>" + (i + 3) + "12"+i+"</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>"+(i+5)+"1212"+i+"</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>" + Nombres[i+2] + " " + Nombres[i + 1] + "</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>" + Nombres[i] + " " + Nombres[i + 2] + "</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'>|</td>";
                //    CurrentSection += "<td style='border-right:solid 1px black;'></td>";
                //    CurrentSection += "</tr>";
                //    TotalCountByConsumer = 0;
                //}
                if (!string.IsNullOrEmpty(CurrentSection))
                {
                    sections.Add(CurrentSection);
                }
                _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);

                //string CurrentMonth = "";

                //_htmlString = _htmlString.Replace("[[MonthReport]]", CurrentMonth + "/" + year);
                _htmlString = _htmlString.Replace("[[RegCenter]]", cdsconsumer);
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));
                _htmlString = _htmlString.Replace("[[CdsReport]]", _cdsName);
                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Landscape;
                int webPageWidth = 1024;
                int webPageHeight = 0;
                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                foreach (var section in sections)
                {
                    string sectionHtml = _htmlString.Replace("[[Table]]", section);
                    SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(sectionHtml, "");
                    foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                    {
                        doc.AddPage(page);
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_CDS_Consumer_" + DateTime.Now.ToShortDateString() + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }


        public FileResult NewConsumerXDate(DateTime _From, DateTime _To)
        {
            FileResult result = null;
            try
            {
                string _dActual = DateTime.Now.ToLongDateString();
                var filePath2 = Server.MapPath("~/Content/Template/rpt_newconsumer.html");
                string _pathLogo = Server.MapPath("~/Content/Template/logo.png");
                string _pathLogoVerde = Server.MapPath("~/Content/Template/verde.png");
                string _pathLogoAzul = Server.MapPath("~/Content/Template/azul.png");
                string _htmlString = System.IO.File.ReadAllText(filePath2);
                List<ReportsModel> _List = _ReportsModel._NewConsumerXDate(_From, _To);
                int MaxRowXPage = 15;
                int CurrentRow = 0;
                int CountRow = 1;
                //string _Programa = "";
                List<string> sections = new List<string>();
                string CurrentSection = "";
                foreach (var _NewConsumer in _List)
                {
                    if (CurrentRow >= MaxRowXPage)
                    {
                        sections.Add(CurrentSection);
                        CurrentSection = "";
                        CurrentRow = 0;
                    }
                    
                    CurrentSection += "<tr>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.ConsumerName +"<br>"+ _NewConsumer.spe_FullName + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.CityID + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.DateOfBirth.ToString("MM-dd-yyyy") + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.RegionalCenter + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.ServiceCoordinator + "</td>";
                    string ccc = _NewConsumer._To.ToString("dd/MM/yyyy");
                    string ffrom = _NewConsumer._From.ToString("dd/MM/yyyy");
                    if (ffrom  == "01/01/1900")
                    {
                        CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer._From.ToString("MM-dd-yyyy") + "</td>";
                    }
                    if (_NewConsumer._To.ToString("dd/MM/yyyy") == "01/01/1900")
                    {
                        CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>--/--/----</td>";
                    }
                    else
                    {
                        CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer._To.ToString("MM-dd-yyyy") + "</td>";
                    }
                    
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.MaxHours + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.PresenterName + "</td>";
                    CurrentSection += "<td style='padding-top: 10px; padding-bottom: 10px; border-bottom:solid 1px black;' class='text-center'>" + _NewConsumer.ParentFullName + "</td>";
                    CurrentSection += "</tr>";
                    CountRow++;
                    CurrentRow++;
                 
                }


                if (!string.IsNullOrEmpty(CurrentSection))
                {
                    sections.Add(CurrentSection);
                }
                _htmlString = _htmlString.Replace("[[Logo]]", _pathLogo);
                _htmlString = _htmlString.Replace("[[Date]]", _From.ToString("MM-dd-yyyy") + " - " + _To.ToString("MM-dd-yyyy"));
                _htmlString = _htmlString.Replace("[[DatePrint]]", DateTime.Now.ToString("MM/dd/yyyy"));

                PdfPageSize pageSize = PdfPageSize.Letter;
                PdfPageOrientation pdfPageOrientation = PdfPageOrientation.Portrait;
                int webPageWidth = 1024;
                int webPageHeight = 0;

                HtmlToPdf converter = new HtmlToPdf();
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfPageOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;

                SelectPdf.PdfDocument doc = new SelectPdf.PdfDocument();
                foreach (var section in sections)
                {
                    string sectionHtml = _htmlString.Replace("[[Table]]", section);
                    SelectPdf.PdfDocument sectionDoc = converter.ConvertHtmlString(sectionHtml, "");
                    foreach (SelectPdf.PdfPage page in sectionDoc.Pages)
                    {
                        doc.AddPage(page);
                    }
                }
                using (var stream = new System.IO.MemoryStream())
                {
                    doc.Save(stream);
                    stream.Seek(0, System.IO.SeekOrigin.Begin);
                    result = new FileContentResult(stream.ToArray(), "application/pdf");
                    result.FileDownloadName = "Rpt_NewConsumer_" + DateTime.Now.ToShortDateString() + ".pdf";
                }
                doc.Close();
                return result;
            }
            catch (Exception ex)
            {
                return result;
                throw;
            }
        }







    }
}