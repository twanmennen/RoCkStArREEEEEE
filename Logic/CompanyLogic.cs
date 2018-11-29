
using System.Collections.Generic;
using Data.Context;
using Models;

namespace Logic
{
    public class CompanyLogic
    {
        CompanyContext _companyContext = new CompanyContext();

        public Company GetCompanyById(int id)
        {
            return _companyContext.GetCompanyById(id);
        }

        //public List<Company> GetCompaniesById(List<Company> companies)
        //{
        //    var fullCompanies = new List<Company>();
        //    foreach (var company in companies)
        //    {
        //        fullCompanies.Add(GetCompanyById(company.Id));
        //    }

        //    return fullCompanies;
        //}

        public List<Review> GetReviewsByCompany(int id)
        {
            return _companyContext.GetReviewsByCompany(id);
        }

        public List<Company> GetAllCompanies()
        {
            return _companyContext.GetAllCompanies();
        }
    }
}
