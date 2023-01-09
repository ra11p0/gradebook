import { ReactElement, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import { ClassResponse } from '../../ApiClient/People/Definitions/Responses/PersonResponse';
import PermissionLevelEnum from '../../Common/Enums/Permissions/PermissionLevelEnum';
import SchoolRolesEnum from '../../Common/Enums/SchoolRolesEnum';
import Class from '../Shared/Class';
import PermissionsBlocker from '../Shared/PermissionsBlocker';

interface Props {
  personName: string;
  personSurname: string;
  personSchoolRole: SchoolRolesEnum;
  activeClass: ClassResponse | undefined;
}

function PersonHeader(props: Props): ReactElement {
  const [activeTab, setActiveTab] = useState<string>('');
  const { t } = useTranslation('personNavigation');
  return (
    <div>
      <Row>
        <Col>
          <Row>
            <Col>
              <Row>
                <Col className="fs-3">{`${props.personName} ${props.personSurname}`}</Col>
              </Row>
              <Row>
                <Col>
                  <small data-testid="schoolRolePersonHeaderHolder">
                    {t(SchoolRolesEnum[props.personSchoolRole])}
                  </small>
                </Col>
              </Row>
            </Col>
            {props.personSchoolRole === SchoolRolesEnum.Student &&
              props.activeClass && (
                <Col>
                  <Class {...props.activeClass} />
                </Col>
              )}
          </Row>
        </Col>
        <Col>
          <div className="d-flex justify-content-end gap-2">
            <Link
              to=""
              className={
                'btn btn-outline-primary ' +
                (activeTab === 'overview' ? 'active' : '')
              }
              onClick={() => {
                setActiveTab('overview');
              }}
            >
              {t('overview')}
            </Link>
            <PermissionsBlocker
              allowingPermissions={[
                PermissionLevelEnum.Permissions_CanManagePermissions,
              ]}
            >
              <Link
                to="permissions"
                className={
                  ' permissions btn btn-outline-primary ' +
                  (activeTab === 'permissions' ? 'active' : '')
                }
                onClick={() => {
                  setActiveTab('permissions');
                }}
              >
                {t('permissions')}
              </Link>
            </PermissionsBlocker>
          </div>
        </Col>
      </Row>
    </div>
  );
}

export default PersonHeader;
