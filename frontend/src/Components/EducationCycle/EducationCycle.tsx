import React, { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom';
import { useTranslation } from "react-i18next";

type Props = {
    educationCycleGuid?: string;
}

function EducationCycle(props: Props) {
    const { t } = useTranslation("educationCycles");
    const { educationCycleGuid } = useParams();
    const [educationCycle, setEducationCycle] = useState<any | null>(null);
    useEffect(() => {
        setEducationCycle(null);
    }, [
        props.educationCycleGuid,
        educationCycleGuid
    ]);
    return (
        <div>EducationCycle {`${educationCycleGuid}`}</div>
    )
}

export default EducationCycle

