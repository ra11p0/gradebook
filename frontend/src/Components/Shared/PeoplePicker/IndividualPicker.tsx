import { faFilter } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { Checkbox, Input } from '@mui/material';
import React, { ReactElement, useState } from 'react';
import { Button, Collapse } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import PersonResponse from '../../../ApiClient/Schools/Definitions/Responses/PersonResponse';
import InfiniteScrollWrapper from '../InfiniteScrollWrapper';

interface Props {
  showFilters?: boolean;
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
  showFilters,
}: Props): ReactElement {
  const { t } = useTranslation('peoplePicker');
  const [query, setQuery] = useState('');
  const [filtersOpen, setFiltersOpen] = useState(false);
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
              onClick={() => {
                setSelectedPeople((ppl) => ppl.concat());
              }}
            >
              {t('selectAll')}
            </Button>
            <Button variant="link" className="btn-sm ">
              {t('unselectAll')}
            </Button>
          </div>
        </div>
      </div>

      <div id="scrollContainer" className="vh-50 overflow-scroll">
        <Collapse in={filtersOpen}>
          <div>dsa</div>
        </Collapse>
        <InfiniteScrollWrapper
          scrollableTarget="scrollContainer"
          effect={[query]}
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
interface PersonItemProps {
  guid: string;
  selectedPeople: string[];
  name: string;
  surname: string;
  setSelectedPeople: (setFn: (e: string[]) => string[]) => void;
}

function PersonItem(props: PersonItemProps): ReactElement {
  return (
    <>
      <div
        className="d-flex justify-content-between border rounded-2 my-1 p-1 cursor-pointer"
        onClick={() => {
          props.selectedPeople.includes(props.guid)
            ? props.setSelectedPeople((s) => s.filter((p) => p !== props.guid))
            : props.setSelectedPeople((s) => [...s, props.guid]);
        }}
      >
        <div className="my-auto">{`${props.name} ${props.surname}`}</div>
        <div>
          <Checkbox
            checked={props.selectedPeople.includes(props.guid)}
            onChange={(e, o) =>
              o
                ? props.setSelectedPeople((s) => [...s, props.guid])
                : props.setSelectedPeople((s) =>
                    s.filter((p) => p !== props.guid)
                  )
            }
          />
        </div>
      </div>
    </>
  );
}

export default IndividualPicker;
