using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using static Societify.connectionStr;

namespace Societify
{
    class DBHandler
    {
        public static DataTable GetSocietyEventsWithStatus(int societyID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                    SELECT
                        se.eventName,
                        se.Date,
                        se.registerationFee,
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
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert into societyEvents table
                    string insertEventQuery = @"
                INSERT INTO societyEvents (societyID, eventName, Date, registerationFee, Description, approved)
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
                INSERT INTO societyEventsApproval (societyID, eventName, Date, registerationFee, Description)
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
        public static void UpdateSocietyApproval(int societyID, bool approvedValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT sa.societyID, s.SocietyName, s.President_ID
                FROM SocietyApprovalRequests sa
                JOIN Society s ON sa.societyID = s.SocietyID";

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
        public static DataTable GetSocietiesInEventsApprovalRequests()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
            SELECT reqID, societyID, eventName
            FROM societyEventsApproval";

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

        public static void UpdateSocietyEventApproval(int societyID, int eventID, bool approvedValue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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

        public static DataTable GetSocietyEventsApprovalDetails(int societyID, int eventID)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
        WHERE EXISTS (
            SELECT 1 
            FROM societyMembers sm
            WHERE s.President_ID = @UserID
        )";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
                using (SqlConnection connection = new SqlConnection(connectionString))
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
            string query = @"INSERT INTO SocietyApprovalRequests (societyID, purpose, motivation, AboutYou, PastExp, PlannedEvent)
                         VALUES (@SocietyID, @Purpose, @Motivation, @AboutYou, @PastExp, @PlannedEvent)";

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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

            using (SqlConnection connection = new SqlConnection(connectionString))
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

        using (SqlConnection connection = new SqlConnection(connectionString))
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
                                return PopulateAdmin(reader);
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
    }

    
}
