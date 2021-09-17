using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using System.Threading.Tasks; 
 using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.DirectoryServices;
using System.Diagnostics;
using Novell.Directory.Ldap;
using IniParser;
using IniParser.Model;
using System.IO;

namespace GTOPPER_ClassLibrary
{
     public class GTopperServiceClient
    {
        //check data exist 
        //insert with parameter 
        //update with parameter 
        //get data with parameter

     

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
      
        public static SqlConnection MyConnection()
        {
            string conn=  GTOPPER_ENCRYPTOR.GetConn();
            SqlConnection sqlconnect = new SqlConnection(conn); 
            return sqlconnect;
        }

        public async   Task<bool> isExist(string query,string[] param,string[] paramval)
        {
            
            SqlConnection sqlconn = MyConnection();
            int paramKey = param.Length;
            int paramVal = paramval.Length;
            if(paramKey != paramVal)
            {
                 
                return false;
            }
            SqlCommand cmd = new SqlCommand(query);
            try
            { 
              
                List<SqlParameter> arrSP = new List<SqlParameter>();
                int i = 0;
                foreach(string paramkey in param)
                {
                     
                        SqlParameter sp = new SqlParameter();
                        sp.ParameterName = paramkey;
                        sp.SqlDbType = System.Data.SqlDbType.VarChar;
                        sp.Direction = System.Data.ParameterDirection.Input;
                        sp.Value = paramval[i];
                        arrSP.Add(sp); 
                    i++;
                }
                foreach(SqlParameter sqlparam in arrSP)
                {
                    cmd.Parameters.Add(sqlparam);
                }
                cmd.Connection = sqlconn;
                cmd.Connection.Open();
                //SqlDataReader rdr =await cmd.ExecuteReaderAsync();
                SqlDataReader rdr =  cmd.ExecuteReader();

                if (rdr.Read())
                {
                    cmd.Connection.Close();
                    
                    return true;
                }
                else
                {
                    cmd.Connection.Close();
              
                    return false;
                }
            }
            catch (Exception ex)
            { 
                cmd.Connection.Close();
        
                return false;
            }  
        }
         
        public async   Task<bool> SQLInsertData(string query,  string[] param, string[] paramval)
        {
     
            SqlConnection sqlconn = MyConnection();
          
          
            int paramKey = param.Length;
            int paramVal = paramval.Length;
            if (paramKey != paramVal)
            {
                 
                return false;
            }
            using (SqlCommand cmd = new SqlCommand(query))
            {
                try
                {
                    List<SqlParameter> arrSP = new List<SqlParameter>();
                    int i = 0;
                    foreach (string paramkey in param)
                    {

                        SqlParameter sp = new SqlParameter();
                        sp.ParameterName = paramkey;
                        sp.SqlDbType = System.Data.SqlDbType.VarChar;
                        sp.Direction = System.Data.ParameterDirection.Input;
                        sp.Value = paramval[i];
                        arrSP.Add(sp);

                        i++;
                    }
                    foreach (SqlParameter sqlparam in arrSP)
                    {
                        cmd.Parameters.Add(sqlparam);
                    }
                    cmd.Connection = sqlconn;
                    cmd.Connection.Open();
             
                    int rowAffected = await cmd.ExecuteNonQueryAsync();
                    //int rowAffected=  cmd.ExecuteNonQuery();

                    if (rowAffected >= 1)
                    { 
                        cmd.Connection.Close();
                
                        return true;
                    }
                    else
                    {
                        cmd.Connection.Close();
                   
                        return false;
                    }
                }   
                catch (Exception ex)
                {
                   
                    cmd.Connection.Close();
                    
                    return false;
                }

            }

        } 
        
