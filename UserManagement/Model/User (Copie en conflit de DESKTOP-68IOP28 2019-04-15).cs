using Michael.Database;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using UserManagement.Interface;
using UserManagement.Model.Service;

namespace UserManagement.Model
{
    public class User : IValidable
    {
        public SalaryBetweenDate GetSalaryWithCalculatedElementsAtDate(DateTime currentDate, User[] allUsers, SalaryRising[] allSalaryRisings, CalculatedElementUser[] allCalculatedElementUsers)
        {
            SalaryRising[] userSalaryRisings = allSalaryRisings.Where(rising => rising.User == Id).ToArray();
            string[] splitted = null;
            bool matches = false;
            char separator = ';';
            SalaryBetweenDate result = null;

            if (userSalaryRisings.Length == 0)
                throw new Exception("Pas d'augmentation de salaire trouvée par rapport à cet utilisateur.");

            SalaryRising nearest = userSalaryRisings.Where(rising => rising.RisingDate <= currentDate).OrderByDescending(rising => rising.RisingDate).First();
            // Filter by dateApplication
            List<CalculatedElementUser> filteredElements = allCalculatedElementUsers.Where(element => element.BeginningDate <= currentDate && (element.EndingDate == null || element.EndingDate >= currentDate)).ToList();
            // Filter by year
            foreach (CalculatedElementUser element in filteredElements)
            {
                splitted = element.Years.Split(separator);
                matches = false;
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (splitted[i].Equals("%"))
                        continue;
                     
                }
            }

            result = new SalaryBetweenDate(currentDate, nearest.Salary.ToString(), null);
            return result;
        }

        public SalaryBetweenDate GetSalaryAtDate(DateTime currentDate, User[] allUsers, SalaryRising[] allSalaryRisings)
        {
            SalaryRising[] userSalaryRisings = allSalaryRisings.Where(rising => rising.User == Id).ToArray();
            SalaryBetweenDate result = null;

            if (userSalaryRisings.Length == 0)
                throw new Exception("Pas d'augmentation de salaire trouvée par rapport à cet utilisateur.");

            SalaryRising nearest = userSalaryRisings.Where(rising => rising.RisingDate <= currentDate).OrderByDescending(rising => rising.RisingDate).First();
            result = new SalaryBetweenDate(currentDate, nearest.Salary.ToString(), null);
            return result;
        }

