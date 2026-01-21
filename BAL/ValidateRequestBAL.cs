namespace BAL
{
    using DAL;
    using PAL;
    using System;
    using System.Data;
    using System.Runtime.InteropServices;
    using Utility;

    using System.Data.SqlClient;
    using System.Configuration;
    public class ValidateRequestBAL : ValidateRequestPAL
    {
        //public static bool ValidateClientRequest(long UserID, string SecretKey, string AuthToken)
        //{
        //    bool returnVal = false;
        //    try
        //    {
                
        //        if (SecretKey == null || AuthToken == null || UserID == 0 || SecretKey == string.Empty || AuthToken == string.Empty)
        //        {
        //            return false;
        //        }
        //        if (Convert.ToString(UserID) != Utility.Security.Rijndael128Algorithm.DecryptString(SecretKey))
        //        {
        //            return false;
        //        }
        //        using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
        //        {
        //            var cmd = Conn.CreateCommand();
        //            cmd.CommandTimeout = 3600;
        //            cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //            cmd.CommandText = "SalesAdminValidateClientRequest";

        //            cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
        //            cmd.Parameters.Add(new SqlParameter("@AuthToken", AuthToken));                    
        //            cmd.Parameters.Add(new SqlParameter("@ReturnVal", 0));
        //            cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

        //            Conn.Open();
        //            cmd.ExecuteNonQuery();

        //            if (cmd.Parameters["@ReturnVal"].Value != null)
        //            {
        //                if (Convert.ToInt32(cmd.Parameters["@ReturnVal"].Value) == 1)
        //                {
        //                    returnVal = true;
        //                }
        //            }
        //            else
        //            {
        //                returnVal = false;
        //            }
        //            Conn.Close();
        //        }
        //        returnVal = true;               
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    return returnVal;         
        //}

        public static bool BusinessValidateClientRequest(long UserID, string SecretKey, string AuthToken)
        {
            bool returnVal = false;
            try
            {

                if (SecretKey == null || AuthToken == null || UserID == 0 || SecretKey == string.Empty || AuthToken == string.Empty)
                {
                    return false;
                }
                if (Convert.ToString(UserID) != Utility.Security.Rijndael128Algorithm.DecryptString(SecretKey))
                {
                    return false;
                }
                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "BusinessValidateClientRequest";

                    cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                    cmd.Parameters.Add(new SqlParameter("@AuthToken", AuthToken));
                    cmd.Parameters.Add(new SqlParameter("@ReturnVal", 0));
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

                    Conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ReturnVal"].Value != null)
                    {
                        if (Convert.ToInt32(cmd.Parameters["@ReturnVal"].Value) == 1)
                        {
                            returnVal = true;
                        }
                    }
                    else
                    {
                        returnVal = false;
                    }
                    Conn.Close();
                }
              //  returnVal = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnVal;
        }

        public static bool UserValidateClientRequest(long UserID, string SecretKey, string AuthToken, string Suburb = "")
        {
            bool returnVal = false;
            try
            {

                if (SecretKey == null || AuthToken == null || UserID == 0 || SecretKey == string.Empty || AuthToken == string.Empty)
                {
                    return false;
                }
                if (Convert.ToString(UserID) != Utility.Security.Rijndael128Algorithm.DecryptString(SecretKey))
                {
                    return false;
                }
                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "UserValidateClientRequest";

                    cmd.Parameters.Add(new SqlParameter("@UserID", UserID));
                    cmd.Parameters.Add(new SqlParameter("@AuthToken", AuthToken));
                    cmd.Parameters.Add(new SqlParameter("@ReturnVal", 0));
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

                    Conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ReturnVal"].Value != null)
                    {
                        if (Convert.ToInt32(cmd.Parameters["@ReturnVal"].Value) == 1)
                        {
                            returnVal = true;
                        }
                    }
                    else
                    {
                        returnVal = false;
                    }
                    Conn.Close();
                }
              //  returnVal = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnVal;
        }

        public static bool VerifyBusinessEmail(string Email)
        {
            bool returnVal = false;
            try
            {
                                
                using (SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    var cmd = Conn.CreateCommand();
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "BusinessVerifyEmailAddress";

                    cmd.Parameters.Add(new SqlParameter("@EmailAddress", Email));
                    cmd.Parameters.Add(new SqlParameter("@ReturnVal", 0));
                    cmd.Parameters["@ReturnVal"].Direction = ParameterDirection.Output;

                    Conn.Open();
                    cmd.ExecuteNonQuery();

                    if (cmd.Parameters["@ReturnVal"].Value != null)
                    {
                        if (Convert.ToInt32(cmd.Parameters["@ReturnVal"].Value) == 1)
                        {
                            returnVal = true;
                        }
                        else
                        {
                            returnVal = false;
                        }
                    }
                    else
                    {
                        returnVal = false;
                    }
                    Conn.Close();
                }
              //  returnVal = true;
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnVal;
        }

        public static bool UserValidateClientRequestWithoutLogin(string SecretKey, string AuthToken)
        {
            bool returnVal = true;
            try
            {

                if (SecretKey == null || AuthToken == null || SecretKey == string.Empty || AuthToken == string.Empty)
                {
                    returnVal = false;
                }
                if (Convert.ToString(SecretKey) != Convert.ToString(ConfigurationManager.AppSettings["SecretKey"]) || Convert.ToString(AuthToken) != Convert.ToString(ConfigurationManager.AppSettings["AuthToken"]))
                {
                    returnVal = false;
                }
                         
            }
            catch (Exception ex)
            {
                return false;
            }
            return returnVal;
        }

    }
}

