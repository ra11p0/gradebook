enum UserRolesEnum{
    Student = "Student",
    SchoolAdmin = "SchoolAdmin",
    Teacher = "Teacher",
    Supervisor = "Supervisor"
}

const allUserRoles = [
    UserRolesEnum.SchoolAdmin,
    UserRolesEnum.Student,
    UserRolesEnum.Supervisor,
    UserRolesEnum.Teacher
];

export {
    UserRolesEnum,
    allUserRoles
};
