import React from "react";
import { ActionMeta, SingleValue } from "react-select";
import { AsyncPaginate } from "react-select-async-paginate";

interface Props<T> {
  value?: T;
  onChange?: (newValue: SingleValue<T>, actionMeta: ActionMeta<T>) => void;
  fetch: (query: string, page: number) => Promise<T[]>;
  getOptionValue?: (option: T) => any;
  getOptionLabel?: (option: T) => string;
  onMenuClose?: () => void
}

function SelectAsyncPaginate<T>(props: Props<T>) {
  const loadOptions = async (searchQuery: any, loadedOptions: any, { page }: any) => {
    const response = await props.fetch(searchQuery, page);
    return {
      options: response,
      hasMore: response.length >= 1,
      additional: {
        page: searchQuery ? 2 : page + 1,
      },
    };
  };
  return (
    <AsyncPaginate
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
    />
  );
}

export default SelectAsyncPaginate;
