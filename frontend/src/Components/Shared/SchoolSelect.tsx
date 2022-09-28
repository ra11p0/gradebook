import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { ReactElement } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import GetSchoolResponse from "../../ApiClient/Schools/Definitions/Responses/GetSchoolResponse";
import getCurrentSchoolReduxProxy from "../../Redux/ReduxProxy/getCurrentSchoolReduxProxy";
import getSchoolsListReduxProxy from "../../Redux/ReduxProxy/getSchoolsListReduxProxy";
import setSchoolsListReduxWrapper, { setSchoolsListAction } from "../../Redux/ReduxWrappers/setSchoolsListReduxWrapper";
import setSchoolReduxWrapper, { setSchoolAction } from "../../Redux/ReduxWrappers/setSchoolReduxWrapper";
const mapStateToProps = (state: any) => ({
  currentSchool: getCurrentSchoolReduxProxy(state),
  currentUserId: state.common.session.userId,
  schoolsList: getSchoolsListReduxProxy(state),
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchool: (action: setSchoolAction) => setSchoolReduxWrapper(dispatch, action),
  setSchoolsList: (action: setSchoolsListAction) => setSchoolsListReduxWrapper(dispatch, action),
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
