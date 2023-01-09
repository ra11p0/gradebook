import { faFlagCheckered, faTimes } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { ReactElement, useEffect, useState } from 'react';
import { Spinner } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';
import InfiniteScroll from 'react-infinite-scroll-component';
import LoadingScreen from './LoadingScreen';

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
  const [items, setItems] = useState<T[]>([]);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [lock, setLock] = useState(false);
  useEffect(() => {
    if (lock) return;
    setHasMore(true);
    setPage(1);
    setItems([]);
    void fetch(1);
  }, [...(props.effect ?? [])]);

  const fetch = async (fetchPage: number = page): Promise<void> => {
    setLock(true);
    setPage(fetchPage + 1);
    return await props.fetch(fetchPage).then((result) => {
      setItems((i) => [...i, ...result]);
      if (result.length === 0) setHasMore(false);
      setLock(false);
    });
  };

  return (
    <LoadingScreen isReady={!(items.length === 0 && hasMore)}>
      <InfiniteScroll
        scrollableTarget={props.scrollableTarget}
        dataLength={items.length}
        next={async () => await fetch()}
        hasMore={hasMore}
        loader={<LoadingSpinner />}
        endMessage={items.length !== 0 && <EndMessage />}
      >
        {props.wrapper && <>{props.wrapper(items.map(props.mapper))}</>}
        {!props.wrapper && <>{items.map(props.mapper)}</>}
        {items.length === 0 && !hasMore && <NoResults />}
      </InfiniteScroll>
    </LoadingScreen>
  );
}

export default InfiniteScrollWrapper;
