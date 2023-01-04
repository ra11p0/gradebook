import React, { ReactElement } from 'react';
import { useTranslation } from 'react-i18next';
import { ActionMeta, SingleValue } from 'react-select';
import { AsyncPaginate } from 'react-select-async-paginate';

interface Props<T> {
  className?: string;
  isClearable?: boolean;
  cacheOptions?: boolean;
  id?: string;
  value?: T;
  onChange?: (newValue: SingleValue<T>, actionMeta: ActionMeta<T>) => void;
  fetch: (query: string, page: number) => Promise<T[]> | T[];
  getOptionValue?: (option: T) => any;
  getOptionLabel?: (option: T) => string;
  onMenuClose?: () => void;
}

function SelectAsyncPaginate<T>(props: Props<T>): ReactElement {
  const { t } = useTranslation();
  const loadOptions = async (
    searchQuery: any,
    loadedOptions: any,
    { page }: any
  ): Promise<{
    options: T[];
    hasMore: boolean;
    additional: { page: number };
  }> => {
    const response = await props.fetch(searchQuery, page);
    return {
      options: response,
      hasMore: response.length >= 1,
      additional: {
        page: searchQuery ? 2 : (page as number) + 1,
      },
    };
  };
  return (
    <AsyncPaginate
      className=""
      inputId={props.id}
      id={props.id}
      placeholder={t('select')}
      noOptionsMessage={() => t('noOptions')}
      onMenuClose={props.onMenuClose}
      defaultOptions
      value={props.value}
      loadOptions={loadOptions}
      getOptionValue={props.getOptionValue}
      getOptionLabel={props.getOptionLabel}
      onChange={props.onChange}
      additional={{
        page: 1,
      }}
      {...props}
    />
  );
}

export default SelectAsyncPaginate;
