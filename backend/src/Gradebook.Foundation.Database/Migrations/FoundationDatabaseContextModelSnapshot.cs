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
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ClassStudent", b =>
                {
                    b.Property<Guid>("ClassesGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StudentsGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("ClassesGuid", "StudentsGuid");

                    b.HasIndex("StudentsGuid");

                    b.ToTable("ClassStudent");
                });

            modelBuilder.Entity("ClassTeacher", b =>
                {
                    b.Property<Guid>("OwnedClassesGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OwnersTeachersGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("OwnedClassesGuid", "OwnersTeachersGuid");

                    b.HasIndex("OwnersTeachersGuid");

                    b.ToTable("ClassTeacher");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Class", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ActiveEducationCycleGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SchoolGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("ActiveEducationCycleGuid");

                    b.HasIndex("SchoolGuid");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycle", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatorGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<Guid>("SchoolGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("CreatorGuid");

                    b.HasIndex("SchoolGuid");

                    b.ToTable("EducationCycles");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleInstance", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("ClassGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("CreatorGuid")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateSince")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DateUntil")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EducationCycleGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Guid");

                    b.HasIndex("ClassGuid");

                    b.HasIndex("CreatorGuid");

                    b.HasIndex("EducationCycleGuid");

                    b.ToTable("EducationCycleInstances");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStep", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EducationCycleGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.HasKey("Guid");

                    b.HasIndex("EducationCycleGuid");

                    b.ToTable("EducationCycleSteps");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepInstance", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DateSince")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateUntil")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EducationCycleInstanceGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EducationCycleStepGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Guid");

                    b.HasIndex("EducationCycleInstanceGuid");

                    b.HasIndex("EducationCycleStepGuid");

                    b.ToTable("EducationCycleStepInstances");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepSubject", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EducationCycleStepGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("GroupsAllowed")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("HoursInStep")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsMandatory")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("SubjectGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("EducationCycleStepGuid");

                    b.HasIndex("SubjectGuid");

                    b.ToTable("EducationCycleStepSubjects");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepSubjectInstance", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AssignedTeacherGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("EducationCycleStepInstanceGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Guid");

                    b.HasIndex("AssignedTeacherGuid");

                    b.HasIndex("EducationCycleStepInstanceGuid");

                    b.ToTable("EducationCycleStepSubjectInstances");
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

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Lesson", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("DateSince")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DateUntil")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("EducationCycleStepSubjectInstanceGuid")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("StartingPersonGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("EducationCycleStepSubjectInstanceGuid");

                    b.HasIndex("StartingPersonGuid");

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

                    b.Property<Guid?>("SchoolGuid")
                        .HasColumnType("char(36)");

                    b.Property<int>("SchoolRole")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserGuid")
                        .HasColumnType("longtext");

                    b.HasKey("Guid");

                    b.HasIndex("CreatorGuid");

                    b.HasIndex("SchoolGuid");

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

                    b.Property<string>("AddressLine1")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("AddressLine2")
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

                    b.Property<Guid>("SchoolGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("SchoolGuid");

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

                    b.Property<Guid>("SchoolGuid")
                        .HasColumnType("char(36)");

                    b.Property<int>("SchoolRole")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UsedDate")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Guid");

                    b.HasIndex("CreatorGuid");

                    b.HasIndex("InvitedPersonGuid");

                    b.HasIndex("SchoolGuid");

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

            modelBuilder.Entity("GroupStudent", b =>
                {
                    b.Property<Guid>("GroupsGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("StudentsGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("GroupsGuid", "StudentsGuid");

                    b.HasIndex("StudentsGuid");

                    b.ToTable("GroupStudent");
                });

            modelBuilder.Entity("GroupTeacher", b =>
                {
                    b.Property<Guid>("OwnedGroupsGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("OwnersTeachersGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("OwnedGroupsGuid", "OwnersTeachersGuid");

                    b.HasIndex("OwnersTeachersGuid");

                    b.ToTable("GroupTeacher");
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.Property<Guid>("SubjectsGuid")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TeachersGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("SubjectsGuid", "TeachersGuid");

                    b.HasIndex("TeachersGuid");

                    b.ToTable("SubjectTeacher");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Administrator", b =>
                {
                    b.HasBaseType("Gradebook.Foundation.Domain.Models.Person");

                    b.HasDiscriminator().HasValue("Administrator");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Student", b =>
                {
                    b.HasBaseType("Gradebook.Foundation.Domain.Models.Person");

                    b.Property<Guid?>("CurrentClassGuid")
                        .HasColumnType("char(36)");

                    b.HasIndex("CurrentClassGuid");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Teacher", b =>
                {
                    b.HasBaseType("Gradebook.Foundation.Domain.Models.Person");

                    b.HasDiscriminator().HasValue("Teacher");
                });

            modelBuilder.Entity("ClassStudent", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Class", null)
                        .WithMany()
                        .HasForeignKey("ClassesGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ClassTeacher", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Class", null)
                        .WithMany()
                        .HasForeignKey("OwnedClassesGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("OwnersTeachersGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Class", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycle", "ActiveEducationCycle")
                        .WithMany()
                        .HasForeignKey("ActiveEducationCycleGuid");

                    b.HasOne("Gradebook.Foundation.Domain.Models.School", "School")
                        .WithMany("Classes")
                        .HasForeignKey("SchoolGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ActiveEducationCycle");

                    b.Navigation("School");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycle", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("School");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleInstance", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycle", "EducationCycle")
                        .WithMany()
                        .HasForeignKey("EducationCycleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Creator");

                    b.Navigation("EducationCycle");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStep", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycle", "EducationCycle")
                        .WithMany("EducationCycleSteps")
                        .HasForeignKey("EducationCycleGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationCycle");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepInstance", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycleInstance", "EducationCycleInstance")
                        .WithMany("EducationCycleStepInstances")
                        .HasForeignKey("EducationCycleInstanceGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycleStep", "EducationCycleStep")
                        .WithMany()
                        .HasForeignKey("EducationCycleStepGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationCycleInstance");

                    b.Navigation("EducationCycleStep");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepSubject", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycleStep", "EducationCycleStep")
                        .WithMany("EducationCycleStepSubjects")
                        .HasForeignKey("EducationCycleStepGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationCycleStep");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepSubjectInstance", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Teacher", "AssignedTeacher")
                        .WithMany()
                        .HasForeignKey("AssignedTeacherGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycleStepInstance", "EducationCycleStepInstance")
                        .WithMany("EducationCycleStepSubjectInstances")
                        .HasForeignKey("EducationCycleStepInstanceGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTeacher");

                    b.Navigation("EducationCycleStepInstance");
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

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Lesson", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.EducationCycleStepSubjectInstance", "EducationCycleStepSubjectInstance")
                        .WithMany()
                        .HasForeignKey("EducationCycleStepSubjectInstanceGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", "StartingPerson")
                        .WithMany()
                        .HasForeignKey("StartingPersonGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EducationCycleStepSubjectInstance");

                    b.Navigation("StartingPerson");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Person", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Administrator", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorGuid");

                    b.HasOne("Gradebook.Foundation.Domain.Models.School", "School")
                        .WithMany("People")
                        .HasForeignKey("SchoolGuid");

                    b.Navigation("Creator");

                    b.Navigation("School");
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

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Subject", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.School", "School")
                        .WithMany("Subjects")
                        .HasForeignKey("SchoolGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
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

                    b.HasOne("Gradebook.Foundation.Domain.Models.School", "School")
                        .WithMany()
                        .HasForeignKey("SchoolGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("InvitedPerson");

                    b.Navigation("School");
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

            modelBuilder.Entity("GroupStudent", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GroupTeacher", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("OwnedGroupsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("OwnersTeachersGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SubjectTeacher", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Subject", null)
                        .WithMany()
                        .HasForeignKey("SubjectsGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gradebook.Foundation.Domain.Models.Teacher", null)
                        .WithMany()
                        .HasForeignKey("TeachersGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Student", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Class", "CurrentClass")
                        .WithMany("ActiveStudents")
                        .HasForeignKey("CurrentClassGuid");

                    b.Navigation("CurrentClass");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Class", b =>
                {
                    b.Navigation("ActiveStudents");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycle", b =>
                {
                    b.Navigation("EducationCycleSteps");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleInstance", b =>
                {
                    b.Navigation("EducationCycleStepInstances");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStep", b =>
                {
                    b.Navigation("EducationCycleStepSubjects");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.EducationCycleStepInstance", b =>
                {
                    b.Navigation("EducationCycleStepSubjectInstances");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.School", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("People");

                    b.Navigation("Subjects");
                });
#pragma warning restore 612, 618
        }
    }
}
