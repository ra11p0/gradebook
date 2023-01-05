import { ReactElement } from 'react';
import { Form } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import ReactSelect from 'react-select';
import SchoolRolesEnum from '../../../Common/Enums/SchoolRolesEnum';

interface Props {
  onRoleChanged: (schoolRole: SchoolRolesEnum | undefined) => void;
}
function RoleSelect(props: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  return (
    <div>
      <Form.Label>{t('schoolRole')}</Form.Label>
      <ReactSelect
        id="roleFilter"
        data-testid="roleFilter"
        maxMenuHeight={200}
        isClearable
        onChange={(e) => {
          props.onRoleChanged(e?.value as SchoolRolesEnum | undefined);
        }}
        options={Object.values(SchoolRolesEnum)
          .filter((e) => !isNaN(e as number))
          .map((e) => ({
            value: e,
            label: t(`roleName${SchoolRolesEnum[e as number]}`),
          }))}
        placeholder={t('select')}
        noOptionsMessage={() => t('noOptions')}
      />
    </div>
  );
}

export default RoleSelect;
