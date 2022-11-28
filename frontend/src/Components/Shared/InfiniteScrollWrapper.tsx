import { faFlagCheckered, faTimes } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { ReactElement, useEffect, useState } from 'react';
import { Spinner } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import InfiniteScroll from 'react-infinite-scroll-component';

interface Props<T> {
  mapper: (item: T, index: number) => ReactElement;
  wrapper?: (items: ReactElement[]) => ReactElement;
  fetch: (page: number) => Promise<T[]>;
  effect?: any;
  scrollableTarget?: string;
}

function LoadingSpinner(): ReactElement {
  const { t } = useTranslation();
  return (
    <div className="d-flex gap-3 justify-content-center">
      <div className="my-auto">
        <Spinner animation="border" role="status">
          <span className="visually-hidden">{t('loading')}</span>
        </Spinner>
      </div>
    </div>
  );
}

function NoResults(): ReactElement {
  const { t } = useTranslation();
  return (
    <div className="d-flex gap-3 justify-content-center">
      <div className="my-auto">
        <FontAwesomeIcon icon={faTimes} />
      </div>
      {t('noResults')}
    </div>
  );
}

function EndMessage(): ReactElement {
  const { t } = useTranslation();
  return (
    <div className="d-flex gap-3 justify-content-center ">
      <div className="my-auto">
        <FontAwesomeIcon icon={faFlagCheckered} />
      </div>
      {t('noMoreResults')}
    </div>
  );
}

function InfiniteScrollWrapper<T>(props: Props<T>): ReactElement {
  const [items, setItems] = useState([]);
  const [itemsCount, setItemsCount] = useState(0);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    setIsReady(true);
    void fetch(1, true);
  }, [...(props.effect ?? [])]);

  const fetch = async (
    fetchPage: number,
    withReset: boolean = false
  ): Promise<unknown> => {
    if (!isReady && !withReset)
      return await new Promise((resolve, reject): void => resolve(null));
    return await props.fetch(fetchPage).then((result) => {
      if (withReset) {
        setItemsCount(0);
        setHasMore(true);
        setPage(1);
        setItems([]);
      }
      setItemsCount((c) => c + result.length);
      setItems((i) => i.concat(result as []));
      if (result.length === 0) {
        setHasMore(false);
        return;
      }
      setPage((p) => p + 1);
    });
  };

  return (
    <InfiniteScroll
      scrollableTarget={props.scrollableTarget}
      dataLength={itemsCount}
      next={async () => await fetch(page)}
      hasMore={hasMore}
      loader={<LoadingSpinner />}
      endMessage={itemsCount !== 0 && <EndMessage />}
    >
      {props.wrapper && <>{props.wrapper(items.map(props.mapper))}</>}
      {!props.wrapper && <>{items.map(props.mapper)}</>}
      {itemsCount === 0 && !hasMore && <NoResults />}
      {itemsCount === 0 && hasMore && <LoadingSpinner />}
    </InfiniteScroll>
  );
}

export default InfiniteScrollWrapper;