        public async   Task<bool> SQLUpdateData(string query, string[] param, string[] paramval)
        {
            
            SqlConnection sqlconn = MyConnection();
            int paramKey = param.Length;
            int paramVal = paramval.Length;
            if (paramKey != paramVal)
            {
               
                return false;
            }
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                List<SqlParameter> arrSP = new List<SqlParameter>();
                int i = 0;
                foreach (string paramkey in param)
                {
                    
                        SqlParameter sp = new SqlParameter();
                        sp.ParameterName = paramkey;
                        sp.SqlDbType = System.Data.SqlDbType.VarChar;
                        sp.Direction = System.Data.ParameterDirection.Input;
                        sp.Value = paramval[i];
                        arrSP.Add(sp);
                    i++;
                }
                foreach (SqlParameter sqlparam in arrSP)
                {
                    cmd.Parameters.Add(sqlparam);
                }
                cmd.Connection = sqlconn;
                cmd.Connection.Open();
                int rowAffected =await cmd.ExecuteNonQueryAsync();

                if (rowAffected >= 1)
                {
                    cmd.Connection.Close();
                
                    return true;
                }
                else
                {
                    cmd.Connection.Close();
              
                    return false;
                }
            }
            catch (Exception ex)
            {
             
                cmd.Connection.Close();
            
                return false;
            }

        }
        
        public async   Task<List<Dictionary<string, string>>> SQLGetData(string query, string[] param, string[] paramval,string[] ColumnsName)
        {
            List<Dictionary<string, string>> lst = new List<Dictionary<string, string>>();
            SqlConnection sqlconn = MyConnection();
            int paramKey = param.Length;
            int paramVal = paramval.Length;
            if (paramKey != paramVal)
            {
                //Dictionary<string, string> status = new Dictionary<string, string>();
                //status.Add("Status", "Falsed");
                //lst.Add(status);
                return lst;
            }
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                List<SqlParameter> arrSP = new List<SqlParameter>();
                int i = 0;
                foreach (string paramkey in param)
                {
                    
                        SqlParameter sp = new SqlParameter();
                        sp.ParameterName = paramkey;
                        sp.SqlDbType = System.Data.SqlDbType.VarChar;
                        sp.Direction = System.Data.ParameterDirection.Input;
                        sp.Value = paramval[i];
                        arrSP.Add(sp);
                    i++;
                }
                foreach (SqlParameter sqlparam in arrSP)
                {
                    cmd.Parameters.Add(sqlparam);
                }
                cmd.Connection = sqlconn;
                cmd.Connection.Open();
                SqlDataReader rdr =await cmd.ExecuteReaderAsync();
                List<object> retData = new List<object>();

              
                if (rdr.HasRows)
                {
                    //while rdr.read then iterate through the list
                    // on each row there is columns
                    //on each row create new dictionary
                    //on each columns add to the new dictionary

                    //Dictionary<string, string> status = new Dictionary<string, string>();
                    //status.Add("Status", "Okay");
                    //lst.Add(status);  
                    while (rdr.Read())
                    {
                        Dictionary<string, string> dict = new Dictionary<string, string>();
                        foreach(string columnName in ColumnsName)
                        {
                            dict.Add(columnName, rdr[columnName].ToString());
                        }
                        lst.Add(dict);
                    }
                     
                        
                    cmd.Connection.Close();
                    return lst;
                }
                else
                {
                    //Dictionary<string, string> status = new Dictionary<string, string>();
                    //status.Add("Status", "Falsed");
                    //lst.Add(status);
                    cmd.Connection.Close();
                    return lst;
                }
            }
            catch (Exception ex)
            {
                //Dictionary<string, string> status = new Dictionary<string, string>();
                //status.Add("Status", "Falsed");
                //lst.Add(status);
                Console.WriteLine(ex.Message);
                cmd.Connection.Close();
                return lst;
            }

        }
        
        public   string GetFolderToSave() 
        {
            string _filePath = Directory.GetCurrentDirectory();
            var data = new FileIniDataParser();
            IniData D = data.ReadFile(Path.Combine(_filePath, "settings.ini"));
            string environment = D["FOLDERENVIRONMENT"]["ENVIRONMENTX"].ToString();
            string toRetPath = environment == "DEV" ? D["FOLDERPATH-DEV"]["PATH"].ToString() : D["FOLDERPATH-PROD"]["PATH"].ToString();

            return toRetPath;
        }
    }
}
