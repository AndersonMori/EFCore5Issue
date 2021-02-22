using EFCoreSaveChangesBug.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;


namespace EfCoreBug.Context
{
    // FOR THE DEVELOPER: Change "_Connection" and Constructor to the Database provider that you want to test
    // Same thing to the "optionsBuilder" on the "OnConfiguring"
    public class TestDbContext : DbContext
    {
         private readonly SqliteConnection _Connection;
        //private readonly SqlConnection _Connection;

        public TestDbContext(DbContextOptions<TestDbContext> options, SqliteConnection connection)
            : base(options)
        {
            this._Connection = connection;
        }

        public TestDbContext(DbContextOptions<TestDbContext> options, SqlConnection connection)
        : base(options)
        {
            // this._Connection = connection;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region School
            modelBuilder.Entity<School>().ToTable("School");


            modelBuilder.Entity<School>().HasMany(c => c.SchoolSubject)
               .WithOne(c => c.School!)
               .HasForeignKey(ca => ca.SchoolID)
               .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<School>().HasMany(c => c.Floors)
               .WithOne(c => c.School!)
               .HasForeignKey(cn => cn.SchoolID)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<School>().HasMany(c => c.SchoolSubjectTeacher)
                .WithOne(c => c.School!)
                .HasForeignKey(c => c.SchoolID)
                .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region SchoolSubject
            modelBuilder.Entity<SchoolSubject>().ToTable("SchoolSubject");

            modelBuilder.Entity<SchoolSubject>().HasAlternateKey(c => new { c.SchoolID, c.SubjectID });

            #endregion

            #region SchoolFloor
            modelBuilder.Entity<SchoolFloor>().ToTable("SchoolFloor");

            modelBuilder.Entity<SchoolFloor>().HasAlternateKey(cn => new { cn.SchoolID, cn.Floor });


            #endregion

            #region SchoolSubjectTeacher
            modelBuilder.Entity<SchoolSubjectTeacher>().ToTable("SchoolSubjectTeacher");

            modelBuilder.Entity<SchoolSubjectTeacher>().HasAlternateKey(c => new { c.SchoolID, c.ClassRoomTeacherID });

            modelBuilder.Entity<SchoolSubjectTeacher>().HasMany(cn => cn.SchoolSubjectTeacherFloor)
                .WithOne(cain => cain.SchoolSubjectTeacher!)
                .HasForeignKey(cain => cain.SchoolSubjectTeacherID)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region SchoolSubjectTeacherFloor
            modelBuilder.Entity<SchoolSubjectTeacherFloor>().ToTable("SchoolSubjectTeacherFloor");

            modelBuilder.Entity<SchoolSubjectTeacherFloor>().HasAlternateKey(cain => new { cain.SchoolSubjectTeacherID, cain.Floor });

            #endregion

            #region ClassRoomTeacher
            modelBuilder.Entity<ClassRoomTeacher>().ToTable("ClassRoomTeacher");

            modelBuilder.Entity<ClassRoomTeacher>().HasAlternateKey(c => new { c.ClassRoomID, c.TeacherID });

            modelBuilder.Entity<ClassRoomTeacher>().HasOne(c => c.Teacher)
                .WithMany(i => i!.ClassRoomTeacher)
                .HasForeignKey(c => c.TeacherID);
            modelBuilder.Entity<ClassRoomTeacher>().HasOne(c => c.ClassRoom)
               .WithMany(g => g!.ClassRoomTeachers)
               .HasForeignKey(c => c.ClassRoomID);
            modelBuilder.Entity<ClassRoomTeacher>().HasMany(c => c.SchoolSubjectTeachers)
               .WithOne(c => c.ClassRoomTeacher!)
               .HasForeignKey(p => p.ClassRoomTeacherID)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region Teacher
            modelBuilder.Entity<Teacher>().ToTable("Teacher");

            modelBuilder.Entity<Teacher>().HasOne(i => i.Subject)
                .WithMany()
                .HasForeignKey(i => i.SubjectID)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            modelBuilder.Entity<Teacher>().HasMany(i => i.ClassRoomTeacher)
                .WithOne(g => g.Teacher!)
                .HasForeignKey(g => g.TeacherID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region ClassRoom
            modelBuilder.Entity<ClassRoom>().ToTable("ClassRoom");      

            modelBuilder.Entity<ClassRoom>().HasMany(a => a.ClassRoomTeachers)
                .WithOne(ia => ia.ClassRoom!)
                .HasForeignKey(ia => ia.ClassRoomID)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Subject
            modelBuilder.Entity<Subject>().ToTable("Subject");

            modelBuilder.Entity<Subject>().HasMany(c => c.SchoolSubjects)
               .WithOne(ca => ca.Subject!)
               .HasForeignKey(p => p.SubjectID)
               .OnDelete(DeleteBehavior.Cascade);
            #endregion


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this._Connection);
           // optionsBuilder.UseSqlServer(this._Connection);
        }
    }
}
