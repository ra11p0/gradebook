enum UserRolesEnum{
    Student = "Student",
    Teacher = "Teacher",
    SuperAdmin = "SuperAdmin"
}

const allUserRoles = [
    UserRolesEnum.Student,
    UserRolesEnum.Teacher,
    UserRolesEnum.SuperAdmin
];

export {
    UserRolesEnum,
    allUserRoles
};
