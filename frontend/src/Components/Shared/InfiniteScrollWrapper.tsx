import { faFlagCheckered, faTimes } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React, { ReactElement, useEffect, useState } from "react";
import { Spinner } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import InfiniteScroll from "react-infinite-scroll-component";

type Props = {
  mapper: (item: any, index: number) => ReactElement;
  fetch: (page: number) => Promise<any[]>;
  effect?: any;
};

function LoadingSpinner() {
  const { t } = useTranslation();
  return (
    <div className="d-flex gap-3 justify-content-center">
      <div className="my-auto">
        <Spinner animation="border" role="status">
          <span className="visually-hidden">{t("loading")}</span>
        </Spinner>
      </div>
    </div>
  );
}

function NoResults() {
  const { t } = useTranslation();
  return (
    <div className="d-flex gap-3 justify-content-center">
      <div className="my-auto">
        <FontAwesomeIcon icon={faTimes} />
      </div>
      {t("noResults")}
    </div>
  );
}

function EndMessage() {
  const { t } = useTranslation();
  return (
    <div className="d-flex gap-3 justify-content-center ">
      <div className="my-auto">
        <FontAwesomeIcon icon={faFlagCheckered} />
      </div>
      {t("noMoreResults")}
    </div>
  );
}

function InfiniteScrollWrapper(props: Props) {
  const [items, setItems] = useState([]);
  const [itemsCount, setItemsCount] = useState(0);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);
  const [isReady, setIsReady] = useState(false);

  useEffect(() => {
    setIsReady(true);
    fetch(1, true);
  }, [...(props.effect ?? [])]);

  const fetch = (fetchPage: number, withReset: boolean = false) => {
    if (!isReady && !withReset) return new Promise((res, rej) => res(null));
    return props.fetch(fetchPage).then((result) => {
      if (withReset) {
        setItemsCount(0);
        setHasMore(true);
        setPage(1);
        setItems([]);
      }
      setItemsCount((c) => c + result.length);
      setItems((i) => i.concat(result as []));
      if (result.length == 0) {
        setHasMore(false);
        return;
      }
      setPage((p) => p + 1);
    });
  };

  return (
    <InfiniteScroll
      dataLength={itemsCount}
      next={() => fetch(page)}
      hasMore={hasMore}
      loader={<LoadingSpinner />}
      endMessage={itemsCount != 0 && <EndMessage />}
    >
      {items.map(props.mapper)}
      {itemsCount == 0 && !hasMore && <NoResults />}
      {itemsCount == 0 && hasMore && <LoadingSpinner />}
    </InfiniteScroll>
  );
}

export default InfiniteScrollWrapper;
