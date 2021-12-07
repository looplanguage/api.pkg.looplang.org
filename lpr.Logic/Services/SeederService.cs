
using System.Collections.Generic;
using lpr.Common;
using lpr.Common.Interfaces.Contexts;
using lpr.Common.Models;

namespace lpr.Logic.Services {
    public class SeederService
    {
        private ILprDbContext _lprDbContext;

        private enum Amounts
        {
            Accounts = 10,
            Packages = 5,
            Versions = 3
        }
        
        public SeederService(ILprDbContext ctx)
        {
            _lprDbContext = ctx;
        }
        public int Seed()
        {
            foreach (Account account in Accounts())
            {
                Organisation organisation = LoopFaker.Organisation();
                OrganisationMember organisationMember = LoopFaker.OrganisationMember(account,organisation);
                _lprDbContext.Add(organisation);
                _lprDbContext.Add(organisationMember);

                foreach (Package package in Packages(organisation))
                {
                    Versions(package);
                }
            }
            


            return _lprDbContext.SaveChanges();
        }

        private List<Account> Accounts()
        {
            List<Account> accounts = new List<Account>();
            for (int i = 0; i < (int)Amounts.Accounts; i++)
            {
                Account account = LoopFaker.Account();
                _lprDbContext.Add(account);
                accounts.Add(account);
            }

            return accounts;
        }

        private List<Package> Packages(Organisation organisation)
        {
            List<Package> packages = new List<Package>();
            for (int i = 0; i < (int)Amounts.Packages; i++)
            {
                Package package = LoopFaker.Package(organisation);
                _lprDbContext.Add(package);
                packages.Add(package);
                
            }

            return packages;
        }

        private void Versions(Package package)
        {
            for (int j = 0; j < (int)Amounts.Versions; j++)
            {
                lpr.Common.Models.Version version = LoopFaker.Version(package);
                _lprDbContext.Add(version);
            }
        }


    }
}