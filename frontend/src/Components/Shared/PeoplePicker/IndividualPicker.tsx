import { Checkbox, Input } from '@mui/material';
import React, { ReactElement, useState } from 'react';
import { useTranslation } from 'react-i18next';
import PersonResponse from '../../../ApiClient/Schools/Definitions/Responses/PersonResponse';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';

interface Props {
  discriminator?: string;
  selectedPeople: string[];
  setSelectedPeople: (setFn: (e: string[]) => string[]) => void;
  currentSchoolGuid?: string;
  getPeople: (
    schoolGuid: string,
    schoolRole: string,
    query: string,
    page: number
  ) => Promise<PersonResponse[]>;
}

function IndividualPicker({
  selectedPeople,
  setSelectedPeople,
  currentSchoolGuid,
  getPeople,
  discriminator,
}: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  const [query, setQuery] = useState('');
  return (
    <>
      <Input
        type="text"
        className="w-100"
        placeholder={t('searchPeople')}
        value={query}
        onChange={(e) => setQuery(e.target.value)}
      />
      <div id="scrollContainer" className="vh-50 overflow-scroll">
        <InfiniteScrollWrapper
          scrollableTarget="scrollContainer"
          effect={[query]}
          mapper={(person: PersonResponse, index) => (
            <div
              key={index}
              className="d-flex justify-content-between border rounded-2 my-1 p-1 cursor-pointer"
              onClick={() => {
                selectedPeople.includes(person.guid)
                  ? setSelectedPeople((s) => s.filter((p) => p !== person.guid))
                  : setSelectedPeople((s) => [...s, person.guid]);
              }}
            >
              <div className="my-auto">{`${person.name} ${person.surname}`}</div>
              <div>
                <Checkbox
                  checked={selectedPeople.includes(person.guid)}
                  onChange={(e, o) =>
                    o
                      ? setSelectedPeople((s) => [...s, person.guid])
                      : setSelectedPeople((s) =>
                          s.filter((p) => p !== person.guid)
                        )
                  }
                />
              </div>
            </div>
          )}
          fetch={async (page: number) => {
            if (!currentSchoolGuid) return [];
            return (await getPeople(
              currentSchoolGuid,
              discriminator ?? '',
              query,
              page
            )) as [];
          }}
        />
      </div>
    </>
  );
}

export default IndividualPicker;
