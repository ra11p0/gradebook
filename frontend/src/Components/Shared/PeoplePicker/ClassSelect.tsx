import React, { ReactElement } from 'react';
import { Form } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import SchoolsProxy from '../../../ApiClient/Schools/SchoolsProxy';
import SelectAsyncPaginate from '../SelectAsyncPaginate';

interface Props {
  onClassChanged: (classGuid: string | undefined) => void;
}

function ClassSelect(props: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  return (
    <div>
      <Form.Label>{t('_class')}</Form.Label>
      <SelectAsyncPaginate
        isClearable
        cacheOptions
        getOptionLabel={(o) => o.name}
        getOptionValue={(o) => o.guid}
        onChange={(e, o) => {
          props.onClassChanged(e?.guid);
        }}
        fetch={async (query: string, page: number) =>
          (await SchoolsProxy.getClassesInSchool(undefined, page, query)).data
        }
      />
    </div>
  );
}

export default ClassSelect;
