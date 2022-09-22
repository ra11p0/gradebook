import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { ReactElement, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import GetAccessibleSchoolsResponse from "../../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
import PeopleProxy from "../../ApiClient/People/PeopleProxy";
import {
  setSchoolsListAction,
  setSchoolsListWrapper,
} from "../../ReduxWrappers/setSchoolsListWrapper";
import {
  setSchoolAction,
  setSchoolWrapper,
} from "../../ReduxWrappers/setSchoolWrapper";
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
  currentPersonGuid: state.common.session.personGuid,
  schoolsList: state.common.schoolsList,
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchool: (action: setSchoolAction) => setSchoolWrapper(dispatch, action),
  setSchoolsList: (action: setSchoolsListAction) =>
    setSchoolsListWrapper(dispatch, action),
});
interface SchoolSelectProps {
  className?: string;
  currentSchoolGuid?: string;
  currentPersonGuid?: string;
  schoolsList?: GetAccessibleSchoolsResponse[];
  setSchool?: (action: setSchoolAction) => void;
  setSchoolsList?: (action: setSchoolsListAction) => void;
}
const SchoolSelect = (props: SchoolSelectProps): ReactElement => {
  const { t } = useTranslation("schoolSelect");
  useEffect(() => {
    PeopleProxy.getAccessibleSchools(props.currentPersonGuid!).then(
      (schoolsArray) => {
        props.setSchoolsList!({ schoolsList: schoolsArray.data });
        if (schoolsArray.data.length != 0)
          props.setSchool!({
            schoolGuid: schoolsArray.data[0].guid,
            schoolName: schoolsArray.data[0].name,
          });
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
            let selectedSchool = props.schoolsList?.find(
              (school) => change.target.value == school.guid
            );
            if (!selectedSchool) {
              props.setSchool!({ schoolName: "", schoolGuid: "" });
              return;
            }
            props.setSchool!({
              schoolGuid: selectedSchool?.guid,
              schoolName: selectedSchool?.name,
            });
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
