using LEAP.Data;
using LEAP.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Bibliography;

namespace LEAP.Controllers
{

    public class ConsumerController : Controller
    {
        ConsumerModel _consumer = new ConsumerModel();
        RegionalCenterModel _regional = new RegionalCenterModel();
        CitiesModel _cities = new CitiesModel();
        SpecialistModel _specialist = new SpecialistModel();
        LanguajesModel _lang = new LanguajesModel();
        NotesxConsumerModel _note_c = new NotesxConsumerModel();
        AuthorizationModel _Auth = new AuthorizationModel();
        ServiceCoordinatorModel _service = new ServiceCoordinatorModel();
        //private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<ConsumerModel> consumers = _consumer.Get_Consumer();

            return View(consumers);

        }
        public ActionResult Add()
        {
            return View();

        }
        public ActionResult Birthdays()
        {
            return View();

        }


        public string datos(string _UCI, int _specialist1, int _specialist2, int _specialist3, string _PresenterName,
        string _Name, string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
        string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
        int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _CB,bool _PEP,
        DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _Report5, DateTime _ReportClose,
        DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
        int _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _ConsumerActive, string _ConsumerNotes,
        int? _AuthNumber, DateTime? _AuthFrom, DateTime? _AuthTo, bool _OTPT, DateTime _terminationdate, string _state)
        {
            string salida = "";
            var usuario=User.Identity.Name;
            try
            {
                _consumer.Create_Consumer(_UCI, _specialist1, _specialist2, _specialist3, _PresenterName, _Name,
                _LastName, _DateOfBirth, _adjage, _Gender, _Address, _CityID, _ZipCode, _Phone,
                _EmergencyPhone, _ParentName, _ParentLastName, _LanguajeID, _Reasonforreferral, _AuthID,
                _HoursxWeek, _MaxHours, _TerminationNumber, _AdditionalEval, _InHome, _EIWITH, _CB,_PEP,
                _initEval, _evaldueby, _Report1, _Report2, _Report3, _Report4,_Report5, _ReportClose,
                _Date, _ReferredBy, _evaluation, _TypeReporte, _RegionalID, _ServiceCoordinator,
                1, _Type, _Action, _UserC, _DateC, _UserU, _DateU, _OTPT,_terminationdate, _state);
                salida = "1";
                if (_ConsumerNotes == "")
                {
                }
                else
                {
                    _note_c.AddNotesxConsumer(_UCI, _ConsumerNotes, Convert.ToBoolean(_ConsumerActive), _UserC);
                }
                if (_AuthNumber == 0)
                {

                }
                else
                {
                    _Auth.AddAuthorization(_UCI, _AuthNumber.ToString(), _AuthFrom, _AuthTo,1, _UserC);
                }



            }
            catch (Exception e)
            {

                salida = e.Message;
            }
            return salida;
        }
        /* public string datos(string _UCI, int _specialist1, int _specialist2, int _specialist3, string _PresenterName,
             string _Name, string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
             string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
             int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _OTPT,
             DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _ReportClose,
             DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
             bool _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _ConsumerActive,string _ConsumerNotes,
             string _TeacherSpecialist,DateTime _TeacherDate, string _TeacherDuration,string _TeacherPresentInSession,string _TeacherNote)
         {
             string salida = "";
             try
             {
              //   _consumer.Create_Consumer(_UCI, _specialist1, _specialist2, _specialist3, _PresenterName, _Name,
              //_LastName, _DateOfBirth, _adjage, _Gender, _Address, _CityID, _ZipCode, _Phone,
              //_EmergencyPhone, _ParentName, _ParentLastName, _LanguajeID, _Reasonforreferral, _AuthID,
              //_HoursxWeek, _MaxHours, _TerminationNumber, _AdditionalEval, _InHome, _EIWITH, _OTPT,
              //_initEval, _evaldueby, _Report1, _Report2, _Report3, _Report4, _ReportClose,
              //_Date, _ReferredBy, _evaluation, _TypeReporte, _RegionalID, _ServiceCoordinator,
              //_Archive, _Type, _Action, _UserC, _DateC, _UserU, _DateU);
                 salida = "1";
                 if (_ConsumerNotes == "")
                 {
                 }
                 else
                 {
                     _note_c.AddNotesxConsumer(_UCI, _ConsumerNotes, Convert.ToBoolean(_ConsumerActive), _UserC);
                 }
                 if (_TeacherNote == "")
                 {

                 }
                 else
                 {
                     _note_t.AddNotesxMaestro(_UCI, Convert.ToInt32(_TeacherSpecialist), _TeacherDate, _TeacherDuration, _TeacherPresentInSession, _TeacherNote, _UserC);
                 }



             }
             catch (Exception e)
             {

                 salida = e.Message;
             }
             return salida;
         }*/
        public string GetRegionals()
        {

            string salida = "";
            List<RegionalCenterModel> dr = _regional.Get_RegionalCenter();

            foreach (var item in dr)
            {
                salida += item.IDRegionalCenter + "," + item.RegionalCenter + "," + item.Address + "," + item.City + "," + item.State + "," + item.ZipCode + "," + item.Phone + ";" + Environment.NewLine;


            }

            return salida;

        }
        public string GetLang()
        {

            string salida = "";
            List<LanguajesModel> dr = _lang.Get_Languajes();

            foreach (var item in dr)
            {
                salida += item.IDLanguaje  + "," + item.Languaje + ";" + Environment.NewLine;


            }

            return salida;

        }
        
