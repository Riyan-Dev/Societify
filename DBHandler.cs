using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
namespace Societify
{
    class DBHandler
    {
        public static DataTable GetMentors()
        {
            DataTable mentorsTable = new DataTable();
            string connectionString = connectionStr.connectionString; // Replace this with your actual connection string

            string query = @"
            SELECT M.UserID AS MentorID, U.Name AS MentorName
            FROM Mentor M
            INNER JOIN Users U ON M.UserID = U.UserID;
        ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(mentorsTable);
                    }
                }
            }

            return mentorsTable;
        }
        public static void InsertMentor(string userID, string department)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    string query = @"
                INSERT INTO Mentor (UserID, Department)
                VALUES (@UserID, @Department)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Department", department);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public static void InsertStudent(string userID, string department, string batch)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    string query = @"
                INSERT INTO student (UserID, Department, Batch)
                VALUES (@UserID, @Department, @Batch)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Department", department);
                        command.Parameters.AddWithValue("@Batch", batch);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static void InsertUser(string userID, string name, string password, string email, DateTime dob)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    string query = @"
                INSERT INTO Users (UserID, Name, Password, Email, DOB)
                VALUES (@UserID, @Name, @Password, @Email, @DOB)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Password", password);
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@DOB", dob);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public static DataTable GetUserByEmailOrUserID(string userID, string email)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    // SQL query to retrieve user data based on UserID or Email
                    string query = @"
                SELECT *
                FROM Users
                WHERE UserID = @UserID OR Email = @Email";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@Email", email);

                        // Open the connection
                        connection.Open();

                        // Execute the query and fill the DataTable with the results
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dataTable;
        }

        public static int GetMemberCount(int societyID, int rollID, int teamID)
        {
            int memberCount = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();
                    string query = @"SELECT COUNT(*) AS memberCount 
                                 FROM societyMembers 
                                 WHERE societyID = @SocietyID 
                                 AND rollID = @RollID 
                                 AND teamID = @TeamID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.Parameters.AddWithValue("@RollID", rollID);
                        command.Parameters.AddWithValue("@TeamID", teamID);

                        memberCount = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return memberCount;
        }
        public static void DeleteMemberApprovalRequests(int societyID, int rollID, int teamID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();
                    string query = @"DELETE FROM MemberApprovalRequests 
                                 WHERE SocietyID = @SocietyID 
                                 AND rollID = @rollID 
                                 AND TeamID = @TeamID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.Parameters.AddWithValue("@rollID", rollID);
                        command.Parameters.AddWithValue("@TeamID", teamID);

                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"{rowsAffected} member approval requests deleted.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        public static void AddSocietyMember(int societyID, string userID, int rollID, int teamID, bool approved)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO societyMembers (SocietyID, UserID, rollID, TeamID)
                             VALUES (@SocietyID, @UserID, @RollID, @TeamID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.Parameters.AddWithValue("@UserID", userID);
                        command.Parameters.AddWithValue("@RollID", rollID);
                        command.Parameters.AddWithValue("@TeamID", teamID);
                        command.Parameters.AddWithValue("@Approved", approved);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        public static void DeleteMemberApprovalRequest(int reqID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM MemberApprovalRequests WHERE reqID = @ReqID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ReqID", reqID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }


        public static MemberApprovalRequest GetApprovalRequestByReqID(int reqID)
        {
            MemberApprovalRequest request = null;

            string query = @"
        SELECT
            reqID,
            SocietyID,
            UserID,
            rollID,
            TeamID,
            purpose,
            motivation,
            AboutYou,
            PastExp
        FROM
            MemberApprovalRequests
        WHERE
            reqID = @ReqID;
    ";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ReqID", reqID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            request = new MemberApprovalRequest();
                            request.ReqID = reader.GetInt32(0);
                            request.SocietyID = reader.GetInt32(1);
                            request.UserID = reader.GetString(2);
                            request.RollID = reader.GetInt32(3);
                            request.TeamID = reader.GetInt32(4);
                            request.Purpose = reader.GetString(5);
                            request.Motivation = reader.GetString(6);
                            request.AboutYou = reader.GetString(7);
                            request.PastExp = reader.GetString(8);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return request;
        }


        public static DataTable GetMemberApprovalRequests(int societyID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT mar.reqID, u.NAME AS UserName, t.teamName AS TeamName, sr.rollName AS RoleName
                    FROM MemberApprovalRequests mar
                    JOIN Users u ON mar.UserID = u.UserID
                    JOIN Teams t ON mar.TeamID = t.TeamID
                    JOIN societyRoles sr ON mar.rollID = sr.rollID
                    WHERE mar.SocietyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dt;
        }

        public static DataTable GetSocietyEventsWithStatus(int societyID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT
                        se.eventName,
                        se.Date,
                        se.registrationFee,
                        CASE
                            WHEN se.approved = 1 THEN 'Approved'
                            WHEN se.approved = 0 AND sar.reqID IS NULL THEN 'Rejected'
                            ELSE 'Approval Pending'
                        END AS Status
                    FROM
                        societyEvents se
                    LEFT JOIN
                        societyEventsApproval sar ON se.societyID = sar.societyID AND se.eventID = sar.reqID
                    WHERE
                        se.societyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dt;
        }
        public static void InsertSocietyEvent(int societyID, string eventName, string date, string registrationFee, string description)
        {
            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into societyEvents table
                    string insertEventQuery = @"
                INSERT INTO societyEvents (societyID, eventName, Date, registrationFee, Description, approved)
                VALUES (@SocietyID, @EventName, @Date, @RegistrationFee, @Description, 0);
            ";

                    using (SqlCommand command = new SqlCommand(insertEventQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.Parameters.AddWithValue("@EventName", eventName);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@RegistrationFee", registrationFee);
                        command.Parameters.AddWithValue("@Description", description);
                        command.ExecuteNonQuery();
                    }

                    // Insert into societyEventsApproval table
                    string insertEventApprovalQuery = @"
                INSERT INTO societyEventsApproval (societyID, eventName, Date, registrationFee, Description)
                VALUES (@SocietyID, @EventName, @Date, @RegistrationFee, @Description);
            ";

                    using (SqlCommand command = new SqlCommand(insertEventApprovalQuery, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.Parameters.AddWithValue("@EventName", eventName);
                        command.Parameters.AddWithValue("@Date", date);
                        command.Parameters.AddWithValue("@RegistrationFee", registrationFee);
                        command.Parameters.AddWithValue("@Description", description);
                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public static Society GetSocietyById(int societyId)
        {
            Society society = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Society WHERE SocietyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyId);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            society = new Society
                            {
                                SocietyID = Convert.ToInt32(reader["SocietyID"]),
                                SocietyName = reader["SocietyName"].ToString(),
                                MentorID = reader["MentorID"].ToString(),
                                CreationDate = Convert.ToDateTime(reader["CreationDate"]),
                                President_ID = reader["President_ID"].ToString(),
                                Approved = Convert.ToBoolean(reader["approved"]),
                                Description = reader["Description"].ToString()
                            };
                            Console.WriteLine(society.SocietyID);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return society;
        }

        public static void UpdateSocietyVerification(int societyID, bool verifiedValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();
                    string query = "UPDATE SocietyApprovalRequests SET Verified = @VerifiedValue WHERE societyID = @SocietyID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VerifiedValue", verifiedValue);
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Society approval status: " + ex.Message);
            }
        }

        public static void UpdateSocietyApproval(int societyID, bool approvedValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Society SET approved = @ApprovedValue WHERE SocietyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApprovedValue", approvedValue);
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Society approval status: " + ex.Message);
            }
        }

        public static void DeleteSocietyApprovalRequests(int societyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM SocietyApprovalRequests WHERE societyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting SocietyApprovalRequests: " + ex.Message);
            }
        }
        public static DataTable GetSocietyApprovalRequestDetails(int societyID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT sa.SocietyID, s.SocietyName, sa.purpose, sa.motivation, sa.AboutYou, sa.PastExp, sa.PlannedEvent
                    FROM SocietyApprovalRequests sa
                    INNER JOIN Society s ON sa.SocietyID = s.SocietyID
                    WHERE sa.SocietyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }
        public static DataTable GetSocietiesInApprovalRequests()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT sa.societyID, s.SocietyName, s.President_ID
                FROM SocietyApprovalRequests sa
                JOIN Society s ON sa.societyID = s.SocietyID
                WHERE sa.Verified = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }
        public static DataTable GetSocietiesVerificationRequests()
        {
            DataTable dt = new DataTable();
            string id = Constants.user.UserID;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {

                connection.Open();
                string query = @"
                SELECT sa.societyID, s.SocietyName, s.President_ID
                FROM SocietyApprovalRequests sa
                JOIN Society s ON sa.societyID = s.SocietyID 
                WHERE s.mentorID = @MentorID AND sa.Verified = 0"; 

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MentorID", id);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }


        public static void UpdateSocietyVerificationMentor(int societyID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Society SET MentorID = NULL WHERE SocietyID = @SocietyID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Society mentor verification: " + ex.Message);
            }
        }

        public static DataTable GetSocietiesInEventsApprovalRequests()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
            SELECT reqID, societyID, eventName
            FROM societyEventsApproval
            WHERE Verified = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }

        public static DataTable GetSocietiesInEventsVerificationRequests()
        {
            DataTable dt = new DataTable();
            String mentorID = Constants.user.UserID;   

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();
                    string query = @"
            SELECT sea.reqID, sea.societyID, sea.eventName
            FROM societyEventsApproval sea
            INNER JOIN Society s ON sea.societyID = s.SocietyID
            WHERE sea.Verified = 0 AND s.MentorID = @MentorID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MentorID", mentorID);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }
        public static void UpdateSocietyEventsVerification(int societyID,int eventID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();
                    string query = @"UPDATE sea SET sea.Verified = 1 FROM societyEventsApproval sea
                    INNER JOIN societyEvents se ON sea.societyID = se.societyID
                    WHERE se.societyID = @SocietyID AND se.eventID = @EventID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    { 
                        command.Parameters.AddWithValue("@SocietyID", societyID);
                        command.Parameters.AddWithValue("@eventID", eventID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Society approval status: " + ex.Message);
            }
        }
        public static void UpdateSocietyEventApproval(int societyID, int eventID, bool approvedValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "UPDATE societyEvents SET approved = @ApprovedValue WHERE societyID = @societyID AND eventID = @eventID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ApprovedValue", approvedValue);
                        command.Parameters.AddWithValue("@societyID", societyID);
                        command.Parameters.AddWithValue("@eventID", eventID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating Society event approval status: " + ex.Message);
            }
        }

        public static void DeleteSocietyEventApproval(int societyID, int eventID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM societyEventsApproval WHERE societyID = @societyID AND reqID = @eventID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@societyID", societyID);
                        command.Parameters.AddWithValue("@eventID", eventID);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting societyEventsApproval: " + ex.Message);
            }
        }

        public static void DeleteSocietyEventVerification(int societyID, int eventID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM societyEventsApproval WHERE societyID = @societyID AND reqID = @eventID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@societyID", societyID);
                        command.Parameters.AddWithValue("@eventID", eventID);
                        command.ExecuteNonQuery();
                    }

                    // Delete from societyEvents table
                    string eventsQuery = "DELETE FROM societyEvents WHERE societyID = @societyID AND eventID = @eventID";

                    using (SqlCommand eventsCommand = new SqlCommand(eventsQuery, connection))
                    {
                        eventsCommand.Parameters.AddWithValue("@societyID", societyID);
                        eventsCommand.Parameters.AddWithValue("@eventID", eventID);
                        eventsCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting societyEventsApproval: " + ex.Message);
            }
        }

        public static DataTable GetSocietyEventsApprovalDetails(int societyID, int eventID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT reqID, societyID,eventName, Date, registrationFee, Description
                    FROM societyEventsApproval
                    WHERE societyID = @societyID AND reqID = @eventID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@societyID", societyID);
                        command.Parameters.AddWithValue("@eventID", eventID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }

        public static DataTable GetSocietyNamesAndIDsForAdmin()
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "SELECT SocietyID, SocietyName, President_ID FROM Society Where approved = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }

        public static DataTable GetSocietyNamesAndIDsForMentor()
        {
            DataTable dt = new DataTable();
            string mentorID = Constants.user.UserID;   
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"SELECT S.SocietyID, S.SocietyName, S.President_ID, S.approved
FROM Society S
LEFT JOIN SocietyApprovalRequests SAR ON S.SocietyID = SAR.societyID
WHERE S.MentorID = @MentorID
AND (SAR.Verified = 1 OR SAR.Verified IS NULL); 
                    ";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MentorID", mentorID);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }
        public static DataTable GetYourSocietiesForStudent(string userID)
        {
            DataTable dt = new DataTable();
            string query = @"
        SELECT s.SocietyID, s.SocietyName, 
               CASE 
                   WHEN s.approved = 1 THEN 'Approved'
                   ELSE 
                       CASE
                           WHEN sar.reqID IS NULL THEN 'Rejected'
                           ELSE 'Approval Pending' 
                       END
               END AS Status,
               'President' AS rollName
        FROM Society s
        LEFT JOIN SocietyApprovalRequests sar ON s.SocietyID = sar.SocietyID
        WHERE s.President_ID = @UserID
        ";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        dt.Columns.Add("SocietyID", typeof(int));
                        dt.Columns.Add("SocietyName", typeof(string));
                        dt.Columns.Add("rollName", typeof(string));
                        dt.Columns.Add("Status", typeof(string));


                        // Populate DataTable row by row

                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            row["SocietyID"] = reader.GetInt32(reader.GetOrdinal("SocietyID"));
                            row["SocietyName"] = reader.GetString(reader.GetOrdinal("SocietyName"));
                            row["rollName"] = reader.GetString(reader.GetOrdinal("rollName"));
                            row["Status"] = reader.GetString(reader.GetOrdinal("Status"));
                            dt.Rows.Add(row);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dt;
        }

        public static DataTable GetApprovalPendingRequests(string userID)
        {
            DataTable dt = new DataTable();

            string query = @"
    SELECT s.SocietyID, s.SocietyName, sr.rollName, 'Approval Pending' AS Status
    FROM Society s
    INNER JOIN MemberApprovalRequests mar ON s.SocietyID = mar.SocietyID
    INNER JOIN societyRoles sr ON mar.rollID = sr.rollID
    WHERE mar.UserID = @UserID;";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Create columns in DataTable based on SqlDataReader schema
                        //dt.Load(reader);

                        // Alternatively, you can create columns manually if needed

                        dt.Columns.Add("SocietyID", typeof(int));
                        dt.Columns.Add("SocietyName", typeof(string));
                        dt.Columns.Add("rollName", typeof(string));
                        dt.Columns.Add("Status", typeof(string));


                        // Populate DataTable row by row
                        
                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            row["SocietyID"] = reader.GetInt32(reader.GetOrdinal("SocietyID"));
                            row["SocietyName"] = reader.GetString(reader.GetOrdinal("SocietyName"));
                            row["rollName"] = reader.GetString(reader.GetOrdinal("rollName"));
                            row["Status"] = reader.GetString(reader.GetOrdinal("Status")); ;
                            dt.Rows.Add(row);
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dt;
        }

        public static DataTable GetJoinedSocietiesForStudent(string userID)
        {
            DataTable dt = new DataTable();
            string query = "SELECT s.SocietyID, s.SocietyName, sr.rollName, 'Approved' AS Status " +
                           "FROM societyMembers sm " +
                           "JOIN Society s ON sm.SocietyID = s.SocietyID " +
                           "JOIN societyRoles sr ON sm.rollID = sr.rollID " +
                           "WHERE sm.UserID = @UserID";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        // Create columns in DataTable based on SqlDataReader schema
                        //dt.Load(reader);

                        // Alternatively, you can create columns manually if needed

                        dt.Columns.Add("SocietyID", typeof(int));
                        dt.Columns.Add("SocietyName", typeof(string));
                        dt.Columns.Add("rollName", typeof(string));
                        dt.Columns.Add("Status", typeof(string));


                        // Populate DataTable row by row

                        while (reader.Read())
                        {
                            DataRow row = dt.NewRow();
                            row["SocietyID"] = reader.GetInt32(reader.GetOrdinal("SocietyID"));
                            row["SocietyName"] = reader.GetString(reader.GetOrdinal("SocietyName"));
                            row["rollName"] = reader.GetString(reader.GetOrdinal("rollName"));
                            row["Status"] = reader.GetString(reader.GetOrdinal("Status"));
                            dt.Rows.Add(row);
                        }

                    }
                    catch (Exception ex)
                    {
                        // Handle exception
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return dt;
        }

        public static void InsertMemberApprovalRequest(int societyID, string userID, int rollID, int teamID, string purpose, string motivation, string aboutYou, string pastExp)
        {
            string query = @"INSERT INTO MemberApprovalRequests (SocietyID, UserID, rollID, TeamID, purpose, motivation, AboutYou, PastExp)
                         VALUES (@SocietyID, @UserID, @RollID, @TeamID, @Purpose, @Motivation, @AboutYou, @PastExp)";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SocietyID", societyID);
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@RollID", rollID);
                    command.Parameters.AddWithValue("@TeamID", teamID);
                    command.Parameters.AddWithValue("@Purpose", purpose);
                    command.Parameters.AddWithValue("@Motivation", motivation);
                    command.Parameters.AddWithValue("@AboutYou", aboutYou);
                    command.Parameters.AddWithValue("@PastExp", pastExp);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public static DataTable GetRemainingSlots(int teamID, int societyID)
        {
            DataTable dataTable = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    // SQL query to retrieve remaining slots
                    string query = @"
                        SELECT sr.rollID, sr.rollName, (sr.numbers - COUNT(sm.UserID)) AS RemainingSlots
                        FROM societyRoles sr
                        LEFT JOIN societyMembers sm ON sr.rollID = sm.rollID AND sm.TeamID = @teamID AND sm.SocietyID = @societyID
                        GROUP BY sr.rollID, sr.rollName, sr.numbers
                        
                        HAVING sr.numbers - COUNT(sm.UserID) > 0 OR COUNT(sm.UserID) IS NULL";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@teamID", teamID);
                        command.Parameters.AddWithValue("@societyID", societyID);

                        // Open the connection
                        connection.Open();

                        // Execute the query and fill the DataTable with the results
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dataTable;
        }
        public static DataTable GetSocietyNamesAndIDs(string userID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT s.SocietyID, s.SocietyName
                FROM Society s
                LEFT JOIN societyMembers sm ON s.SocietyID = sm.SocietyID AND sm.UserID = @UserID
                WHERE (sm.SocietyID IS NULL AND s.President_ID != @UserID)
                    AND s.approved = 1
                    AND NOT EXISTS (
                        SELECT 1
                        FROM MemberApprovalRequests mar
                        WHERE mar.UserID = @UserID AND mar.SocietyID = s.SocietyID
                    )";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }

        public static DataTable GetTeams()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = "SELECT TeamID, teamName FROM Teams";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }

        public static void InsertIntoSocietyApprovalRequests( int societyID, string purpose, string motivation, string aboutYou, string pastExp, string plannedEvents)
        {
            string query = @"INSERT INTO SocietyApprovalRequests (societyID, purpose, motivation, AboutYou, PastExp, PlannedEvent, Verified)
                    VALUES (@SocietyID, @Purpose, @Motivation, @AboutYou, @PastExp, @PlannedEvent, 0);";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SocietyID", societyID);
                    command.Parameters.AddWithValue("@Purpose", purpose);
                    command.Parameters.AddWithValue("@Motivation", motivation);
                    command.Parameters.AddWithValue("@AboutYou", aboutYou);
                    command.Parameters.AddWithValue("@PastExp", pastExp);
                    command.Parameters.AddWithValue("@PlannedEvent", plannedEvents);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static int InsertIntoSociety(string societyName, object mentorID, DateTime creationDate, string presidentID, bool approved, string description)
        {
            int societyID = 0; // Variable to hold the autogenerated SocietyID

            string query = @"
        INSERT INTO Society (SocietyName, MentorID, CreationDate, President_ID, approved, Description)
        VALUES (@SocietyName, @MentorID, @CreationDate, @PresidentID, @Approved, @Description);
        SELECT SCOPE_IDENTITY();"; // Query to insert and retrieve the autogenerated SocietyID

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SocietyName", societyName);
                    command.Parameters.AddWithValue("@MentorID", mentorID ?? DBNull.Value); // Set to DBNull.Value if MentorID is NULL
                    command.Parameters.AddWithValue("@CreationDate", creationDate);
                    command.Parameters.AddWithValue("@PresidentID", presidentID);
                    command.Parameters.AddWithValue("@Approved", approved);
                    command.Parameters.AddWithValue("@Description", description);

                    connection.Open();
                    // ExecuteScalar is used to retrieve a single value (SocietyID in this case) from the query
                    societyID = Convert.ToInt32(command.ExecuteScalar());
                }
            }

            return societyID; // Return the autogenerated SocietyID
        }


        static void InsertIntoUsers( string userID, string password, string email, DateTime dob)
        {
            string query = @"INSERT INTO Users (UserID, Password, Email, DOB)
                         VALUES (@UserID, @Password, @Email, @DOB)";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@DOB", dob);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        static void InsertIntoStudent( string userID, string department, string batch)
        {
            string query = @"INSERT INTO student (UserID, Department, Batch)
                         VALUES (@UserID, @Department, @Batch)";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Department", department);
                    command.Parameters.AddWithValue("@Batch", batch);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public static User Login(string userID, string password)
        {
            string query = @"SELECT U.UserID, U.Password, U.Email, U.DOB,
                            S.Department AS StudentDepartment, S.Batch AS StudentBatch,
                            M.Department AS MentorDepartment,
                            A.UserID AS AdminID
                     FROM Users U
                     LEFT JOIN student S ON U.UserID = S.UserID
                     LEFT JOIN Mentor M ON U.UserID = M.UserID
                     LEFT JOIN Admin A ON U.UserID = A.UserID
                     WHERE U.UserID = @UserID AND U.Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", userID);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Determine user type
                            string userType = reader["StudentDepartment"] != DBNull.Value ? "Student" :
                                              reader["MentorDepartment"] != DBNull.Value ? "Mentor" :
                                              reader["AdminID"] != DBNull.Value ? "Admin" : "";

                            switch (userType)
                            {
                                case "Student":
                                    return PopulateStudent(reader);
                                case "Mentor":
                                    return PopulateMentor(reader);
                                case "Admin":
                                    return PopulateAdmin(reader); // Return Admin object
                                default:
                                    return null; // No matching user type
                            }
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
                }
            }
        }


        private static Student PopulateStudent(SqlDataReader reader)
    {
        return new Student
        {
            UserID = reader["UserID"].ToString(),
            Password = reader["Password"].ToString(),
            Email = reader["Email"].ToString(),
            DOB = Convert.ToDateTime(reader["DOB"]),
            Department = reader["StudentDepartment"].ToString(),
            Batch = reader["StudentBatch"].ToString()
        };
    }

    private static Mentor PopulateMentor(SqlDataReader reader)
    {
        return new Mentor
        {
            UserID = reader["UserID"].ToString(),
            Password = reader["Password"].ToString(),
            Email = reader["Email"].ToString(),
            DOB = Convert.ToDateTime(reader["DOB"]),
            Department = reader["MentorDepartment"].ToString()
        };
    }

    private static Admin PopulateAdmin(SqlDataReader reader)
    {
        return new Admin
        {
            UserID = reader["UserID"].ToString(),
            Password = reader["Password"].ToString(),
            Email = reader["Email"].ToString(),
            DOB = Convert.ToDateTime(reader["DOB"])
        };
    }


        public static DataTable GetApprovedSocietyEvents(int societyID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
            {
                string sqlQuery = "SELECT eventID, eventName, Date, registrationFee, Description " +
                                  "FROM societyEvents " +
                                  "WHERE approved = 1 AND societyID = @societyID";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@societyID", societyID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                }
                catch (Exception ex)
                {
                    // Handle exception
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return dt;
        }



        public static DataTable GetSocietyEventsForStudent(string userID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionStr.connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT se.eventID, se.societyID, se.eventName, se.Date, se.registrationFee, se.Description
                FROM societyEvents se
                INNER JOIN societyMembers sm ON se.societyID = sm.SocietyID
                LEFT JOIN societyEventsApproval sea ON se.eventID = sea.reqID AND se.societyID = sea.societyID
                WHERE sm.UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                MessageBox.Show("Error: " + ex.Message);
            }

            return dt;
        }









    }

}

   