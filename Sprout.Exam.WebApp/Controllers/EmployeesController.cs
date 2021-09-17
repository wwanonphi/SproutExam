using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using GTOPPER_ClassLibrary;
namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            GTopperServiceClient client = new GTopperServiceClient();
            string sql = @"USE SPROUTEXAMDB; SELECT id,employeetypeid,fullname,convert(varchar(20),birthdate,101) as Birthdate,TIN FROM EMPLOYEE 
                            WHERE ISDELETED=0";
            string[] param = { };
            string[] val = { };
            string[] col = { "id","EmployeeTypeId","Fullname","Birthdate","TIN"};
            List<Dictionary<string, string>> toRet = await client.SQLGetData(sql, param, val, col);
            return Ok(toRet);

            //var result = await Task.FromResult(StaticEmployees.ResultList);
            //return Ok(result);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            GTopperServiceClient client = new GTopperServiceClient();
            string sql = @"USE SPROUTEXAMDB; SELECT id,EmployeeTypeId,Fullname,convert(varchar(10),Birthdate,120) as Birthdate,TIN  FROM EMPLOYEE  
                            WHERE  ID=@ID";
            string[] param = {"@ID" };
            string[] val = { id.ToString()};
            string[] col = {"id", "EmployeeTypeId", "Fullname", "Birthdate", "TIN" };
            List<Dictionary<string, string>> toRet = await client.SQLGetData(sql, param, val, col);
            return Ok(toRet);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            //var item = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == input.Id));
            //if (item == null) return NotFound();
            //item.FullName = input.FullName;
            //item.Tin = input.Tin;
            //item.Birthdate = input.Birthdate.ToString("yyyy-MM-dd");
            //item.TypeId = input.TypeId;
            //return Ok(item); 

            GTopperServiceClient client = new GTopperServiceClient();
            string sql = @"USE SPROUTEXAMDB;  UPDATE EMPLOYEE SET  fullname=@fullname,birthdate=convert(date,@birthdate),tin=@tin,employeetypeid=convert(int,@typeid) WHERE ID=@ID";
            string[] param = { "@ID","@fullname","@birthdate","@tin","@typeid" };
            string[] val = { input.Id.ToString(),input.FullName,input.Birthdate.ToString(),input.Tin,input.TypeId.ToString() };
            bool isSuccessUpdate = await client.SQLUpdateData(sql, param, val);
            if (isSuccessUpdate)
            {
                return Ok(200);
            }
            else
            {
                return BadRequest(400);
            }


        }





        public async Task<string> GetMaxID()
        {

            string returndValue = "";
            GTopperServiceClient client = new GTopperServiceClient();
                   string sql = @"USE SPROUTEXAMDB; select case 
                                   when max(id) is null then '1' else max(id) end as cnt
                                   from employee  ";
            string[] param = { };
            string[] val = { };
            string[] col = { "cnt" };
            List<Dictionary<string, string>> retVals = await client.SQLGetData(sql, param, val, col);
            foreach(Dictionary<string,string> retVal in retVals)
            {
                returndValue = retVal.GetValueOrDefault("cnt").ToString();
            }
            return returndValue;
           
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {

            var id = await GetMaxID();
            GTopperServiceClient client = new GTopperServiceClient();
            string sql = @"USE SPROUTEXAMDB; INSERT INTO EMPLOYEE ( FULLNAME,TIN,BIRTHDATE,EMPLOYEETYPEID,ISDELETED)
                            VALUES
                            ( @FULLNAME,@TIN,CONVERT(DATE,@BIRTHDATE),CONVERT(INT,@EMPLOYEETYPEID),CONVERT(BIT,@ISDELETED))";
            string[] param = {  "@FULLNAME","@TIN","@BIRTHDATE","@EMPLOYEETYPEID","@ISDELETED" };
            string[] val = {  input.FullName,input.Tin,input.Birthdate.ToString(),input.TypeId.ToString(),"0" }; 
            bool isSuccessInsert = await client.SQLInsertData(sql, param, val);
            if (isSuccessInsert)
            {
                return Created($"/api/employees/{id}", id);
            }
            else
            {
                return BadRequest(400);
            }

    
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete/user/{id}")]
        public async Task<IActionResult> Delete(int id)
        { 
            GTopperServiceClient client = new GTopperServiceClient();
            string sql = @"USE SPROUTEXAMDB;  UPDATE EMPLOYEE SET ISDELETED='1' WHERE ID=@ID";
            string[] param = { "@ID" };
            string[] val = { id.ToString() };
            bool isSuccessUpdate = await client.SQLUpdateData(sql, param, val);
            if (isSuccessUpdate)
            {
                return Ok(200);
            }
            else
            {
                return BadRequest(400);
            }

            //var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == id));
            //if (result == null) return NotFound();
            //StaticEmployees.ResultList.RemoveAll(m => m.Id == id);
            //return Ok(id);
        }


        [HttpGet]
        [Route("type")]
        public async Task<IActionResult> Get_EmployeeType()
        {
            GTopperServiceClient client = new GTopperServiceClient();
            string sql = @"USE SPROUTEXAMDB;  SELECT * FROM EMPLOYEETYPE";
            string[] param = {   };
            string[] val = {  };
            string[] col = { "id","TypeName"};
            List<Dictionary<string,string>> toRet= await client.SQLGetData(sql, param, val,col);
            return Ok(toRet);  
        }



        [HttpGet]
        [Route("{id}/salary/detail")]
        public async Task<IActionResult> Get_Employee_Salary_Detail([FromRoute]string id)
        {
            string emptypeID = await Get_Employee_EmpType(id); 
            Dictionary<string, string> CalculationData = await Get_Employee_ComputationsTable(emptypeID); 
            return Ok(CalculationData);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("salary/calculate")]
        public async Task<IActionResult> Calculate([FromBody]SalaryCalculationModel m)
        { 
            string emptypeID = await Get_Employee_EmpType(m.id.ToString());

            Dictionary<string, string> CalculationData = await Get_Employee_ComputationsTable(emptypeID); 
            bool isPayableperDay =Convert.ToBoolean( CalculationData.GetValueOrDefault("RetPayablePerDay")); 
            double tax = Convert.ToDouble(CalculationData.GetValueOrDefault("RetTax"));
            double rate = Convert.ToDouble(CalculationData.GetValueOrDefault("RetRatePerDay"));
            double monthlygross = Convert.ToDouble(CalculationData.GetValueOrDefault("RetMonthlyGross"));

            if (!isPayableperDay)
            {
                //Monthly
                double totalAbsence = (monthlygross / 22) * Convert.ToDouble(m.absentDays);
                double grossIncome = monthlygross - totalAbsence;
                double taxPercentage = tax / 100;
                double taxCalculation = monthlygross * taxPercentage;
                double netIncome = grossIncome - taxCalculation;

                return Ok(Math.Round(netIncome,2));
            }
            else
            {
                //per day
                double netIncome = rate * Convert.ToDouble(m.workedDays);
                return Ok(Math.Round(netIncome, 2));

            } 

        }



        public async Task<string> Get_Employee_EmpType(string empid)
        {
            GTopperServiceClient client = new GTopperServiceClient();

            string sql = @"USE SPROUTEXAMDB; SELECT employeetypeid as employeetypeid  from employee where id=@id";
            string[] param = { "@ID" };
            string[] val = { empid };
            string[] col = { "employeetypeid" };
            List<Dictionary<string, string>> returnedData = await client.SQLGetData(sql, param, val, col); 
            string EmpTypeID = "";
            foreach (Dictionary<string, string> x in returnedData)
            { 
                EmpTypeID = x.GetValueOrDefault("employeetypeid").ToString(); 
            }

            return EmpTypeID;

        }

        public async Task<Dictionary<string,string>> Get_Employee_ComputationsTable(string EMPTYPEID)
        {
            GTopperServiceClient client = new GTopperServiceClient();
         
            string sql = @"USE SPROUTEXAMDB; SELECT top 1 * FROM COMPUTATIONS where emptypeid=@ID  ";
            string[] param = { "@ID"};
            string[] val = { EMPTYPEID };
            string[] col = {"Tax","PayablePerDay","RatePerDay","MonthlyGross" };
            List<Dictionary<string, string>> returnedData = await client.SQLGetData(sql, param, val, col);

            Dictionary<string, string> toRetData = new Dictionary<string, string>();
            foreach (Dictionary<string,string> x in returnedData)
            {
            
                toRetData.Add("RetTax", x.GetValueOrDefault("Tax").ToString());
                toRetData.Add("RetPayablePerDay", x.GetValueOrDefault("PayablePerDay").ToString());
                toRetData.Add("RetRatePerDay", x.GetValueOrDefault("RatePerDay").ToString());
                toRetData.Add("RetMonthlyGross", x.GetValueOrDefault("MonthlyGross").ToString());
            }

            return toRetData;

        }
    }
}
