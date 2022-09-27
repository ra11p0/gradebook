import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { ReactElement } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import GetSchoolResponse from "../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse";
import { currentSchoolProxy } from "../../Redux/ReduxProxy/currentSchoolProxy";
import { schoolsListProxy } from "../../Redux/ReduxProxy/schoolsListProxy";
import { setSchoolsListAction, setSchoolsListWrapper } from "../../Redux/ReduxWrappers/setSchoolsListWrapper";
import { setSchoolAction, setSchoolWrapper } from "../../Redux/ReduxWrappers/setSchoolWrapper";
const mapStateToProps = (state: any) => ({
  currentSchool: currentSchoolProxy(state),
  currentUserId: state.common.session.userId,
  schoolsList: schoolsListProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchool: (action: setSchoolAction) => setSchoolWrapper(dispatch, action),
  setSchoolsList: (action: setSchoolsListAction) => setSchoolsListWrapper(dispatch, action),
});
interface SchoolSelectProps {
  className?: string;
  currentSchool?: any;
  currentUserId?: string;
  schoolsList: GetSchoolResponse[] | null;
  setSchool: (action: setSchoolAction) => void;
  setSchoolsList: (action: setSchoolsListAction) => void;
}
const SchoolSelect = (props: SchoolSelectProps): ReactElement => {
  const { t } = useTranslation("schoolSelect");

  return (
    <div className={`${props.className}`}>
      <FormControl sx={{ m: 1, minWidth: 380 }}>
        <InputLabel>{t("selectSchool")}</InputLabel>
        <Select
          value={props.currentSchool?.schoolGuid ?? ""}
          label={t("selectSchool")}
          onChange={(change) => {
            let selectedSchool = props.schoolsList?.find((school) => change.target.value == school.guid);
            if (!selectedSchool) {
              props.setSchool!({ schoolName: "", schoolGuid: "" });
              return;
            }
            props.setSchool!({
              schoolGuid: selectedSchool.guid,
              schoolName: selectedSchool.name,
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
