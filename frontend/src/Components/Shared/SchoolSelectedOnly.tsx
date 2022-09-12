import React from "react";
import { connect } from "react-redux";
import { t } from "i18next";

const mapStateToProps = (state: any) => ({
  isSchoolSelected: (() => {
    if (!state.common.school) return false;
    if (!state.common.school.schoolGuid) return false;
    return true;
  })(),
});

const mapDispatchToProps = (dispatch: any) => ({});

interface Props {
  isSchoolSelected?: boolean;
  children: any;
}

class SchoolSelectedOnly extends React.Component<Props> {
  render(): React.ReactNode {
    return (
      <>
        {this.props.isSchoolSelected ? (
          <>{this.props.children}</>
        ) : (
          <>
            <div className="text-center">
              <>
                <div className="display-6 ">
                  <>{t("schoolIsNotSelected")} </>
                </div>
                <div>
                  <>{t("selectSchoolToManageStudents")}</>{" "}
                </div>
              </>
            </div>
          </>
        )}
      </>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SchoolSelectedOnly);