        public string GetConsumerBirthdays(string Month)
        {

            string salida = "";
            List<BirthdaysModel> dr = _consumer.Get_births(Month);

            foreach (var item in dr)
            {
                salida += item.UCI + "," + item.Name + "," + item.LastName + "," + item.Month + "," + item.DateOfBirth + "," + item.Type + ";" + Environment.NewLine;


            }

            return salida;

        }

        public string Getcities()
        {

            string salida = "";
            List<CitiesModel> dr = _cities.Get_Cities();

            foreach (var item in dr)
            {
                salida += item.IDCity + "," + item.City + ";" + Environment.NewLine;


            }

            return salida;

        }

        public string Getspecialist()
        {

            string salida = "";
            List<SpecialistModel> dr = _specialist.Get_Specialist();

            foreach (var item in dr)
            {
                salida += item.IDSpecialist + "," + item.Name + "," + item.LastName + ";" + Environment.NewLine;


            }

            return salida;

        }
        public string consumerCreate(string _UCI)
        {

            string salida = "";


            return salida;

        }
        public string GetRegionalByID(int id)
        {

            string salida = "";
            List<RegionalCenterModel> dr = _regional.Get_RegionalByID(id);

            foreach (var item in dr)
            {
                salida += item.IDRegionalCenter + "|" + item.RegionalCenter + "|" + item.Address + "|" + item.City + "|" + item.State + "|" + item.ZipCode + "|" + item.Phone + "|" + item.serviceC + "|" + item.telephone + "|" + item.IDServiceC + ";" + Environment.NewLine;


            }

            return salida;

        }
    
