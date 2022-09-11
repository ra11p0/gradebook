import { faFile, faFlagCheckered } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { CircularProgress } from "@mui/material";
import React, { ReactElement, useEffect, useState } from "react";
import { Row } from "react-bootstrap";
import { useTranslation } from "react-i18next";
import InfiniteScroll from "react-infinite-scroll-component";

type Props = {
  mapper: (item: any, index: number) => ReactElement;
  fetch: (page: number) => Promise<[]>;
  effect?: any;
};

function InfiniteScrollWrapper(props: Props) {
  const { t } = useTranslation();
  const [items, setItems] = useState([]);
  const [itemsCount, setItemsCount] = useState(0);
  const [page, setPage] = useState(1);
  const [hasMore, setHasMore] = useState(true);

  useEffect(() => {
    setItems([]);
    setItemsCount(0);
    setHasMore(true);
    setPage(1);
    fetch(1);
  }, [...(props.effect ?? [])]);

  const fetch = async (fetchPage: number) => {
    props.fetch(fetchPage).then((result) => {
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
      loader={
        <div className="d-flex gap-3 justify-content-center fs-3">
          <CircularProgress /> {t("loading")}
        </div>
      }
      endMessage={
        itemsCount != 0 && (
          <div className="d-flex gap-3 justify-content-center fs-3">
            <FontAwesomeIcon icon={faFlagCheckered} />
            {t("noMoreResults")}
          </div>
        )
      }
    >
      {items.map(props.mapper)}
      {itemsCount == 0 && (
        <div className="d-flex gap-3 justify-content-center fs-3">
          <FontAwesomeIcon icon={faFlagCheckered} />
          {t("noResults")}
        </div>
      )}
    </InfiniteScroll>
  );
}

export default InfiniteScrollWrapper;
