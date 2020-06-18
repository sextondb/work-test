import { NextApiRequest, NextApiResponse } from 'next'
import { sampleUserData, sampleBusinessContactRecordData } from '../../../../../utils/sample-data'

const handler = (req: NextApiRequest, res: NextApiResponse) => {
    const {
        query: { userId, recordId },
    } = req

    const user = sampleUserData.find(x => x.id == Number(userId));
    if (user) {
        const records = sampleBusinessContactRecordData.find(x => x.userId === user.id && x.id === Number(recordId));
        if (records) {
            res.status(200).json(records)
        } else {
            res.status(404).end();
        }
    } else {
        res.status(404).end();
    }

    res.status(200).json(sampleUserData)
    res.end(`User Record: ${userId} ${recordId}`)
}

export default handler