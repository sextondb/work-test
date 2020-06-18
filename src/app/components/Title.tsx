import React, { PropsWithChildren } from 'react';
import Typography from '@material-ui/core/Typography';

export default function Title({ children }: PropsWithChildren<{}>) {
    return (
        <Typography component="h2" variant="h6" color="primary" gutterBottom>
            {children}
        </Typography>
    );
}