using EfCoreBug.Context;
using EFCoreSaveChangesBug.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFCoreSaveChangesBug
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {

            var context = SetUpContext();

            context.Database.EnsureCreated();

            var subject1 = new Subject
            {
                Name = "subject1"
            };

            context.Add(subject1);

            var classRoom1 = new ClassRoom()
            {
                Name = "classRoom1",
            };

            context.Add(classRoom1);

            var teacher1 = new Teacher()
            {
                Name = "teacher1",
                Subject = subject1
            };

            context.Add(teacher1);

            var classRoom1_teacher1 = new ClassRoomTeacher()
            {
                ClassRoom = classRoom1,
                Teacher = teacher1
            };

            context.Add(classRoom1_teacher1);

            var schoolTest = new School
            {
                Name = "schoolTest",
                Floors = new List<SchoolFloor>
                    {
                        new SchoolFloor()
                        {
                            Floor = 1
                        },
                        new SchoolFloor()
                        {
                            Floor = 2
                        },
                          new SchoolFloor()
                        {
                            Floor = 3
                        },
                    },

                SchoolSubject = new List<SchoolSubject>
                    {
                        new SchoolSubject()
                        {                         
                            Subject = subject1
                        }
                    },
                SchoolSubjectTeacher = new List<SchoolSubjectTeacher>()
                    {
                        new SchoolSubjectTeacher()
                        {
                            ClassRoomTeacher = classRoom1_teacher1,
                            SchoolSubjectTeacherFloor = new List<SchoolSubjectTeacherFloor>()
                            {
                               new SchoolSubjectTeacherFloor()
                               {
                                   Floor = 1,
                                   Grade = GradeEnum.FirstGrade
                               },
                               new SchoolSubjectTeacherFloor()
                               {
                                   Floor = 2,
                                   Grade = GradeEnum.SecondGrade
                               },
                               new SchoolSubjectTeacherFloor()
                               {
                                   Floor = 3,
                                   Grade = GradeEnum.FirstGrade
                               }
                            }
                        }
                }
            };

            context.Add(schoolTest);
            await context.SaveChangesAsync();

            try
            {

                context.ChangeTracker.Clear();

                var schoolDB = context.Set<School>();

                var schoolEdit = schoolDB
                    .Include(c => c.SchoolSubject)
                    .Include(c => c.SchoolSubjectTeacher)
                        .ThenInclude(c => c.SchoolSubjectTeacherFloor)
                    .First();

                schoolEdit.SchoolSubject!.RemoveAll(a => true);
                schoolEdit.SchoolSubjectTeacher!.RemoveAll(a => true);
                context.Update(schoolEdit);

                await context.SaveChangesAsync();

            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);

            }

        }

    
    private static TestDbContext SetUpContext()
        {
            // FOR THE DEVELOPER: Change "GetConnection" to the Database provider that you want to test
            var connection = GetConnectionSQLite();
            // var connection = GetConnectionSQL();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<TestDbContext>(ServiceLifetime.Transient, ServiceLifetime.Transient);
            serviceCollection.AddSingleton(connection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetRequiredService<TestDbContext>();

        }

        // Sqlite
        private static SqliteConnection GetConnectionSQLite()
        {
            Microsoft.Data.Sqlite.SqliteConnectionStringBuilder builder = new Microsoft.Data.Sqlite.SqliteConnectionStringBuilder();
            builder.DataSource = ":memory:";
            builder.Mode = SqliteOpenMode.Memory;
            builder.Cache = SqliteCacheMode.Private;
            string connectionString = builder.ToString();

            var connection = new SqliteConnection(connectionString);

            connection.Open();

            return connection;
        }

        // SqlServer 
        private static SqlConnection GetConnectionSQL()
        {
            string connectionString =
                "Data Source=MX00302\\SQLEXPRESS2019;Initial Catalog=BUGDB;Trusted_Connection=True;";

            var connection = new SqlConnection(connectionString);

            connection.Open();

            return connection;
        }

    }
    public enum GradeEnum
    {
        FirstGrade = 1,
        SecondGrade = 2
    }
}
