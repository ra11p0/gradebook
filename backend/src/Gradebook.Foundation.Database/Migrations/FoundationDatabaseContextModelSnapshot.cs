﻿// <auto-generated />
using System;
using Gradebook.Foundation.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gradebook.Foundation.Database.Migrations
{
    [DbContext(typeof(FoundationDatabaseContext))]
    partial class FoundationDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Class", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Guid");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Grade", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("LessonGuid")
                        .HasColumnType("char(36)");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<Guid>("RelatedGradeGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeacherGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("LessonGuid");

                    b.HasIndex("RelatedGradeGuid");

                    b.HasIndex("StudentGuid");

                    b.HasIndex("TeacherGuid");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Group", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClassGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.HasIndex("ClassGuid");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Lesson", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Guid");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Person", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("CreatorGuid")
                        .HasColumnType("char(36)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SchoolRole")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserGuid")
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.HasIndex("CreatorGuid");

                    b.ToTable("Person");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Position", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.School", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Address2")
                        .HasColumnType("longtext");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.StudentsAbsence", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("SinceDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("StudentGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UntilDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Guid");

                    b.HasIndex("StudentGuid");

                    b.ToTable("StudentsAbsences");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Subject", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.SystemInvitation", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatorGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("ExprationDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("InvitationCode")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid?>("InvitedPersonGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsUsed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("SchoolRole")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UsedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Guid");

                    b.HasIndex("CreatorGuid");

                    b.HasIndex("InvitedPersonGuid");

                    b.ToTable("SystemInvitations");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.TeachersAbsence", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("SinceDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("TeacherGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("UntilDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Guid");

                    b.HasIndex("TeacherGuid");

                    b.ToTable("TeachersAbsences");
                });

            modelBuilder.Entity("PersonSchool", b =>
                {
                    b.Property<Guid>("PeopleGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("SchoolsGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("PeopleGuid", "SchoolsGuid");

                    b.HasIndex("SchoolsGuid");

                    b.ToTable("PersonSchool");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Administrator", b =>
                {
                    b.HasBaseType("Gradebook.Foundation.Domain.Models.Person");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Student", b =>
                {
                    b.HasBaseType("Gradebook.Foundation.Domain.Models.Person");

                    b.Property<Guid?>("ClassGuid")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("GroupGuid")
                        .IsRequired()
                        .HasColumnType("char(36)");

                    b.HasIndex("ClassGuid");

                    b.HasIndex("GroupGuid");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Teacher", b =>
                {
                    b.HasBaseType("Gradebook.Foundation.Domain.Models.Person");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Grade", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Grade", "RelatedGrade")
                        .WithMany()
                        .HasForeignKey("RelatedGradeGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("RelatedGrade");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Group", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Person", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Administrator", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorGuid");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.StudentsAbsence", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.SystemInvitation", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", "InvitedPerson")
                        .WithMany()
                        .HasForeignKey("InvitedPersonGuid");

                    b.Navigation("Creator");

                    b.Navigation("InvitedPerson");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.TeachersAbsence", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("PersonSchool", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PeopleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.School", null)
                        .WithMany()
                        .HasForeignKey("SchoolsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Student", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Class", "Class")
                        .WithMany("Students")
                        .HasForeignKey("ClassGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Group", "Group")
                        .WithMany("Students")
                        .HasForeignKey("GroupGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Class", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Group", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
