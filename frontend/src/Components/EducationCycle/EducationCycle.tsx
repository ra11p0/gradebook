import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import { useTranslation } from "react-i18next";
import EducationCyclesProxy from '../../ApiClient/EducationCycles/EducationCyclesProxy';
import EducationCycleResponse from '../../ApiClient/EducationCycles/Definitions/Responses/EducationCycleResponse';
import Notifications from '../../Notifications/Notifications';
import LoadingScreen from '../Shared/LoadingScreen';

type Props = {
    educationCycleGuid?: string;
}

function EducationCycle(props: Props) {
    const { t } = useTranslation("educationCycles");
    const { educationCycleGuid } = useParams();
    const [educationCycle, setEducationCycle] = useState<EducationCycleResponse | undefined>(undefined);
    useEffect(() => {
        const guid = educationCycleGuid ?? props.educationCycleGuid;
        if (!guid) return;
        EducationCyclesProxy.getEducationCycle(guid)
            .then(res => setEducationCycle(res.data))
            .catch(Notifications.showApiError);


    }, [
        props.educationCycleGuid,
        educationCycleGuid
    ]);
    return (
        <LoadingScreen isReady={!!educationCycle}>
            <div>EducationCycle {`${educationCycleGuid}`}</div>
        </LoadingScreen>
    )
}

export default EducationCycle

