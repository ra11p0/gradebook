import React from 'react';
import { Nav } from 'react-bootstrap';
import { withTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import PermissionsBlocker from '../Shared/PermissionsBlocker';

interface DashboardNavigationProps {
  t: any;
}
interface DashboardNavigationState {
  activeTab: string;
}

class DashboardNavigation extends React.Component<
  DashboardNavigationProps,
  DashboardNavigationState
> {
  constructor(props: DashboardNavigationProps) {
    super(props);
    this.state = {
      activeTab: '',
    };
  }

  setActiveTab(tab: string): void {
    this.setState({
      ...this.state,
      activeTab: tab,
    });
  }

  render(): React.ReactNode {
    const { t } = this.props;
    return (
      <div>
        <Nav className="d-flex gap-2 justify-content-end">
          <Link
            to="/dashboard/grades"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'grades' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('grades');
            }}
          >
            {t('grades')}
          </Link>
          <Link
            to="/dashboard/absence"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'absence' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('absence');
            }}
          >
            {t('absence')}
          </Link>
          <Link
            to="/dashboard/manageSubjects"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'subject' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('subject');
            }}
          >
            {t('subjects')}
          </Link>
          <Link
            to="/dashboard/timetable"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'timetable' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('timetable');
            }}
          >
            {t('timetable')}
          </Link>
          <PermissionsBlocker
            allowingPermissions={[
              PermissionLevelEnum.EducationCycles_CanCreateAndDelete,
              PermissionLevelEnum.EducationCycles_ViewOnly,
            ]}
          >
            <Link
              data-testid="educationCycleButton"
              to="/dashboard/educationCycle"
              className={
                'btn btn-outline-primary ' +
                (this.state.activeTab === 'educationCycle' ? 'active' : '')
              }
              onClick={() => {
                this.setActiveTab('educationCycle');
              }}
            >
              {t('educationCycle')}
            </Link>
          </PermissionsBlocker>

          <Link
            to="/dashboard/manageStudents"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'manageStudents' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('manageStudents');
            }}
          >
            {t('manageStudents')}
          </Link>
          <Link
            to="/dashboard/manageTeachers"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'manageTeachers' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('manageTeachers');
            }}
          >
            {t('manageTeachers')}
          </Link>
          <Link
            to="/dashboard/manageClasses"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'manageClasses' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('manageClasses');
            }}
          >
            {t('manageClasses')}
          </Link>
          <Link
            to="/dashboard/manageInvitations"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'manageInvitations' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('manageInvitations');
            }}
          >
            {t('manageInvitations')}
          </Link>
          <Link
            to="/dashboard/manageSchool"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'manageSchool' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('manageSchool');
            }}
          >
            {t('manageSchool')}
          </Link>
          <Link
            to="/dashboard/settings"
            className={
              'btn btn-outline-primary ' +
              (this.state.activeTab === 'settings' ? 'active' : '')
            }
            onClick={() => {
              this.setActiveTab('settings');
            }}
          >
            {t('settings')}
          </Link>
        </Nav>
      </div>
    );
  }
}

export default withTranslation('dashboardNavigation')(DashboardNavigation);
