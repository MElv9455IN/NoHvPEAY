// 代码生成时间: 2025-10-10 03:52:25
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

// Define the namespace for the Electronic Health Record System.
namespace ElectronicHealthRecordSystem
{
    // Define the Patient entity to represent a patient in the system.
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<MedicalRecord> MedicalRecords { get; set; } = new HashSet<MedicalRecord>();
    }

    // Define the MedicalRecord entity to represent a medical record for a patient.
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateOfRecord { get; set; }
    }

    // Define the context for the Electronic Health Record System.
    public class ElectronicHealthRecordContext : DbContext
    {
        public ElectronicHealthRecordContext(DbContextOptions<ElectronicHealthRecordContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
    }

    // Define the service class for handling operations related to electronic health records.
    public class ElectronicHealthRecordService
    {
        private readonly ElectronicHealthRecordContext _context;

        public ElectronicHealthRecordService(ElectronicHealthRecordContext context)
        {
            _context = context;
        }

        // Method to add a new patient to the system.
        public async Task<bool> AddPatientAsync(Patient patient)
        {
            try
            {
                _context.Patients.Add(patient);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception.
                Console.WriteLine($"Error adding patient: {ex.Message}");
                return false;
            }
        }

        // Method to add a new medical record for a patient.
        public async Task<bool> AddMedicalRecordAsync(int patientId, MedicalRecord record)
        {
            try
            {
                record.PatientId = patientId;
                _context.MedicalRecords.Add(record);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception.
                Console.WriteLine($"Error adding medical record: {ex.Message}");
                return false;
            }
        }

        // Method to retrieve a patient's medical records.
        public async Task<IEnumerable<MedicalRecord>> GetMedicalRecordsForPatientAsync(int patientId)
        {
            try
            {
                return await _context.MedicalRecords.Where(r => r.PatientId == patientId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Log the exception.
                Console.WriteLine($"Error retrieving medical records: {ex.Message}");
                return null;
            }
        }
    }
}
