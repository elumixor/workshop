﻿using NHibernate;
using System;
using System.Collections.Generic;
using workshopIS.Helpers;
using workshopIS.Models;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Net;

namespace workshopIS
{
    public static class Data
    {
        public static List<IPartner> Partners;
        private const decimal MIN_LOAN_AMOUNT = 20000;
        private const decimal MAX_LOAN_AMOUNT = 500000;
        private const decimal MIN_LOAN_DURATION = 6;
        private const decimal MAX_LOAN_DURATION = 96;

        internal static HttpResponseMessage GetCallCentreResults()
        {
            ISession session = NHibernateHelper.GetCurrentSession();
            /*
            var results = session.QueryOver<CCustomer>()
                .Fetch(t => t.Partner).Eager
                .List();

            var result2 = results.GroupBy(c => c.Partner).Select(c => new { Partner = c.Key, Cnt = c.Count() });
            session.Close();*/
            //var result = session.QueryOver<CLoan>()
            //    .Fetch(t => t.Customer).Eager
            //    .List();

            IList<CLoan> query = session.QueryOver<CLoan>()
                        .JoinQueryOver(l => l.Customer)
                        .JoinQueryOver(l => l.Partner).List();
            var result = query.Select(x => new { loan = x, customer = x.Customer });


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        // Runs on startup
        public static void Initialize()
        {
            // ReadDataFromDatabase(); 

            FakeDatabase();
        }

        // DATABASE CODE BELOW
        private static void ReadDataFromDatabase()
        {
            ISession session = NHibernateHelper.GetCurrentSession();

            //ITransaction tx = session.BeginTransaction();
            //session.Save(new Message { Body = "dsfsdfsdf", CreationTime = DateTime.Now });
            //tx.Commit();
            var results = session.Query<CPartner>();
            Partners = results.ToList<IPartner>();
        }

        // Fake DB
        public static void FakeDatabase()
        {
            // initialize fake list of partners,
            // customers and loans - all related
            Partners = new List<IPartner>();
            /*
            Partners = new List<IPartner>
            {
                new CPartner
                {
                    Id = 1,
                    Name = "Partner John",
                    ICO = 7891,
                    Customers = new List<ICustomer>
                    {
                        new CCustomer
                        {
                            Id = 1,
                            FirstName = "Customer Bob",
                            Loans = new List<ILoan>
                            {
                                new CLoan
                                {
                                    Id = 1,
                                    Amount = 40000,
                                    Duration = 10
                                },
                                new CLoan
                                {
                                    Id = 10,
                                    Amount = 200000,
                                    Duration = 50
                                },
                                new CLoan
                                {
                                    Id = 7,
                                    Amount = 50000,
                                    Duration = 960
                                }
                            }
                        },
                        new CCustomer
                        {
                            Id = 2,
                            FirstName = "Customer Cob"
                        },
                        new CCustomer
                        {
                            Id = 3,
                            FirstName = "Customer Hello World"
                        }
                    }
                },
                new CPartner
                {
                    Id = 2,
                    Name = "Partner Lawn",
                    ICO = 32213,
                    Customers = new List<ICustomer>
                    {
                        new CCustomer
                        {
                            Id = 6,
                            FirstName = "Second Partner Customer"
                        },
                        new CCustomer
                        {
                            Id = 13,
                            FirstName = "Friday"
                        }
                    }
                }
            };
            */
        }

        private static int counterP = 0;
        private static int counterC = 0;
        private static int counterL = 0;

        internal static int SaveToDB(Object obj)
        {
            // not sure if this works
            /*
            ISession session = NHibernateHelper.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();
            int index = (int)session.Save(obj);
            tx.Commit();
            */ // this doesnt work
            if (obj.GetType() == typeof(CPartner))
                return counterP++;
            if (obj.GetType() == typeof(CCustomer))
                return counterC++;
            if (obj.GetType() == typeof(CLoan))
                return counterL++;
            return 0;
        }

        // Partners Methods
        public static List<IPartner> GetPartners()
        {
            // TODO
            return null;
        }

        public static IPartner GetPartner(int id)
        {
            // TODO
            return null;
        }

        // Customers methods
        internal static List<ICustomer> GetCustomers()
        {
            // TODO
            return null;
        }
        // ...
    }
}