import { Box, Container, Divider, Stack, Typography } from "@mui/material";
import { Favorite as FavoriteIcon } from "@mui/icons-material";
import Link from "next/link";
import LogoIcon from "../common/LogoIcon";

const Footer = () => {
  return (
    <Stack mt={8}>
      <Divider />
      <Container>
        <Stack py={6}>
          <Stack
            width="100%"
            alignItems={{
              xs: "center",
              md: "flex-start",
            }}
            spacing={2}
          >
            <Box
              href="/"
              component={Link}
              sx={{
                display: "flex",
                alignItems: "center",
              }}
            >
              <LogoIcon />
            </Box>
            <Stack
              justifyContent="center"
              textAlign={{
                xs: "center",
                md: "left",
              }}
            >
              <Typography variant="body2" color="text.secondary">
                Vi Books is a individual project to learn{" "}
                <Typography
                  variant="body2"
                  component="a"
                  href="https://nextjs.org/"
                  target="_blank"
                >
                  Next JS
                </Typography>
                .
              </Typography>
              <Typography variant="body2" color="text.secondary">
                Inspired by{" "}
                <Typography
                  variant="body2"
                  component="a"
                  href="https://mui.com/material-ui/"
                  target="_blank"
                >
                  Material UI
                </Typography>{" "}
                and{" "}
                <Typography
                  variant="body2"
                  component="a"
                  href="https://zone-ui.vercel.app/"
                  target="_blank"
                >
                  Zone Template
                </Typography>
                .
              </Typography>
            </Stack>
          </Stack>
        </Stack>
      </Container>
      <Divider />
      <Container>
        <Stack
          py={3}
          direction="row"
          justifyContent="space-between"
          alignItems="center"
        >
          <Typography variant="body1" color="text.secondary">
            Library Website
          </Typography>
          <Stack direction="row" alignItems="center" spacing={1}>
            <Typography>Make with </Typography>
            <FavoriteIcon
              sx={{
                color: "primary.main",
              }}
            />
          </Stack>
        </Stack>
      </Container>
    </Stack>
  );
};

export default Footer;
