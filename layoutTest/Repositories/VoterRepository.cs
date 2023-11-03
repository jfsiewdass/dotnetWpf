using layoutTest.Models;
using layoutTest.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layoutTest.Data
{
    public class VoterRepository
    {
        private readonly Repository<VoterModel> _voterRepository;

        private readonly ApplicationDBContext _db;

        public VoterRepository()
        {
            var contextFactory = new ApplicationContextFactory();
            _db = contextFactory.CreateDbContext();
            _voterRepository = new Repository<VoterModel>(new ApplicationContextFactory());
        }
        

        public async Task<bool> DeleteBrand(int id)
        {
            try
            {
                VoterModel delete = await SearchBrandbyID(id);

                return await _voterRepository.Delete(delete);



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ICollection<VoterModel>> ListVoters()
        {
            try
            {
                return (ICollection<VoterModel>) await _voterRepository.GetAll();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public Task<VoterModel> SearchBrandbyID(int ID)
        {
            try
            {
                return _voterRepository.Get(ID);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ICollection<VoterModel>> FindVoter(string nationality, string idnumber)
        {
            try
            {
                var voters = await ListVoters();
                dynamic voter = voters.Where(x => x.Nationality == nationality && x.IdNumber == idnumber).ToList();
                return voter;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<ICollection<VoterModel>> FindVoterByIdNumber(string? nationality, string? idnumber)
        {
            try
            {
                return await _db.Voters
                       .Where(v => v.Nationality == nationality && v.IdNumber == idnumber)
                       .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //public async Task<VoterModel> UpdateBrand(int id, string stname, string course)
        //{
        //    try
        //    {
        //        VoterModel br = await SearchBrandbyID(id);
        //        br.stname = stname;
        //        return await _voterRepository.Update(br);


        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);

        //    }

        //}
    }
}