        public ActionResult Delete(int id)
        {
            var List_NotesC = _consumer.Get_ConsumerByID(id);
            return View(List_NotesC);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            var valid = _consumer.DeleteConsumer(id, User.Identity.Name);
            if (valid)
            {
                return RedirectToAction("Index", "Consumer");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Update(int id)
        {
            ConsumerModel consumers = _consumer.Get_ConsumerByID(id);
            consumers.Age = CalcularEdad(consumers.DateOfBirth,DateTime.Now);

            return View(consumers);
        }
        [HttpPost]
        public bool AgregarImagenXConsumer(string _UCI, HttpPostedFileBase C_image)
        {
            if (C_image != null && C_image.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(C_image.InputStream))
                {
                    byte[] imageBytes = binaryReader.ReadBytes(C_image.ContentLength);
                    string base64String = Convert.ToBase64String(imageBytes);

                    _consumer.AgregarImagenXConsumer(_UCI, base64String, User.Identity.Name);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

     
      

      

        [HttpPost]
        public bool ChangeStatusR4A(string _uci)
        {
            if (_uci != "")
            {
                bool response = _consumer.ChangeR4A(_uci, 2, User.Identity.Name);
                return response;
            }
            else
            {
                return false;
            }
        }
        public ActionResult Ready4Archive()
        {
            List<ConsumerModel> consumers = _consumer.Get_Consumer4Status(2);
            return View(consumers);
        }


        //SE LLAMA CUANDO ES R4A
        public ActionResult Details(int id)
        {
            var List_NotesC = _consumer.Get_ConsumerByID(id);
           List_NotesC.Age = CalcularEdad(List_NotesC.DateOfBirth, DateTime.Now);
            return View(List_NotesC);
        }
        [HttpPost]
        public bool ChangeStatusToActive(string _uci)
        {
            if (_uci != "")
            {
                bool response = _consumer.ChangeR4A(_uci, 1, User.Identity.Name);
                return response;
            }
            else
            {
                return false;
            }
        }
        [HttpPost]
        public bool ChangeStatusToArchive(string _uci)
        {
            if (_uci != "")
            {
                bool response = _consumer.ChangeR4A(_uci, 3, User.Identity.Name);
                return response;
            }
            else
            {
                return false;
            }
        }

        public ActionResult Archived()
        {
            List<ConsumerModel> consumers = _consumer.Get_Consumer4Status(3);
            return View(consumers);
        }

        public ActionResult DetailsArchived(int id)
        {
            var List_NotesC = _consumer.Get_ConsumerByID(id);
            return View(List_NotesC);
        }
        [HttpPost]
        public bool UpdateImagenXConsumer(string _UCI, HttpPostedFileBase C_image)
        {
            if (C_image != null && C_image.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(C_image.InputStream))
                {
                    byte[] imageBytes = binaryReader.ReadBytes(C_image.ContentLength);
                    string base64String = Convert.ToBase64String(imageBytes);

                    _consumer.AgregarImagenXConsumer(_UCI, base64String, User.Identity.Name);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public string datosUpdate(string _UCI, int? _specialist1, int? _specialist2, int? _specialist3, string _PresenterName,
       string _Name, string _LastName, DateTime _DateOfBirth, DateTime _adjage, string _Gender, string _Address, string _CityID, string _ZipCode, string _Phone,
       string _EmergencyPhone, string _ParentName, string _ParentLastName, int _LanguajeID, string _Reasonforreferral, int _AuthID,
       int _HoursxWeek, int _MaxHours, int _TerminationNumber, string _AdditionalEval, bool _InHome, bool _EIWITH, bool _CB, bool _PEP,
       DateTime _initEval, DateTime _evaldueby, DateTime _Report1, DateTime _Report2, DateTime _Report3, DateTime _Report4, DateTime _Report5, DateTime _ReportClose,
       DateTime _Date, int _ReferredBy, string _evaluation, string _TypeReporte, int _RegionalID, string _ServiceCoordinator,
       int _Archive, string _Type, int _Action, string _UserC, DateTime _DateC, string _UserU, DateTime _DateU, bool _OTPT,DateTime _terminationdate, string _state)
         {
            string salida = "";
            var usuario = User.Identity.Name;
            try
            {
                _consumer.Update_Consumer(_UCI, _specialist1, _specialist2, _specialist3, _PresenterName, _Name,
                _LastName, _DateOfBirth, _adjage, _Gender, _Address, _CityID, _ZipCode, _Phone,
                _EmergencyPhone, _ParentName, _ParentLastName, _LanguajeID, _Reasonforreferral, _AuthID,
                _HoursxWeek, _MaxHours, _TerminationNumber, _AdditionalEval, _InHome, _EIWITH, _CB,_PEP,
                _initEval, _evaldueby, _Report1, _Report2, _Report3, _Report4,_Report5, _ReportClose,
                _Date, _ReferredBy, _evaluation, _TypeReporte, _RegionalID, _ServiceCoordinator,
                1, _Type, _Action, _UserC, _DateC, _UserU, _DateU, _OTPT,_terminationdate, _state);
                salida = "1";
            }
            catch (Exception e)
            {

                salida = e.Message;
            }
            return salida;
        }
        public string edad(DateTime dateOfBirth)
        {
            DateTime birth = dateOfBirth;
            DateTime today = DateTime.Now;
            TimeSpan span = today - birth;
            DateTime age = DateTime.MinValue + span;

            // Make adjustment due to MinValue equalling 1/1/1int years = age.Year - 1;
            int months = age.Month - 1;
            var meses = (age.Year - 1) * 12;
            var final = Convert.ToInt32(months) + Convert.ToInt32(meses);
            int days = age.Day - 2;

            // Print out not only how many years old they are but give months and days as well
            var ageInYMD = final + " months";
            var ageInYM = string.Format("{0} years, {1} months", age.Year, months);
            var ageInY = string.Format("{0} years", age.Year);
            var FechaFinal = string.Format("{0} years, {1} months,  {2} days", age.Year -1, months, days);
            return FechaFinal;
        }
        //public string CalcularEdad(DateTime fechaNacimiento, DateTime fechaActual)
        //{
        //    int años = fechaActual.Year - fechaNacimiento.Year;
        //    int meses = fechaActual.Month - fechaNacimiento.Month;
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
        public string GetservByID(int id)
        {

            string salida = "";
            List<ServiceCoordinatorModel> dr = _service.Get_ServiceCbyID(id);

            foreach (var item in dr)
            {
                salida += item.IDServiceC + "|" + item.Tel + ";" + Environment.NewLine;


            }

            return salida;

        }
        public ActionResult Archall()
        {

           _consumer.ArchiveAll();
            string salida = "Done";

            
             return RedirectToAction("Ready4Archive", "Consumer");

        }
        public string GetNextConsumerByUCI(int id)
        {
            ConsumerModel dr = _consumer.GetNextConsumerByUCI(id);
            return dr.IDConsumer.ToString();
        }
        public string GetPrevConsumerByUCI(int id)
        {
            ConsumerModel dr = _consumer.GetPrevConsumerByUCI(id);
            return dr.IDConsumer.ToString();
        }
    }
}
