import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { ReactElement, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import {
  setSchool,
  setSchoolsList,
} from "../../Actions/Account/accountActions";
import GetAccessibleSchoolsResponse from "../../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
import PeopleProxy from "../../ApiClient/People/PeopleProxy";
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
  currentPersonGuid: state.common.session.personGuid,
  schoolsList: state.common.schoolsList,
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolGuid: (schoolGuid: string) => {
    dispatch({ ...setSchool, schoolGuid });
  },
  setSchoolsList: (schoolsList: GetAccessibleSchoolsResponse[]) => {
    dispatch({ ...setSchoolsList, schoolsList });
  },
});
interface SchoolSelectProps {
  className?: string;
  currentSchoolGuid?: string;
  currentPersonGuid?: string;
  schoolsList?: GetAccessibleSchoolsResponse[];
  setSchoolGuid?: (schoolGuid: string) => void;
  setSchoolsList?: (schoolsList: GetAccessibleSchoolsResponse[]) => void;
}
const SchoolSelect = (props: SchoolSelectProps): ReactElement => {
  const { t } = useTranslation("schoolSelect");
  useEffect(() => {
    PeopleProxy.getAccessibleSchools(props.currentPersonGuid!).then(
      (schoolsArray) => {
        props.setSchoolsList!(schoolsArray.data);
        if (schoolsArray.data.length != 0)
          props.setSchoolGuid!(schoolsArray.data[0].guid);
      }
    );
  }, []);

  return (
    <div className={`${props.className}`}>
      <FormControl sx={{ m: 1, minWidth: 380 }}>
        <InputLabel>{t("selectSchool")}</InputLabel>
        <Select
          value={
            props.schoolsList?.find(
              (school) => school.guid == props.currentSchoolGuid
            )?.guid ?? ""
          }
          label={t("selectSchool")}
          onChange={(change) => {
            props.setSchoolGuid!(change.target.value);
          }}
        >
          {props.schoolsList?.map((school) => (
            <MenuItem value={school.guid} key={school.guid}>
              {school.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(SchoolSelect);
