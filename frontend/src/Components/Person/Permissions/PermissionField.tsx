import { MenuItem, Select } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { Col, Row } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import PermissionEnum from '../../../Common/Enums/Permissions/PermissionEnum';
import PermissionLevelEnum from '../../../Common/Enums/Permissions/PermissionLevelEnum';

interface Props {
  permission: {
    permissionId: PermissionEnum;
    permissionLevel: PermissionLevelEnum;
    permissionLevels: PermissionLevelEnum[];
  };
  onChange: (
    permission: PermissionEnum,
    permissionLevel: PermissionLevelEnum
  ) => void;
}

function PermissionField(props: Props): JSX.Element {
  const { t } = useTranslation('permissions');
  const [selectedPermissionLevel, setSelectedPermissionLevel] =
    useState<PermissionLevelEnum>(props.permission.permissionLevel);
  useEffect(() => {
    setSelectedPermissionLevel(props.permission.permissionLevel);
  }, [props.permission.permissionLevel]);
  return (
    <>
      <Row
        className={`permission_${props.permission.permissionId} m-1 p-1 border-bottom`}
      >
        <Col className="my-auto">
          {t(`permission_${props.permission.permissionId}`)}
        </Col>
        <Col className="my-auto">
          <small>
            {t(`permission_${props.permission.permissionId}_description`, '')}
          </small>
        </Col>
        <Col className="my-auto">
          <Select
            size="small"
            className="setDefaultPersonGuidSelect form-control"
            value={
              props.permission.permissionLevels.includes(
                selectedPermissionLevel
              )
                ? selectedPermissionLevel.toString()
                : ''
            }
            onChange={(e) => {
              setSelectedPermissionLevel(parseInt(e.target.value));
              props.onChange(
                props.permission.permissionId,
                parseInt(e.target.value)
              );
            }}
            renderValue={(selected: string) => t(`permissionLevel_${selected}`)}
          >
            {props.permission.permissionLevels.map((permissionLevel) => (
              <MenuItem key={permissionLevel} value={permissionLevel}>
                {t(`permissionLevel_${permissionLevel}`)}
              </MenuItem>
            ))}
          </Select>
        </Col>
      </Row>
    </>
  );
}

export default PermissionField;
