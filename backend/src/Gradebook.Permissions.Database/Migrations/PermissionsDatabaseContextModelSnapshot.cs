﻿// <auto-generated />
using System;
using Gradebook.Permissions.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gradebook.Permissions.Database.Migrations
{
    [DbContext(typeof(PermissionsDatabaseContext))]
    partial class PermissionsDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
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

                    b.HasIndex("SchoolGuid");

                    b.ToTable("Class");
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

                    b.ToTable("Group");
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

                    b.ToTable("School");
                });

            modelBuilder.Entity("Gradebook.Permissions.Domain.Models.Permission", b =>
                {
                    b.Property<Guid>("Guid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionLevel")
                        .HasColumnType("int");

                    b.Property<Guid>("PersonGuid")
                        .HasColumnType("char(36)");

                    b.HasKey("Guid");

                    b.HasIndex("PersonGuid");

                    b.ToTable("Permissions");
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
                    b.HasOne("Gradebook.Foundation.Domain.Models.School", "School")
                        .WithMany("Classes")
                        .HasForeignKey("SchoolGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.Person", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Administrator", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorGuid");

                    b.Navigation("Creator");
                });

            modelBuilder.Entity("Gradebook.Permissions.Domain.Models.Permission", b =>
                {
                    b.HasOne("Gradebook.Foundation.Domain.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonGuid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
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

            modelBuilder.Entity("Gradebook.Foundation.Domain.Models.School", b =>
                {
                    b.Navigation("Classes");
                });
#pragma warning restore 612, 618
        }
    }
}
