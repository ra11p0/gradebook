import { Spinner } from "react-bootstrap";
import { useTranslation } from "react-i18next";

type Props = {
  isReady: boolean;
  children?: React.ReactElement;
};

function Loading(props: Props) {
  const { t } = useTranslation();
  return (
    <>
      {props.isReady ? (
        props.children
      ) : (
        <div className="d-flex justify-content-center">
          <Spinner animation="border" role="status" className="m-2 p-2">
            <span className="visually-hidden">{t("loading")}</span>
          </Spinner>
        </div>
      )}
    </>
  );
}

export default Loading;
