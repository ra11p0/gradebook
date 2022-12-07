import _ from 'lodash';
import React, { ReactElement, useState } from 'react';

import AsyncSelect from 'react-select/async';
import AccountsProxy from '../../../../../../ApiClient/Accounts/AccountsProxy';

interface Props {
  formik: any;
}

function DefaultSchoolSetting(props: Props): ReactElement {
  const [schools, setSchools] = useState<
    Array<{ value: string; label: string }>
  >([]);
  return (
    <>
      <AsyncSelect
        cacheOptions
        defaultOptions
        value={{
          value: _.get(props.formik.values, 'defaultSchool'),
          label: schools.find(
            (e) => e.value === _.get(props.formik.values, 'defaultSchool')
          )?.label,
        }}
        onChange={(e) => {
          props.formik.setFieldValue('defaultSchool', e?.value);
        }}
        loadOptions={async () => {
          const schools = (await AccountsProxy.getAccessibleSchools()).data.map(
            (e) => ({
              value: e.school.guid,
              label: e.school.name,
            })
          );
          setSchools(schools);
          return schools;
        }}
      />
    </>
  );
}

export default DefaultSchoolSetting;
