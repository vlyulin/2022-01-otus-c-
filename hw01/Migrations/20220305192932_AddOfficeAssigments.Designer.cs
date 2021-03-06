// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using hw01.DAL;

#nullable disable

namespace hw01.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220305192932_AddOfficeAssigments")]
    partial class AddOfficeAssigments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.1.22076.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CourseInstructor", b =>
                {
                    b.Property<int>("Coursesid")
                        .HasColumnType("integer");

                    b.Property<int>("Instructorsid")
                        .HasColumnType("integer");

                    b.HasKey("Coursesid", "Instructorsid");

                    b.HasIndex("Instructorsid");

                    b.ToTable("CourseInstructor");
                });

            modelBuilder.Entity("hw01.DAL.OfficeAssignment", b =>
                {
                    b.Property<int>("InstructorID")
                        .HasColumnType("integer");

                    b.Property<string>("Location")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("InstructorID");

                    b.ToTable("OfficeAssignments");
                });

            modelBuilder.Entity("hw01.Model.Course", b =>
                {
                    b.Property<int>("id")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("hw01.Model.Instructor", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Instructors");
                });

            modelBuilder.Entity("CourseInstructor", b =>
                {
                    b.HasOne("hw01.Model.Course", null)
                        .WithMany()
                        .HasForeignKey("Coursesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("hw01.Model.Instructor", null)
                        .WithMany()
                        .HasForeignKey("Instructorsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("hw01.DAL.OfficeAssignment", b =>
                {
                    b.HasOne("hw01.Model.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });
#pragma warning restore 612, 618
        }
    }
}
