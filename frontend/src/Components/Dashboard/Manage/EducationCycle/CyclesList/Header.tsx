import React from 'react';
import { Button } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router';
import PermissionLevelEnum from '../../../../../Common/Enums/Permissions/PermissionLevelEnum';
import PermissionsBlocker from '../../../../Shared/PermissionsBlocker';

function Header(): React.ReactElement {
  const { t } = useTranslation('educationCycle');
  const navigate = useNavigate();
  return (
    <div className="d-flex justify-content-between">
      <div>
        <h5>{t('educationCycles')}</h5>
      </div>
      <div>
        <PermissionsBlocker
          allowingPermissions={[
            PermissionLevelEnum.EducationCycles_CanCreateAndDelete,
          ]}
        >
          <Button
            data-testid="addNewEducationCycleButton"
            onClick={() => {
              navigate('new');
            }}
          >
            {t('addNewEducationCycle')}
          </Button>
        </PermissionsBlocker>
      </div>
    </div>
  );
}

export default Header;
