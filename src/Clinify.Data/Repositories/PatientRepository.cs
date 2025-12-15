using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Clinify.Data.Context;
using Clinify.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clinify.Data.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly ApplicationContext _context;

        public PatientRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Patient entity)
        {
            await _context.Patients.AddAsync(entity);
        }

        public async Task<Patient?> GetAsync(Guid id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task UpdateAsync(Patient entity)
        {
            await Task.Run(() =>
            {
                entity.UpdatedAt = DateTimeOffset.UtcNow;
                _context.Patients.Update(entity);
            });
        }

        public async Task DeleteAsync(Guid id)
        {
            var patient = await this.GetAsync(id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }
        }

        public async Task DeleteAsync(Patient entity)
        {
            await Task.Run(() =>
            {
                _context.Patients.Remove(entity);
            });
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Patients.AnyAsync(x => x.Id == id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
