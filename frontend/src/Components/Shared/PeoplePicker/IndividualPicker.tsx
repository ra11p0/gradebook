import { faFilter } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Input } from '@mui/material';
import React, { ReactElement, useState } from 'react';
import { Button, Collapse } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import PeoplePickerData from '../../../ApiClient/People/Definitions/Requests/PeoplePickerData';
import { SimplePersonResponse } from '../../../ApiClient/People/Definitions/Responses/PersonResponse';
import PersonResponse from '../../../ApiClient/Schools/Definitions/Responses/PersonResponse';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';
import Filters from './Filters';
import PersonItem from './PersonItem';

interface Props {
  showFilters?: boolean;
  selectedPeople: string[];
  setSelectedPeople: (setFn: (e: string[]) => string[]) => void;
  currentSchoolGuid?: string;
  getPeople: (
    pickerData: PeoplePickerData,
    page: number
  ) => Promise<SimplePersonResponse[]>;
}

function IndividualPicker({
  selectedPeople,
  setSelectedPeople,
  currentSchoolGuid,
  getPeople,
  showFilters,
}: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  const [query, setQuery] = useState('');
  const [filtersOpen, setFiltersOpen] = useState(false);
  const [pickerData, setPickerData] = useState<PeoplePickerData | undefined>(
    undefined
  );
  return (
    <>
      <div className="d-flex justify-content-between">
        {showFilters && (
          <div>
            <Button
              className="btn-sm"
              onClick={() => {
                setFiltersOpen(!filtersOpen);
              }}
            >
              {t('filters')} <FontAwesomeIcon icon={faFilter} />
            </Button>
          </div>
        )}
        <div className={`${showFilters ? 'w-75' : 'w-100'}`}>
          <Input
            type="text"
            className="w-100"
            placeholder={t('searchPeople')}
            value={query}
            onChange={(e) => setQuery(e.target.value)}
          />
          <div className="d-flex justify-content-end">
            <Button
              variant="link"
              className="btn-sm"
              onClick={async () => {
                if (!currentSchoolGuid) return;
                const allPeople = await getPeople(
                  { schoolGuid: currentSchoolGuid, query, ...pickerData },
                  0
                );
                const selectedWithAll = selectedPeople.concat(
                  allPeople.map((e) => e.guid)
                );

                setSelectedPeople(() =>
                  selectedWithAll.filter(
                    (el, index) => selectedWithAll.indexOf(el) === index
                  )
                );
              }}
            >
              {t('selectAll')}
            </Button>
            <Button
              variant="link"
              className="btn-sm "
              onClick={async () => {
                if (!currentSchoolGuid) return;
                const pplToUnselect = await getPeople(
                  { schoolGuid: currentSchoolGuid, query, ...pickerData },
                  0
                );

                setSelectedPeople((ppl) =>
                  ppl.filter(
                    (el) => !pplToUnselect.map((e) => e.guid).includes(el)
                  )
                );
              }}
            >
              {t('unselectAll')}
            </Button>
          </div>
        </div>
      </div>

      <div id="scrollContainer" className="vh-50 overflow-auto p-1">
        <Collapse in={filtersOpen}>
          <div>
            <Filters onChange={setPickerData} />
          </div>
        </Collapse>
        <InfiniteScrollWrapper
          scrollableTarget="scrollContainer"
          effect={[query, pickerData]}
          mapper={(person: PersonResponse, index) => (
            <PersonItem
              {...person}
              key={index}
              selectedPeople={selectedPeople}
              setSelectedPeople={setSelectedPeople}
            />
          )}
          fetch={async (page: number) => {
            if (!currentSchoolGuid) return [];
            return (await getPeople(
              { ...pickerData, schoolGuid: currentSchoolGuid, query },
              page
            )) as [];
          }}
        />
      </div>
    </>
  );
}

export default IndividualPicker;
