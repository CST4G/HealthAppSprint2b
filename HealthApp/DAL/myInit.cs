using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using healthApp.Models;
using DevOne.Security.Cryptography.BCrypt;

namespace healthApp.DAL {
    public class AccountInit : CreateDatabaseIfNotExists<AccountsDBContext> {
        protected override void Seed( AccountsDBContext context ) {
            string[] mypassword = { "sysadmin", "admin", "123322" };
            string[] salts = { BCryptHelper.GenerateSalt(), BCryptHelper.GenerateSalt(), BCryptHelper.GenerateSalt() };
            string[] hash = new String[3];
            for ( int i = 0; i < 3; ++i ) {
                hash[i] = BCryptHelper.HashPassword( mypassword[i], salts[i] );
            }

            var users = new List<Accounts>
            {
                new Accounts{ID=1,Username="sysadmin",encryptedPassword=hash[0], salt=salts[0], fName="system",lName="admin", acctType="sysadmin"},
            };

            users.ForEach( s => context.Accounts.Add( s ) );
            context.SaveChanges();
        }
    }

    public class ClientInit : CreateDatabaseIfNotExists<ClientDBContext> {
        protected override void Seed( ClientDBContext context ) {
            DateTime now = DateTime.Now;
            DateTime yesterday = new DateTime( now.Year, now.Month, now.Day - 1 );

            var clients = new List<Client>
            {
                new Client{ClientID=1, ClientFirstName="John", ClientLastName="Doe", ClientDOB=yesterday, ClientHealthNum=4, ClientGender="Male", ClientBedNum=1, ClientFamilyDoc="John Wayne"},
                new Client{ClientID=2, ClientFirstName="Fred", ClientLastName="Daily", ClientDOB=yesterday, ClientHealthNum=2, ClientGender="Male", ClientBedNum=2, ClientFamilyDoc="Mason Family"},
                new Client{ClientID=3, ClientFirstName="Charlie", ClientLastName="Zoolander", ClientDOB=yesterday, ClientHealthNum=5, ClientGender="Male", ClientBedNum=3, ClientFamilyDoc="Dr Who"},
                new Client{ClientID=4, ClientFirstName="Susan", ClientLastName="Francis", ClientDOB=yesterday, ClientHealthNum=3, ClientGender="Female", ClientBedNum=4, ClientFamilyDoc="You"},
                new Client{ClientID=5, ClientFirstName="Betty", ClientLastName="Faulker", ClientDOB=yesterday, ClientHealthNum=1, ClientGender="Female", ClientBedNum=5, ClientFamilyDoc="Surely"},
            };

            clients.ForEach( s => context.Client.Add( s ) );
            context.SaveChanges();
        }
    }

    public class TaskInit : CreateDatabaseIfNotExists<TaskDBContext> {
        protected override void Seed( TaskDBContext context ) {
            DateTime now = DateTime.Now;
            DateTime yesterday = new DateTime( now.Year, now.Month, now.Day - 1 );
            var tasks = new List<Tasks>
            {
                new Tasks{ID=1, created=DateTime.Now, PatientID="1", Task="Feed", duration=1, dtStart=yesterday, freq="daily", count=4},
                new Tasks{ID=2, created=DateTime.Now, PatientID="2", Task="Feed", duration=1, dtStart=yesterday, freq="daily", count=4},
                new Tasks{ID=3, created=DateTime.Now, PatientID="3", Task="Feed", duration=1, dtStart=yesterday, freq="daily", count=4},
                new Tasks{ID=4, created=DateTime.Now, PatientID="4", Task="Feed", duration=1, dtStart=yesterday, freq="daily", count=4},
                new Tasks{ID=5, created=DateTime.Now, PatientID="5", Task="Feed", duration=1, dtStart=yesterday, freq="daily", count=4},
            };

            tasks.ForEach( s => context.Tasks.Add( s ) );
            context.SaveChanges();
        }
    }
}