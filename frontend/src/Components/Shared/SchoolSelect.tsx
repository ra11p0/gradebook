import { FormControl, InputLabel, MenuItem, Select } from "@mui/material";
import { ReactElement, useEffect, useState } from "react";
import { useTranslation } from "react-i18next";
import { connect } from "react-redux";
import { setSchool } from "../../Actions/Account/accountActions";
import GetAccessibleSchoolsResponse from "../../ApiClient/People/Definitions/GetAccessibleSchoolsResponse";
import PeopleProxy from "../../ApiClient/People/PeopleProxy";
const mapStateToProps = (state: any) => ({
  currentSchoolGuid: state.common.school?.schoolGuid,
  currentPersonGuid: state.common.session.personGuid,
});

const mapDispatchToProps = (dispatch: any) => ({
  setSchoolGuid: (schoolGuid: string) => {
    dispatch({ ...setSchool, schoolGuid });
  },
});
interface SchoolSelectProps {
  className?: string;
  currentSchoolGuid?: string;
  currentPersonGuid?: string;
  setSchoolGuid?: (schoolGuid: string) => void;
}
const SchoolSelect = (props: SchoolSelectProps): ReactElement => {
  const { t } = useTranslation("schoolSelect");
  const [schools, setSchools] = useState<GetAccessibleSchoolsResponse[] | null>(
    null
  );
  useEffect(() => {
    PeopleProxy.getAccessibleSchools(props.currentPersonGuid!).then(
      (schoolsArray) => {
        setSchools(schoolsArray.data);
      }
    );
  }, []);

  return (
    <div className={`${props.className}`}>
      <FormControl sx={{ m: 1, minWidth: 380 }}>
        <InputLabel>{t("selectSchool")}</InputLabel>
        <Select
          value={props.currentSchoolGuid}
          label={t("selectSchool")}
          onChange={(change) => {
            props.setSchoolGuid!(change.target.value);
          }}
        >
          {schools?.map((school) => (
            <MenuItem value={school.guid}>{school.name}</MenuItem>
          ))}
        </Select>
      </FormControl>
    </div>
  );
};
export default connect(mapStateToProps, mapDispatchToProps)(SchoolSelect);