        public SalaryBetweenDate[] GetSalaryBetweenDatesWithCalculatedElement(string beginningDateInput, string endingDateInput, CalculatedElementUser[] allCalculatedElementUsers, User[] allUsers, SalaryRising[] allSalaryRisings)
        {
            DateTime beginningDate = DateTime.ParseExact(beginningDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endingDate = DateTime.ParseExact(endingDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            
            List<CalculatedElementUser> filteredElements = allCalculatedElementUsers.ToList();
            filteredElements = filteredElements.Where(element => (element.BeginningDate <= beginningDate && (element.EndingDate == null || element.EndingDate >= endingDate))).ToList();

            return null;
        }

        public SalaryBetweenDate[] GetSalaryBetweenDates(string beginningDateInput, string endingDateInput, User[] allUsers, SalaryRising[] allSalaryRisings)
        {
            DateTime beginningDate = DateTime.ParseExact(beginningDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime endingDate = DateTime.ParseExact(endingDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            SalaryRising start = null;
            SalaryRising end = null;
            List<SalaryBetweenDate> results = new List<SalaryBetweenDate>();

            SalaryRising[] userSalaryRisings = allSalaryRisings.Where(rising => rising.User == Id).ToArray();

            DateTime currentDate = beginningDate;

            while (currentDate <= endingDate)
            {
                results.Add(GetSalaryAtDate(currentDate, allUsers, allSalaryRisings));
                currentDate = currentDate.AddMonths(1);
            }

            return results.ToArray();
        }

        public void InsertCalculatedElementUser(string indemnityInput, string indemnityTypeInput, string formulaInput, string signInput, string usersInput, string beginningDateInput, string endingDateInput, string monthsInput, string yearsInput)
        {
            DbConnection connection = null;
            Indemnity[] allIndemnities = null;
            User[] allUsers = null;
            DateTime beginningDate = new DateTime();
            DateTime? endingDate = null;
            bool sign = true;

            List<string> months = new List<string>();
            List<string> years = new List<string>();
            string tempControl = null;
            int min = 0;
            int max = 0;
            string pattern = null;
            MatchCollection matches = null;
        
            bool exist = true;
            string[] splitted = null;

            ServiceIndemnity serviceIndemnity = new ServiceIndemnity();
            ServiceIndemnityType serviceIndemnityType = new ServiceIndemnityType();
            ServiceUser serviceUser = new ServiceUser();

            // Control indemnity existence
            allIndemnities = serviceIndemnity.GetAllIndemnities();
            exist = allIndemnities.Any(indemnity => indemnity.Name == indemnityInput);
                if (!exist)
                    throw new Exception("L'élément " + indemnityInput + " n'existe pas.");

            // Control users existence
            allUsers = serviceUser.GetAllUsers();
            pattern = @";$";
            usersInput = Regex.Replace(usersInput, pattern, "");
            splitted = usersInput.Split(';');
            for (int i = 0; i < splitted.Length; i++)
            {
                exist = allUsers.Any(user => user.Id == int.Parse(splitted[i]));
                if (!exist)
                    throw new Exception("L'utilisateur avec l'id " + splitted[i] + "n'existe pas.");
            }

            // Control sign
            if (!signInput.Equals("-1") && !signInput.Equals("1"))
                throw new Exception("Sign value incorrect.");

            sign = signInput.Equals("1") ? true : false;

            // Control monthsInput
            // month intervals control
            pattern = @"(\d+-\d+)";
            matches = Regex.Matches(monthsInput, pattern);

            foreach (Match match in matches)
            {
                splitted = match.Value.Split('-');
                min = int.Parse(splitted[0]);
                max = int.Parse(splitted[1]);

                if (min == 0 || min > 12)
                    throw new Exception("Le mois doit être compris entre 1 et 12.");

                if (max == 0 || max > 12)
                    throw new Exception("Le mois doit être compris entre 1 et 12.");

                if (min >= max)
                    throw new Exception("Erreur date: "+min+">="+max);
            }

            // month by month control
            pattern = @"(\d+-\d+;?)";
            tempControl = Regex.Replace(monthsInput, pattern, "");
            pattern = @";$";
            tempControl = Regex.Replace(tempControl, pattern, "");

            if(!string.IsNullOrEmpty(tempControl))
            {
                splitted = tempControl.Split(';');
                for (int i = 0; i < splitted.Length; i++)
                {
                    if(int.Parse(splitted[i]) == 0 || int.Parse(splitted[i]) > 12)
                        throw new Exception("Le mois doit être compris entre 1 et 12.");

                    if (months.Contains(splitted[i]))
                        throw new Exception("Doublon - le mois " + splitted[i] + " a été défini plusieurs fois.");

                    months.Add(splitted[i]);
                }
            }


            pattern = @";$";
            monthsInput = Regex.Replace(monthsInput, pattern, "");

            // Control yearsInput
            // year intervals control
            pattern = @"(\d+-\d+)";
            matches = Regex.Matches(monthsInput, pattern);

            foreach (Match match in matches)
            {
                splitted = match.Value.Split('-');
                min = int.Parse(splitted[0]);
                max = int.Parse(splitted[1]);

                if (min >= max)
                    throw new Exception("Erreur année: " + min + ">=" + max);
            }

            // year by year control
            pattern = @"(\d+-\d+;?)";
            tempControl = Regex.Replace(yearsInput, pattern, "");
            pattern = @";$";
            tempControl = Regex.Replace(tempControl, pattern, "");

            if(!string.IsNullOrEmpty(tempControl))
            {
                splitted = tempControl.Split(';');
                for (int i = 0; i < splitted.Length; i++)
                {
                    if (years.Contains(splitted[i]))
                        throw new Exception("Doublon - l'année " + splitted[i] + " a été défini plusieurs fois.");

                    years.Add(splitted[i]);
                }
            }

            pattern = @";$";
            yearsInput = Regex.Replace(yearsInput, pattern, "");

            // TODO: Control date input
            // Parse Date
            if (string.IsNullOrEmpty(beginningDateInput))
                throw new Exception("Date de début d'application obligatoire.");

            beginningDate = DateTime.ParseExact(beginningDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            if(!string.IsNullOrEmpty(endingDateInput))
                endingDate = DateTime.ParseExact(endingDateInput, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            CalculatedElementUser calculatedElementUser = new CalculatedElementUser(0, indemnityInput, int.Parse(indemnityTypeInput), formulaInput, sign, usersInput, SATUS_VALID, beginningDate, endingDate, monthsInput, yearsInput);

            connection = serviceUser.GetDbConnection();
            connection.Open();
            Reflection.Insert(connection, "CalculatedElementUser" ,calculatedElementUser);
            connection.Close();
        }

        public void ModifyUsersRightsOnDataTable(string usersInput, string dataTablesInput, string authorizationInput)
        {
            if (!IsAdmin())
                throw new Exception("Only admin can modify users rights.");

            List<string> usersIds = new List<string>();
            List<string> authorizationsId = new List<string>();
            List<string> tableNames = new List<string>();
            List<User> users = new List<User>();
            List<Authorization> authorizations = new List<Authorization>();
            List<DataTable> dataTables = new List<DataTable>();
            ServiceAuthorization serviceAuthorization = new ServiceAuthorization();
            ServiceUser serviceUser = new ServiceUser();
            ServiceDataTable serviceDataTable = new ServiceDataTable();

            // Control profil inputs
            if (usersInput.Equals("%"))
            {
                users = serviceUser.GetAllUsers().ToList();
            }
            else
            {
                if (!usersInput.Contains(";"))
                    usersIds.Add(usersInput);
                else
                {
                    char[] separators = { ';' };
                    usersIds = usersInput.Split(separators).ToList();
                }
            }


            // Control authorization inputs
            if (!authorizationInput.Contains(","))
                authorizationsId.Add(authorizationInput);
            else
            {
                char[] separators = { ',' };
                authorizationsId = authorizationInput.Split(separators).ToList();
            }

            // Control table inputs
            if (dataTablesInput.Equals("%"))
            {
                dataTables = serviceDataTable.GetAllDataTables().ToList();
            }
            else
            {
                if (!dataTablesInput.Contains(";"))
                    tableNames.Add(dataTablesInput);
                else
                {
                    char[] separators = { ';' };
                    tableNames = dataTablesInput.Split(separators).ToList();
                }
            }

            // Control Profile existance
            User tempUser = null;

            for (int i = 0; i < usersIds.Count(); i++)
            {
                tempUser = serviceUser.GetUser(usersIds[i]);

                if (tempUser == null)
                    throw new Exception("L'id d'utilisateur " + usersIds[i] + " n'existe pas.");

                users.Add(tempUser);
            }

            // Control Authorization existance
            Authorization tempAuthorization = null;

            for (int i = 0; i < authorizationsId.Count(); i++)
            {
                tempAuthorization = serviceAuthorization.GetAuthorization(authorizationsId[i]);

                if (tempAuthorization == null)
                    throw new Exception("L'id d'autorisation " + authorizationsId[i] + " n'existe pas.");

                authorizations.Add(tempAuthorization);
            }

            // Control Table existance
            DataTable tempTable = null;

            for (int i = 0; i < tableNames.Count(); i++)
            {
                tempTable = serviceDataTable.GetDataTable(tableNames[i]);

                if (tempTable == null)
                    throw new Exception("La table " + tableNames[i] + " n'existe pas.");

                dataTables.Add(tempTable);
            }

            UserAuthorizationOnTable duplicate = null;
            ServiceUserAuthorizationOnTable serviceUserAuthorizationOnTable = new ServiceUserAuthorizationOnTable();

            // TODO: For each profile/auth/table create an instance
            for (int i = 0; i < users.Count(); i++)
            {
                for (int j = 0; j < dataTables.Count(); j++)
                {
                    for (int k = 0; k < authorizations.Count(); k++)
                    {
                        duplicate = serviceUserAuthorizationOnTable.GetUserAuthorizationOnTable(users[i].Id.ToString(), authorizations[k].Id.ToString(), dataTables[j].Id.ToString());

                        // If there's a duplicate just continue the loop else insert new data
                        if (duplicate != null)
                            continue;

                        // Create a new data for table. NB: Using duplicate to avoid allocating new variable and spare memory
                        duplicate = new UserAuthorizationOnTable(0, users[i].Id, authorizations[k].Id, dataTables[j].Id);
                        serviceUserAuthorizationOnTable.InsertUserAuthorizationOnTable(duplicate);
                    }
                }
            }
        }

        public void ModifyProfilsRightsOnDataTable(string profilesInput, string dataTablesInput, string authorizationInput)
        {
            if (!IsAdmin())
                throw new Exception("Only admin can modify profiles rights.");

            List<string> profilIds = new List<string>();
            List<string> authorizationsId = new List<string>();
            List<string> tableNames = new List<string>();
            List<Profile> profiles = new List<Profile>();
            List<Authorization> authorizations = new List<Authorization>();
            List<DataTable> dataTables = new List<DataTable>();
            ServiceAuthorization serviceAuthorization = new ServiceAuthorization();
            ServiceProfile serviceProfile = new ServiceProfile();
            ServiceDataTable serviceDataTable = new ServiceDataTable();

            // Control profil inputs
            if (profilesInput.Equals("%"))
            {
                profiles = serviceProfile.GetAllProfiles().ToList();
            }
            else
            {
                if (!profilesInput.Contains(";"))
                    profilIds.Add(profilesInput);
                else
                {
                    char[] separators = { ';' };
                    profilIds = profilesInput.Split(separators).ToList();
                }
            }


            // Control authorization inputs
            if (!authorizationInput.Contains(","))
                authorizationsId.Add(authorizationInput);
            else
            {
                char[] separators = { ',' };
                authorizationsId = authorizationInput.Split(separators).ToList();
            }

            // Control table inputs
            if(dataTablesInput.Equals("%"))
            {
                dataTables = serviceDataTable.GetAllDataTables().ToList();
            }
            else
            {
                if (!dataTablesInput.Contains(";"))
                    tableNames.Add(dataTablesInput);
                else
                {
                    char[] separators = { ';' };
                    tableNames = dataTablesInput.Split(separators).ToList();
                }
            }

            // Control Profile existance
            Profile tempProfile = null;

            for (int i = 0; i < profilIds.Count(); i++)
            {
                tempProfile = serviceProfile.GetProfile(profilIds[i]);

                if(tempProfile == null)
                    throw new Exception("L'id de profil " + profilIds[i] + " n'existe pas.");

                profiles.Add(tempProfile);
            }

            // Control Authorization existance
            Authorization tempAuthorization = null;

            for (int i = 0; i < authorizationsId.Count(); i++)
            {
                tempAuthorization = serviceAuthorization.GetAuthorization(authorizationsId[i]);

                if (tempAuthorization == null)
                    throw new Exception("L'id d'autorisation " + authorizationsId[i] + " n'existe pas.");

                authorizations.Add(tempAuthorization);
            }

            // Control Table existance
            DataTable tempTable = null;

            for (int i = 0; i < tableNames.Count(); i++)
            {
                tempTable = serviceDataTable.GetDataTable(tableNames[i]);

                if (tempTable == null)
                    throw new Exception("La table " + tableNames[i] + " n'existe pas.");

                dataTables.Add(tempTable);
            }

            ProfileAuthorizationOnTable duplicate = null;
            ServiceProfileAuthorizationOnTable serviceProfileAuthorizationOnTable = new ServiceProfileAuthorizationOnTable();

            // TODO: For each profile/auth/table create an instance
            for (int i = 0; i < profiles.Count(); i++)
            {
                for (int j= 0; j < dataTables.Count(); j++)
                {
                    for (int k = 0; k < authorizations.Count(); k++)
                    {
                        duplicate = serviceProfileAuthorizationOnTable.GetProfileAuthorizationOnTable(profiles[i].Id, authorizations[k].Id.ToString(), dataTables[j].Id.ToString());

                        // If there's a duplicate just continue the loop else insert new data
                        if (duplicate != null)
                            continue;

                        // Create a new data for table. NB: Using duplicate to avoid allocating new variable and spare memory
                        duplicate = new ProfileAuthorizationOnTable(0, profiles[i].Id, dataTables[j].Id, authorizations[k].Id);
                        serviceProfileAuthorizationOnTable.InsertProfileAuthorizationOnTable(duplicate);
                    }
                }
            }
        }

        public void LoadProfile()
        {
            ServiceProfile service = new ServiceProfile();
            DbConnection connection = service.GetDbConnection();
            try
            {
                connection.Open();
                string[] attributes = { "User" };
                string[] values = { Id.ToString() };
                string[] operators = { "=" };

                if(ProfileId == null)
                {
                    throw new Exception("Désolé, vous n'avez pas encore de profil, Veuillez contacter l'admin pour en créer un.");
                }

                Profile = service.GetProfile(ProfileId);
            }
            catch (Exception)
            {
                throw;
            } finally
            {
                connection.Close();
            }
        }

        public User(int id, string name, string password, decimal salary)
        {
            Id = id;
            Name = name;
            Password = password;
            Salary = salary;
        }

        public bool IsAdmin()
        {
            if (Profile == null)
                LoadProfile();

            return Profile.Name.Equals("root");
        }

        public User[] GetOtherUsersInformation(User[] allUsers)
        {
            return IsAdmin() ? allUsers : allUsers.Where(user => user.Salary <= Salary).ToArray();            
        }

        public bool IsValid()
        {
            return Status > 1;
        }

        public static int SATUS_VALID = 10;

        public int Id { get; }
        public string Name { get; set; }
        public string Password { get; set; }
        public decimal Salary { get; set; }
        public string ProfileId { get; set; }
        public Profile Profile { get; set; }
        public int Status { get; set; }
    }
}