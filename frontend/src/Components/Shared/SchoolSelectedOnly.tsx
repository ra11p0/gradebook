import React from "react";
import { connect } from "react-redux";
import { t } from "i18next";
import { isSchoolSelectedProxy } from "../../Redux/ReduxProxy/isSchoolSelectedProxy";

const mapStateToProps = (state: any) => ({
  isSchoolSelected: isSchoolSelectedProxy(state),
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
